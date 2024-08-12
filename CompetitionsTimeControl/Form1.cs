using CompetitionsTimeControl.Controllers;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms;
using WMPLib;
using static CompetitionsTimeControl.Controllers.CompetitionController;

namespace CompetitionsTimeControl
{
    public partial class MainForm : Form
    {
        public const string SupportedExtensions = "*.mp3; *.wma; *.wav";
        public const byte TicksToStartCompetition = 5;

        private string CurrentConfigurationFile { get; set; }

        private Action<bool>? _finishCurrentIntervalCallback;
        private Action? _setLblIntervalsElapsedYellowColor;
        private bool _recreatePlaylist;
        private bool _setVisibleAndFocus;
        private bool _closingApplication;
        private byte _ticksToStartCounter;
        private BeepsController _beepsController = null!;
        private CompetitionController _competitionController = null!;
        private MusicsController _musicsController = null!;
        private readonly Stopwatch _stopwatch;
        private readonly string _originalFormText;
        private long _lastMillisecond;

        public MainForm()
        {
            CurrentConfigurationFile = "";
            _lastMillisecond = 0;
            InitializeComponent();
            ConfigureBeepMediaPlayer();
            ConfigureMusicMediaPlayer();
            _finishCurrentIntervalCallback = null;
            _setLblIntervalsElapsedYellowColor = null;
            _recreatePlaylist = false;
            _closingApplication = false;
            _ticksToStartCounter = TicksToStartCompetition;
            _originalFormText = this.Text;

            TimerController.CreateThreadTimer(ThreadTimerTick);
            _stopwatch = new Stopwatch();
        }

        private void ConfigureBeepMediaPlayer()
        {
            BeepMediaPlayer.settings.autoStart = true;
            BeepMediaPlayer.uiMode = "none";
            BeepMediaPlayer.enableContextMenu = false;
            BeepMediaPlayer.Ctlenabled = false;
        }

        private void ConfigureMusicMediaPlayer()
        {
            MusicMediaPlayer.settings.autoStart = false;
            MusicMediaPlayer.uiMode = "full";
            MusicMediaPlayer.enableContextMenu = false;
            MusicMediaPlayer.Ctlenabled = false;
        }

        private void SetFormTextWithCurrentFileName()
            => this.Text = $"{_originalFormText} [ {Path.GetFileName(CurrentConfigurationFile)} ]";

