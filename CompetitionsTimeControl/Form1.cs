using CompetitionsTimeControl.Controllers;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Text;
using WMPLib;
using static CompetitionsTimeControl.Controllers.CompetitionController;
using static System.Windows.Forms.DataFormats;

namespace CompetitionsTimeControl
{
    public partial class MainForm : Form
    {
        public const string SupportedExtensions = "*.mp3; *.wma; *.wav";

        private string CurrentConfigurationFile { get; set; }

        private Action? _finishCurrentIntervalCallback;
        private bool _recreatePlaylist;
        private bool _setVisibleAndFocus;
        private bool _closingApplication;
        private BeepsController _beepsController = null!;
        private CompetitionController _competitionController = null!;
        private MusicsController _musicsController = null!;
        private Stopwatch _stopwatch;
        private int _lastMillisecond;

        public MainForm()
        {
            CurrentConfigurationFile = "";
            _lastMillisecond = 0;
            InitializeComponent();
            ConfigureBeepMediaPlayer();
            ConfigureMusicMediaPlayer();
            _finishCurrentIntervalCallback = null;
            _recreatePlaylist = false;
            _closingApplication = false;

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
                    _beepsController.SetVolumePercentage((byte)(TBBeepVolume.Value * -1));
                    _beepsController.TimeForResumeMusics = (float)NumUDTimeForResumeMusics.Value;
                }
            }

            if (_musicsController == null)
            {
                _musicsController = new(ListViewMusics);

                if (_musicsController != null)
                {
                    _musicsController.SetMinVolumePercentage((byte)(TBMusicVolumeMin.Value * -1));
                    _musicsController.SetMaxVolumePercentage((byte)(TBMusicVolumeMax.Value * -1));
                    _musicsController.SetVolumePercentage((byte)(TBMusicCurrentVol.Value * -1));
                }
            }

            if (_competitionController == null)
            {
                _competitionController = new(LblCompetitionTotalTime, ProgressBarCurrentIntervalElapsed, LblIntervalsElapsed,
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
            /*if (!TimerController.ThreadTimerIsRunning || _closingApplication)
                return;*/
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

            if (_stopwatch.ElapsedMilliseconds < _lastMillisecond)
            {
                _lastMillisecond = 0;
                _stopwatch.Restart();
            }

            int currentMillisecond = (int)_stopwatch.ElapsedMilliseconds;

            int elapsedTime = Math.Abs(currentMillisecond - _lastMillisecond);

            _lastMillisecond = currentMillisecond;

            MusicMediaPlayer.fullScreen = false;
            MusicMediaPlayer.settings.volume = TBMusicCurrentVol.Value * -1;

            if (_beepsController == null || _musicsController == null || _competitionController == null)
                return;

            if (_competitionController.CanRunCompetition)
            {
                _competitionController.TryPerformCompetition(_beepsController, _musicsController,
                    ChooseHowToClearPlaylist, StopCompetition, elapsedTime);

                if (_musicsController.SecondsToChangeVolume != null && _competitionController.ProgramIsRunning)
                {
                    _musicsController.TryChangeVolume(MusicMediaPlayer, TBMusicCurrentVol, TBMusicVolumeMin,
                        TBMusicVolumeMax, elapsedTime);
                }

                if (_competitionController.CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps &&
                    !_recreatePlaylist)
                {
                    _musicsController.SelectPlayingMusic(MusicMediaPlayer, ref _setVisibleAndFocus);

                    if (!MusicMediaPlayer.settings.getMode("loop"))
                        _musicsController.AutoChangeToNextMusic(MusicMediaPlayer, elapsedTime);
                }

                if (!MusicMediaPlayer.settings.getMode("loop") && _recreatePlaylist)
                {
                    TryToRecreatePlaylist();
                    _recreatePlaylist = false;
                }
            }

            if (_beepsController.CanPerformBeepsEvent &&
                (_competitionController.CanRunCompetition == _competitionController.ProgramIsRunning))
            {
                _beepsController.TryPerformBeeps(BeepMediaPlayer, elapsedTime, LblTestMessages,
                    _finishCurrentIntervalCallback);

                if (!_competitionController.CanRunCompetition && !_beepsController.CanPerformBeepsEvent)
                {
                    PrepareBeepsTest(false);
                    CheckEnableCompetitionControls();
                    ComboBoxProgramming.Enabled = true;
                    BtnStartCompetition.Enabled = true;
                }
            }
        }

        #region STRIP MENU
        private void ToolStripMenuItemOpenConfiguration_Click(object sender, EventArgs e)
        {
            string filePath = ConfigurationsDataController.GetFileToOpenConfiguration(
                out ConfigurationsDataController? dataController);
            
            if (dataController == null)
                return;

            BeepsController dataBeepsController = dataController.DataBeepsController;
            MusicsController dataMusicsController = dataController.DataMusicsController;
            CompetitionController dataCompetitionController = dataController.DataCompetitionController;

            // Não foi possível ler arquivo. Mensagem aqui
            if (dataBeepsController == null || dataMusicsController == null || dataCompetitionController == null)
                return;

            ComboBoxBeepPair.Text = dataBeepsController.CurrentBeepPairText;
            NumUDTimeBeforePlayBeeps.Value = (decimal)dataBeepsController.TimeBeforePlayBeeps;
            NumUDAmountOfBeeps.Value = dataBeepsController.AmountOfBeeps;
            NumUDTimeForEachBeep.Value = (decimal)dataBeepsController.TimeForEachBeep;
            NumUDTimeForResumeMusics.Value = (decimal)dataBeepsController.TimeForResumeMusics;
            TBBeepVolume.Value = dataBeepsController.CurrentBeepsVolume * -1;

            if (_musicsController?.FillListView([.. dataMusicsController.MusicsPathsList], MusicMediaPlayer) ?? false)
                EnableControlsHavingItems(_musicsController?.HasValidMusics() ?? false);

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
        }

        private void ToolStripMenuItemEnableTextAndButtonsTips_Click(object sender, EventArgs e)
        {
            ToolStripMenuItemEnableTextAndButtonsTips.Checked = !ToolStripMenuItemEnableTextAndButtonsTips.Checked;
            ToolTip.Active = ToolStripMenuItemEnableTextAndButtonsTips.Checked;
        }

        private void ToolStripMenuItemReloadBeepList_Click(object sender, EventArgs e)
        {
            _beepsController?.GetBeepsSounds(ComboBoxBeepPair);
            ComboBoxBeepPair_SelectedIndexChanged(sender, EventArgs.Empty);
        }

        private void ToolStripMenuItemAbout_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItemCloseProgram_Click(object sender, EventArgs e)
        {
            Application.Exit();
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
            _beepsController.SetVolumePercentage(beepVolume);
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
            _beepsController?.PlayBeep(BeepMediaPlayer, _beepsController.LowBeepPath);
        }

        private void BtnStartBeepTest_Click(object sender, EventArgs e)
        {
            _beepsController?.PlayBeep(BeepMediaPlayer, _beepsController.HighBeepPath);
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
                    isToCheckTogglePlaylistMode = currentModeIsShuffle ? false : true;
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
                _musicsController?.ChooseHowToClearPlaylist(MusicMediaPlayer, clearAllPlaylist);
            }
        }