        private void MainForm_Activated(object sender, EventArgs e)
        {
            if (_beepsController == null)
            {
                _beepsController = new(ComboBoxBeepPair);

                if (_beepsController != null)
                {
                    _beepsController.TimeBeforePlayBeeps = (float)NumUDTimeBeforePlayBeeps.Value;
                    _beepsController.AmountOfBeeps = (int)NumUDAmountOfBeeps.Value;
                    _beepsController.TimeForEachBeep = (float)NumUDTimeForEachBeep.Value;
                    _beepsController.CurrentBeepsVolume = (byte)(TBBeepVolume.Value * -1);
                    _beepsController.TimeForResumeMusics = (float)NumUDTimeForResumeMusics.Value;
                    ComboBoxHalfIntervalBeep.SelectedIndex = 0;
                }
            }

            if (_musicsController == null)
            {
                _musicsController = new(ListViewMusics, LblListViewMusicsStatus);

                if (_musicsController != null)
                {
                    _musicsController.LimitMinMusicVolume = (byte)(TBMusicVolumeMin.Value * -1);
                    _musicsController.LimitMaxMusicVolume = (byte)(TBMusicVolumeMax.Value * -1);
                    _musicsController.CurrentMusicVolume = (byte)(TBMusicCurrentVol.Value * -1);
                }
            }

            if (_competitionController == null)
            {
                _competitionController = new(LblIntervalAndCompetitionFormatedTime, ProgressBarCurrentIntervalElapsed, LblIntervalsElapsed,
                    ProgressBarCompetitionElapsedTime, LblCurrentIntervalElapsedTime, LblCompetitionElapsedTime);

                if (_competitionController != null)
                {
                    _competitionController.TimeToChangeVolume = (byte)NumUDTimeToVolMin.Value;
                    _competitionController.StartWithBeeps = CheckBoxStartWithBeeps.Checked;
                    _competitionController.StopMusicsAtEnd = CheckBoxStopMusicsAtEnd.Checked;
                    _competitionController.SetCompetitionAmountIntervals((byte)NumUDCompetitionAmountIntervals.Value);
                    _competitionController.SetCompetitionIntervalSeconds((int)NumUDCompetitionIntervalSeconds.Value);

                    if (_beepsController != null && _musicsController != null)
                    {
                        _stopwatch.Start();
                        TimerController.StartThreadTimer();
                    }
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_competitionController.CanRunCompetition)
            {
                StringBuilder sb = new("A competição está em andamento!\n\n");
                sb.Append("Tem certeza que deseja fechar a tela e cancelar a competição?");

                if (MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
            }

            _closingApplication = true;
            TimerController.StopThreadTimer();
            _stopwatch.Stop();
            BeepMediaPlayer.close();
            MusicMediaPlayer.close();
            BeepMediaPlayer.Dispose();
            MusicMediaPlayer.Dispose();
            TimerController.DisposeThreadTimer();
        }

        private void ThreadTimerTick(object? stateInfo)
        {
            try
            {
                Invoke(() =>
                {
                    if (this.IsDisposed)
                        return;

                    FormTimerTick();
                });
            }
            catch (ObjectDisposedException)
            {
            }
        }

        private void FormTimerTick()
        {
            if (_closingApplication)
                return;

            long currentMillisecond = _stopwatch.ElapsedMilliseconds; // up to 2 trillion hours

            int elapsedTime = (int)Math.Abs(currentMillisecond - _lastMillisecond);

            _lastMillisecond = currentMillisecond;

            MusicMediaPlayer.fullScreen = false;
            MusicMediaPlayer.settings.volume = TBMusicCurrentVol.Value * -1;

            if (_beepsController == null || _musicsController == null || _competitionController == null)
                return;

            if (_competitionController.CanRunCompetition)
            {
                _competitionController.TryPerformCompetition(_beepsController, _musicsController,
                    ChooseHowToClearPlaylist, StopCompetition, PerformCountdownBeep, _stopwatch.ElapsedMilliseconds);

                if (_musicsController.SecondsToChangeVolume != null && _competitionController.ProgramIsRunning)
                {
                    _musicsController.TryChangeVolume(MusicMediaPlayer, TBMusicCurrentVol, TBMusicVolumeMin,
                        TBMusicVolumeMax, elapsedTime);
                }

                if (_competitionController.CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps)
                {
                    _musicsController.SelectPlayingMusic(MusicMediaPlayer, ref _setVisibleAndFocus);
                    _musicsController.AutoChangeToNextMusic(MusicMediaPlayer, elapsedTime);
                }

                if (!_musicsController.HasJustOneValidMusic && _recreatePlaylist)
                {
                    TryToRecreatePlaylist();
                    _recreatePlaylist = false;
                }
            }
            else
            {
                _competitionController.KeepLastMillisecondUpdated(_stopwatch.ElapsedMilliseconds);
            }

            if (_beepsController.CanPerformBeepsEvent &&
                (_competitionController.CanRunCompetition == _competitionController.ProgramIsRunning))
            {
                _beepsController.TryPerformBeeps(BeepMediaPlayer, elapsedTime, LblTestMessages,
                    _finishCurrentIntervalCallback, _setLblIntervalsElapsedYellowColor);

                if (!_competitionController.CanRunCompetition && !_beepsController.CanPerformBeepsEvent)
                {
                    PrepareBeepsTest(false);
                    ComboBoxProgramming.Enabled = true;
                    CheckEnableCompetitionControls();
                }
            }

            // Competition start separated from BtnStartCompetition_Click by '_ticksToStartCounter' ticks.
            // OBS: Workaround to avoid intermittent freezing (as it doesn't always happen) of the form when
            //      the competition is started with beeps and music at the same time.
            if (!_competitionController.CanRunCompetition && !_competitionController.ProgramIsRunning &&
                _competitionController.IsPreparationComplete)
            {
                if (_ticksToStartCounter > 0)
                {
                    _ticksToStartCounter--;
                    return;
                }

                _competitionController.StartCompetition();
            }
        }

        #region STRIP MENU
        private void ToolStripMenuItemOpenConfiguration_Click(object sender, EventArgs e)
        {
            string filePath = ConfigurationsDataController.GetFileToOpenConfiguration(
                out ConfigurationsDataController? dataController);

            if (dataController == null || filePath == "")
                return;

            BeepsController dataBeepsController = dataController.DataBeepsController;
            MusicsController dataMusicsController = dataController.DataMusicsController;
            CompetitionController dataCompetitionController = dataController.DataCompetitionController;

            // Não foi possível ler arquivo. Mensagem aqui
            if (dataBeepsController == null || dataMusicsController == null || dataCompetitionController == null)
                return;

            ComboBoxBeepPair.Text = dataBeepsController.CurrentBeepPairText;
            ComboBoxHalfIntervalBeep.SelectedIndex = dataBeepsController.HasHalfIntervalBeep ? 1 : 0;
            NumUDTimeBeforePlayBeeps.Value = (decimal)dataBeepsController.TimeBeforePlayBeeps;
            NumUDAmountOfBeeps.Value = dataBeepsController.AmountOfBeeps;
            NumUDTimeForEachBeep.Value = (decimal)dataBeepsController.TimeForEachBeep;
            NumUDTimeForResumeMusics.Value = (decimal)dataBeepsController.TimeForResumeMusics;
            TBBeepVolume.Value = dataBeepsController.CurrentBeepsVolume * -1;

            if (_musicsController?.FillListView([.. dataMusicsController.MusicsPathsList], MusicMediaPlayer, true) ?? false)
                EnableControlsHavingItems(true);

            TogglePlaylistMode.Checked = dataMusicsController.PlaylistInRandomMode;
            ToggleSeeDetails.Checked = !dataMusicsController.IsSimplifiedView;
            TBMusicVolumeMin.Value = dataMusicsController.LimitMinMusicVolume * -1;
            TBMusicVolumeMax.Value = dataMusicsController.LimitMaxMusicVolume * -1;
            TBMusicCurrentVol.Value = dataMusicsController.CurrentMusicVolume * -1;

            ComboBoxProgramming.SelectedIndex = (int)dataCompetitionController.CompetitionProgramSetup;
            ComboBoxInitialization.SelectedIndex = (int)dataCompetitionController.InitializationProgramOption;
            ComboBoxRepeatPlaylist.SelectedIndex = (int)dataCompetitionController.PlaylistRestartMode;
            NumUDTimeToVolMin.Value = dataCompetitionController.TimeToChangeVolume;
            CheckBoxStartWithBeeps.Checked = dataCompetitionController.StartWithBeeps;
            CheckBoxStopMusicsAtEnd.Checked = dataCompetitionController.StopMusicsAtEnd;
            NumUDCompetitionAmountIntervals.Value = dataCompetitionController.CompetitionAmountIntervals;
            NumUDCompetitionIntervalSeconds.Value = dataCompetitionController.CompetitionIntervalSeconds;
            CheckEnableCompetitionControls();

            CurrentConfigurationFile = filePath;
            ToolStripMenuItemSaveConfiguration.Enabled = CurrentConfigurationFile != "";
            SetFormTextWithCurrentFileName();
        }

        private void ToolStripMenuItemSaveConfiguration_Click(object sender, EventArgs e)
        {
            ConfigurationsDataController.SaveFileConfiguration(CurrentConfigurationFile, _beepsController,
                _musicsController, _competitionController);
        }

        private void ToolStripMenuItemSaveConfigurationAs_Click(object sender, EventArgs e)
        {
            CurrentConfigurationFile = ConfigurationsDataController.SaveFileConfigurationAs(_beepsController,
                _musicsController, _competitionController);

            ToolStripMenuItemSaveConfiguration.Enabled = CurrentConfigurationFile != "";
            SetFormTextWithCurrentFileName();
        }

        private void ToolStripMenuItemEnableTextAndButtonsTips_Click(object sender, EventArgs e)
        {
            ToolStripMenuItemEnableTextAndButtonsTips.Checked = !ToolStripMenuItemEnableTextAndButtonsTips.Checked;
            ToolTip.Active = ToolStripMenuItemEnableTextAndButtonsTips.Checked;

            if (!ToolTip.Active)
                return;

            MusicsController.UpdateAllDinamicToolTip(ToggleMarkAndExclude, _musicsController?.HasCheckedMusicToDelete ?? false,
                TogglePlayMusicBySelection, TogglePlaylistMode, ToolTip);
        }

        private void ToolStripMenuItemReloadBeepList_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new("Deseja buscar por novos pares de beeps?\n\n");
            sb.Append("As opções atuais serão apagadas e recarregadas ao prosseguir.");

            if (sb == null || MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }

            _beepsController?.GetBeepsSounds(ComboBoxBeepPair);
            ComboBoxBeepPair_SelectedIndexChanged(sender, EventArgs.Empty);
        }

        private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {
            AboutForm aboutForm = new();
            aboutForm.ShowDialog();
        }

        private void ToolStripMenuItemCloseProgram_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void SetEnableToolStripMenuItems(bool enable)
        {
            ToolStripMenuItemOpenConfiguration.Enabled = enable;
            ToolStripMenuItemSaveConfiguration.Enabled = enable && CurrentConfigurationFile != "";
            ToolStripMenuItemSaveConfigurationAs.Enabled = enable;
            ToolStripMenuItemReloadBeepList.Enabled = enable;
        }
        #endregion

        #region BEEPS CONTROLLER
        private void ComboBoxBeepPair_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enableButtons = _beepsController?.ChangeBeepPairSelection(
                ComboBoxBeepPair.SelectedIndex, ComboBoxBeepPair.Text, BeepMediaPlayer) ?? false;

            NumUDTimeForResumeMusics_ValueChanged(sender, e);
            SetEnableBeepsControls(enableButtons);
            ComboBoxProgramming.Enabled = enableButtons;

            if (ComboBoxBeepPair.Items.Count > 0)
                ComboBoxBeepPair.Enabled = true;
            else
            {
                ComboBoxBeepPair.Enabled = false;
                ComboBoxProgramming.SelectedIndex = -1;
            }
            EnableControlsToStartCompetition();
        }

        private void ComboBoxHalfIntervalBeep_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (_beepsController != null)
                _beepsController.HasHalfIntervalBeep = ComboBoxHalfIntervalBeep.SelectedIndex > 0;
        }

        private void NumUDTimeBeforePlayBeeps_ValueChanged(object sender, EventArgs e)
        {
            if (_beepsController != null)
                _beepsController.TimeBeforePlayBeeps = (float)NumUDTimeBeforePlayBeeps.Value;
        }

        private void NumUDAmountOfBeeps_ValueChanged(object sender, EventArgs e)
        {
            if (_beepsController != null)
                _beepsController.AmountOfBeeps = (int)NumUDAmountOfBeeps.Value;
        }

        private void NumUDTimeForEachBeep_ValueChanged(object sender, EventArgs e)
        {
            if (_beepsController != null)
                _beepsController.TimeForEachBeep = (float)NumUDTimeForEachBeep.Value;
        }

        private void NumUDTimeForResumeMusics_ValueChanged(object sender, EventArgs e)
        {
            if (_beepsController != null)
                _beepsController.TimeForResumeMusics = (float)NumUDTimeForResumeMusics.Value;
        }

        private void TBBeepVolume_ValueChanged(object sender, EventArgs e)
        {
            byte beepVolume = (byte)(TBBeepVolume.Value * -1);

            LblBeepVolumePercent.Text = $"{beepVolume}%";
            BeepMediaPlayer.settings.volume = beepVolume;
            _beepsController.CurrentBeepsVolume = beepVolume;
        }