        private void SetEnableMusicsControls(bool enable)
        {
            BtnAddMusics.Enabled = enable;
            TogglePlaylistMode.Enabled = enable;
            EnableControlsHavingItems(enable);
        }

        private void BtnAddMusics_Click(object sender, EventArgs e)
        {
            if (_musicsController?.AddMusicsToList(MusicMediaPlayer) ?? false)
                EnableControlsHavingItems(_musicsController?.HasValidMusics() ?? false);
        }

        private void BtnClearMusicsList_Click(object sender, EventArgs e)
        {
            if (_musicsController?.TryClearListViewMusics() ?? false)
            {
                if (ComboBoxProgramming.SelectedIndex > 0)
                    ComboBoxProgramming.SelectedIndex = 0;

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
            _musicsController?.TogglePlayMusicBySelectionCheckedChanged(
                TogglePlayMusicBySelection, ToolTip, MusicMediaPlayer, _competitionController.CanRunCompetition);
        }

        private void TogglePlaylistMode_CheckedChanged(object sender, EventArgs e)
        {
            _musicsController?.TogglePlaylistModeCheckedChanged(MusicMediaPlayer, TogglePlaylistMode, ToolTip);
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
            _musicsController.SetMinVolumePercentage(musicVolumeMin);
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
            _musicsController.SetMaxVolumePercentage(musicVolumeMax);
        }

        private void MusicMediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            //LblCompetitionTotalTime.Text = $"{(WMPPlayState)e.newState}";
            if (_competitionController.CompetitionProgramSetup != CompetitionProgram.MusicsAndBeeps ||
                !_competitionController.CanRunCompetition)
            {
                return;
            }

            if ((WMPPlayState)e.newState == WMPPlayState.wmppsReady)
            {
                //MessageBox.Show("wmppsReady");
                _recreatePlaylist = _musicsController?.IsTheLastPlaylistMusic() ?? false;
            }

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
            _musicsController.SetVolumePercentage(musicVolume);
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
            _competitionController?.ChangeInitializationProgramSelection(ComboBoxInitialization.SelectedIndex,
                MusicMediaPlayer);

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
                _musicsController.PrepareForPause(TBMusicCurrentVol);

                if (_competitionController.CanRunCompetition) // Check again in case of competition timeout
                    BtnStartCompetition.Text = _competitionController.ProgramIsRunning ? "Pausar" : "Retomar";
                return;
            }

            if (_competitionController.ValidateBeepsEventTimes(_beepsController) &&
                _competitionController.CanStartCompetition())
            {
                _setVisibleAndFocus = true;
                _competitionController.BlockListviewAndCreatePlaylist(MusicMediaPlayer, _musicsController);
                _competitionController.StartCompetition(out _finishCurrentIntervalCallback);

                SetEnableBeepsControls(false);
                SetEnableMusicsControls(false);
                TogglePlayMusicBySelection.Checked = false;
                DisableCompetitionControls();
                BtnStartCompetition.Text = "Pausar";
                BtnStopCompetition.Enabled = true;
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
            CheckEnableCompetitionControls();
            BtnStartCompetition.Text = "Iniciar";
            BtnStopCompetition.Enabled = false;
            ChooseHowToClearPlaylist(_competitionController.StopMusicsAtEnd);
        }
        #endregion
    }
}