        private void BtnBeepsTest_Click(object sender, EventArgs e)
        {
            _beepsController.CanPerformBeepsEvent = true;
            PrepareBeepsTest(true);
            DisableCompetitionControls();
            BtnStartCompetition.Enabled = false;
        }

        private void PrepareBeepsTest(bool startTest)
        {
            SetEnableBeepsControls(!startTest);
            BtnBeepsTest.Visible = !startTest;
            BtnCountdownBeepTest.Visible = !startTest;
            BtnStartBeepTest.Visible = !startTest;
            LblTestMessages.Visible = startTest;
        }

        private void SetEnableBeepsControls(bool enable)
        {
            ComboBoxBeepPair.Enabled = enable;
            ComboBoxHalfIntervalBeep.Enabled = enable;
            NumUDTimeBeforePlayBeeps.Enabled = enable;
            NumUDAmountOfBeeps.Enabled = enable;
            NumUDTimeForEachBeep.Enabled = enable;
            NumUDTimeForResumeMusics.Enabled = enable;
            BtnBeepsTest.Enabled = enable;
            BtnCountdownBeepTest.Enabled = enable;
            BtnStartBeepTest.Enabled = enable;
        }

        private void BtnCountdownBeepTest_Click(object sender, EventArgs e)
        {
            PerformCountdownBeep();
        }

        private void PerformCountdownBeep()
        {
            BeepsController.PlayBeep(BeepMediaPlayer, _beepsController.LowBeepPath);

            if (_setLblIntervalsElapsedYellowColor == null || _competitionController?.CanPerformHalfIntervalBeep == false)
                return;
            
            _setLblIntervalsElapsedYellowColor();
        }

        private void BtnStartBeepTest_Click(object sender, EventArgs e)
        {
            BeepsController.PlayBeep(BeepMediaPlayer, _beepsController.HighBeepPath);
        }

        private void BtnBeepTest_MouseHover(object sender, EventArgs e)
        {
            string toolTipMessage = _beepsController?.GetBtnConfigTestToolTipMsg() ?? string.Empty;

            ToolTip.SetToolTip(BtnBeepsTest, toolTipMessage);
        }
        #endregion

        #region MUSICS CONTROLLER
        private void TryToRecreatePlaylist()
        {
            bool isToCheckTogglePlaylistMode = false;
            bool currentModeIsShuffle = TogglePlaylistMode.Checked;
            bool recreatePlaylist = false;

            switch (_competitionController.PlaylistRestartMode)
            {
                case PlaylistRestart.ShuffleList:
                    isToCheckTogglePlaylistMode = true;
                    recreatePlaylist = true;
                    break;

                case PlaylistRestart.SequentialList:
                    isToCheckTogglePlaylistMode = false;
                    recreatePlaylist = currentModeIsShuffle;
                    break;

                case PlaylistRestart.InvertLastMode:
                    isToCheckTogglePlaylistMode = !currentModeIsShuffle;
                    recreatePlaylist = true;
                    break;
            }

            TogglePlaylistMode.Checked = isToCheckTogglePlaylistMode;

            if (recreatePlaylist)
                _musicsController.CreatePlaylist(MusicMediaPlayer, true);
            else
                MusicMediaPlayer.Ctlcontrols.play();
        }

        private void ChooseHowToClearPlaylist(bool clearAllPlaylist)
        {
            if (_competitionController?.CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps)
            {
                TogglePlayMusicBySelection.Checked = !clearAllPlaylist;
                MusicsController.ChooseHowToClearPlaylist(MusicMediaPlayer, clearAllPlaylist);
            }
        }

        private void SetEnableMusicsControls(bool enable)
        {
            BtnAddMusics.Enabled = enable;
            TogglePlaylistMode.Enabled = enable;
            EnableControlsHavingItems(enable && ListViewMusics.Items.Count > 0);
        }

        private void BtnAddMusics_Click(object sender, EventArgs e)
        {
            if (_musicsController?.AddMusicsToList(MusicMediaPlayer) ?? false)
                EnableControlsHavingItems(true);
        }

        private void BtnClearMusicsList_Click(object sender, EventArgs e)
        {
            if (_musicsController?.TryClearListViewMusics() ?? false)
            {
                if (ComboBoxProgramming.SelectedIndex > 0)
                    ComboBoxProgramming.SelectedIndex = 0;

                TogglePlayMusicBySelection.Checked = false;
                EnableControlsHavingItems(false);
            }
        }

        private void EnableControlsHavingItems(bool enable)
        {
            if (!enable)
                ToggleMarkAndExclude.Checked = false;

            BtnClearMusicsList.Enabled = enable;
            ToggleMarkAndExclude.Enabled = enable;
            TogglePlayMusicBySelection.Enabled = enable;
            ListViewMusics.GridLines = enable;
        }

        private void ToggleMarkAndExclude_CheckedChanged(object sender, EventArgs e)
        {
            _musicsController?.DeleteCheckedMusics(!ToggleMarkAndExclude.Checked);

            bool hasItemsInTheList = ListViewMusics.Items.Count > 0;
            bool hasValidMusics = _musicsController?.HasValidMusics() ?? false;

            if (ComboBoxProgramming.SelectedIndex > 0 && !hasValidMusics)
                ComboBoxProgramming.SelectedIndex = 0;

            TogglePlayMusicBySelection.Checked = false;
            EnableControlsHavingItems(hasItemsInTheList);
            TogglePlayMusicBySelection.Enabled = !ToggleMarkAndExclude.Checked && hasItemsInTheList;
            _musicsController?.SetMarkAndExcludeTextAndToolTip(ToggleMarkAndExclude, ToolTip);
        }

        private void ListViewMusics_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            _musicsController?.ListViewMusicsItemChecked(e.Item);
            _musicsController?.SetMarkAndExcludeTextAndToolTip(ToggleMarkAndExclude, ToolTip);
        }

        private void ListViewMusics_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            if (!_competitionController?.CanRunCompetition ?? false)
                _musicsController?.PlayPauseSelectedMusic(TogglePlayMusicBySelection.Checked, e.Item, MusicMediaPlayer);
        }

        private void TogglePlayMusicBySelection_CheckedChanged(object sender, EventArgs e)
        {
            _musicsController?.TogglePlayMusicBySelectionCheckedChanged(TogglePlayMusicBySelection, ToolTip,
                MusicMediaPlayer, _competitionController.CanRunCompetition || _competitionController.IsPreparationComplete);
        }

        private void TogglePlaylistMode_CheckedChanged(object sender, EventArgs e)
        {
            _musicsController?.TogglePlaylistModeCheckedChanged(TogglePlaylistMode, ToolTip);
        }

        private void ToggleSeeDetails_CheckedChanged(object sender, EventArgs e)
        {
            _musicsController?.ToggleSeeDetailsCheckedChanged(ToggleSeeDetails);
        }

        private void TBMusicVolumeMin_ValueChanged(object sender, EventArgs e)
        {
            // Se volume mínimo (barra é negativa) passar do volume máximo, atualiza volume máximo.
            if (TBMusicVolumeMax.Value > TBMusicVolumeMin.Value)
                TBMusicVolumeMax.Value = TBMusicVolumeMin.Value;

            // Se volume mínimo (barra é negativa) passar do volume atual, atualiza volume atual.
            if (TBMusicCurrentVol.Value > TBMusicVolumeMin.Value)
                TBMusicCurrentVol.Value = TBMusicVolumeMin.Value;

            byte musicVolumeMin = (byte)(TBMusicVolumeMin.Value * -1);

            LblMusicVolMinPercent.Text = $"{musicVolumeMin}%";
            _musicsController.LimitMinMusicVolume = musicVolumeMin;
        }

        private void TBMusicVolumeMax_ValueChanged(object sender, EventArgs e)
        {
            // Se volume máximo (barra é negativa) passar do volume atual, atualiza volume atual.
            if (TBMusicCurrentVol.Value < TBMusicVolumeMax.Value)
                TBMusicCurrentVol.Value = TBMusicVolumeMax.Value;

            // Se volume máximo (barra é negativa) passar do volume mínimo, atualiza volume mínimo.
            if (TBMusicVolumeMin.Value < TBMusicVolumeMax.Value)
                TBMusicVolumeMin.Value = TBMusicVolumeMax.Value;

            byte musicVolumeMax = (byte)(TBMusicVolumeMax.Value * -1);

            LblMusicVolMaxPercent.Text = $"{musicVolumeMax}%";
            _musicsController.LimitMaxMusicVolume = musicVolumeMax;
        }

        private void MusicMediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (_competitionController.CompetitionProgramSetup != CompetitionProgram.MusicsAndBeeps ||
                !_competitionController.CanRunCompetition)
            {
                return;
            }

            if ((WMPPlayState)e.newState == WMPPlayState.wmppsReady)
                _recreatePlaylist = _musicsController?.IsTheLastPlaylistMusic() ?? false;

            if ((WMPPlayState)e.newState == WMPPlayState.wmppsPlaying)
                _musicsController?.SetCurrentPlaylistMedia(MusicMediaPlayer);

            if ((WMPPlayState)e.newState == WMPPlayState.wmppsTransitioning)
                _setVisibleAndFocus = true;
        }

        private void TBMusicCurrentVol_ValueChanged(object sender, EventArgs e)
        {
            // Se volume atual (barra é negativa) passar do volume mínimo, atualiza volume mínimo.
            if (TBMusicVolumeMin.Value < TBMusicCurrentVol.Value)
                TBMusicVolumeMin.Value = TBMusicCurrentVol.Value;

            // Se volume atual (barra é negativa) passar do volume máximo, atualiza volume máximo.
            if (TBMusicVolumeMax.Value > TBMusicCurrentVol.Value)
                TBMusicVolumeMax.Value = TBMusicCurrentVol.Value;

            byte musicVolume = (byte)(TBMusicCurrentVol.Value * -1);

            LblMusicCurrentVolPercent.Text = $"{musicVolume}%";
            _musicsController.CurrentMusicVolume = musicVolume;
        }
        #endregion

        #region COMPETITION CONTROLLER
        private void ComboBoxProgramming_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckEnableCompetitionControls();
        }

        private void CheckEnableCompetitionControls()
        {
            bool enableMoreControls = false;

            if (_musicsController == null || _competitionController == null)
                return;

            _competitionController.ChangeCompetitionProgramSelection(ComboBoxProgramming.SelectedIndex);

            if (_competitionController.CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps)
            {
                enableMoreControls = ListViewMusics.Items.Count > 0;

                if (!enableMoreControls)
                {
                    StringBuilder sb = new("Não há nenhuma música na lista para tocar durante a competição.\n\n");
                    sb.Append("Adicione músicas na lista se deseja esta opção.");

                    MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ComboBoxProgramming.SelectedIndex = 0;
                }
                else
                {
                    enableMoreControls = _musicsController.HasValidMusics();

                    if (!enableMoreControls)
                    {
                        StringBuilder sb = new("Não há músicas válidas. (Músicas em vermelho na lista são inválidas).\n\n");
                        sb.Append("Adicione músicas válidas para tocar se deseja esta opção para a competição.");

                        MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        ComboBoxProgramming.SelectedIndex = 0;
                    }
                }
            }

            ComboBoxInitialization.Enabled = enableMoreControls;
            ComboBoxRepeatPlaylist.Enabled = enableMoreControls;
            CheckBoxStartWithBeeps.Enabled = true;
            EnableControlsToStartCompetition();
        }

        private void ComboBoxInitialization_SelectedIndexChanged(object sender, EventArgs e)
        {
            _competitionController?.ChangeInitializationProgramSelection(ComboBoxInitialization.SelectedIndex);
            EnableControlsToStartCompetition();
        }

        private void ComboBoxRepeatPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            _competitionController?.ChangePlaylistRestartSelection(ComboBoxRepeatPlaylist.SelectedIndex);
            EnableControlsToStartCompetition();
        }

        private void EnableControlsToStartCompetition()
        {
            bool enableOnlyBeepsControls = ComboBoxProgramming.SelectedIndex == 0; // "CompetitionProgram.OnlyBeeps"

            bool enableMusicsAndBeepsControls = ComboBoxProgramming.SelectedIndex > 0 &&
                ComboBoxInitialization.SelectedIndex >= 0 && ComboBoxRepeatPlaylist.SelectedIndex >= 0;

            bool canStartCompetition = ComboBoxProgramming.Enabled &&
                (enableOnlyBeepsControls || enableMusicsAndBeepsControls);

            if (_competitionController != null)
            {
                NumUDTimeToVolMin.Enabled = enableMusicsAndBeepsControls;
                CheckBoxStopMusicsAtEnd.Enabled = enableMusicsAndBeepsControls;
                NumUDCompetitionAmountIntervals.Enabled = canStartCompetition;
                NumUDCompetitionIntervalSeconds.Enabled = canStartCompetition;
                BtnStartCompetition.Enabled = canStartCompetition;
            }
        }

        private void NumUDTimeToVolMin_ValueChanged(object sender, EventArgs e)
        {
            if (_competitionController != null)
                _competitionController.TimeToChangeVolume = (byte)NumUDTimeToVolMin.Value;
        }

        private void CheckBoxStartWithBeeps_CheckedChanged(object sender, EventArgs e)
        {
            if (_competitionController != null)
                _competitionController.StartWithBeeps = CheckBoxStartWithBeeps.Checked;
        }

        private void CheckBoxStopMusicsAtEnd_CheckedChanged(object sender, EventArgs e)
        {
            if (_competitionController != null)
                _competitionController.StopMusicsAtEnd = CheckBoxStopMusicsAtEnd.Checked;
        }

        private void NumUDCompetitionAmountIntervals_ValueChanged(object sender, EventArgs e)
        {
            _competitionController?.SetCompetitionAmountIntervals((byte)NumUDCompetitionAmountIntervals.Value);
        }

        private void NumUDCompetitionIntervalSeconds_ValueChanged(object sender, EventArgs e)
        {
            _competitionController?.SetCompetitionIntervalSeconds((int)NumUDCompetitionIntervalSeconds.Value);
        }

        private void DisableCompetitionControls()
        {
            ComboBoxProgramming.Enabled = false;
            ComboBoxInitialization.Enabled = false;
            NumUDTimeToVolMin.Enabled = false;
            CheckBoxStartWithBeeps.Enabled = false;
            NumUDCompetitionAmountIntervals.Enabled = false;
            NumUDCompetitionIntervalSeconds.Enabled = false;
        }

        private void BtnStartCompetition_Click(object sender, EventArgs e)
        {
            if (_beepsController == null || _musicsController == null || _competitionController == null)
                return;

            if (_competitionController.CanRunCompetition)
            {
                _competitionController.TogglePause();
                MusicsController.PrepareForPause(TBMusicCurrentVol);

                if (_competitionController.CanRunCompetition) // Check again in case of competition timeout
                {
                    if (_competitionController.ProgramIsRunning)
                    {
                        BtnStartCompetition.Text = " Pausar";
                        BtnStartCompetition.ImageIndex = 4;
                    }
                    else
                    {
                        BtnStartCompetition.Text = "Retomar";
                        BtnStartCompetition.ImageIndex = 3;
                    }
                }
                return;
            }

            bool hasMusicsToPlaylist = _competitionController.ValidateHasMusicsToPlaylist(_musicsController,
                out bool skipCanStartMessage);

            CheckEnableCompetitionControls();

            if (_competitionController.ValidateBeepsEventTimes(_beepsController) &&
                _competitionController.HasTimeForHalfIntervalBeep(_beepsController) && hasMusicsToPlaylist &&
                _competitionController.CanStartCompetition(skipCanStartMessage))
            {
                _setVisibleAndFocus = true;
                _competitionController.BlockListviewAndCreatePlaylist(MusicMediaPlayer, _musicsController);
                _competitionController.PrepareToStart(out _finishCurrentIntervalCallback, out _setLblIntervalsElapsedYellowColor);

                SetEnableBeepsControls(false);
                SetEnableMusicsControls(false);
                TogglePlayMusicBySelection.Checked = false;
                DisableCompetitionControls();
                SetEnableToolStripMenuItems(false);
                BtnStartCompetition.Text = " Pausar";
                BtnStartCompetition.ImageIndex = 4;
                BtnStopCompetition.Enabled = true;
                _ticksToStartCounter = TicksToStartCompetition;
            }
        }

        private void BtnStopCompetition_Click(object sender, EventArgs e)
        {
            if (_competitionController == null)
                return;

            if (_competitionController.StopCompetition())
            {
                StopCompetition();
            }
        }

        private void StopCompetition()
        {
            _beepsController.CancelBeepsEvent();
            _musicsController.RepaintListViewGrid(true);
            TBMusicCurrentVol.Enabled = true;
            TBMusicVolumeMin.Enabled = true;
            TBMusicVolumeMax.Enabled = true;
            SetEnableBeepsControls(true);
            SetEnableMusicsControls(true);
            ComboBoxProgramming.Enabled = true;
            SetEnableToolStripMenuItems(true);
            CheckEnableCompetitionControls();
            BtnStartCompetition.Text = " Iniciar";
            BtnStartCompetition.ImageIndex = 3;
            BtnStopCompetition.Enabled = false;
            ChooseHowToClearPlaylist(_competitionController.StopMusicsAtEnd);
        }
        #endregion
    }
}
