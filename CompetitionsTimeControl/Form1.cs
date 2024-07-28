using CompetitionsTimeControl.Controllers;
using System.Text;
using System.Timers;
using WMPLib;
using static CompetitionsTimeControl.Controllers.CompetitionController;
using Timer = System.Timers.Timer;

namespace CompetitionsTimeControl
{
    public partial class MainForm : Form
    {
        public const string SupportedExtensions = "*.mp3; *.wma; *.wav";

        private const int MusicNameColumnWidth = 300;
        private const int MusicFormatColumnWidth = 75;
        private const int MusicDurationColumnWidth = 55;
        private const int MusicPathColumnWidth = 400;

        private Action? _finishCurrentIntervalCallback;
        private bool _recreatePlaylist;
        private bool _setVisibleAndFocus;
        private bool _closingApplication;
        private BeepsController _beepsController = null!;
        private CompetitionController _competitionController = null!;
        private MusicsController _musicsController = null!;
        private Timer _timer;
        private int _lastMillisecond;

        public MainForm()
        {
            _timer = new Timer(10);
            _timer.Elapsed += Timer_Tick;
            _timer.AutoReset = true;
            _timer.SynchronizingObject = this;
            _lastMillisecond = 0;

            InitializeComponent();
            ConfigureBeepMediaPlayer();
            ConfigureMusicMediaPlayer();
            _finishCurrentIntervalCallback = null;
            _recreatePlaylist = false;
            _closingApplication = false;
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
                _timer.Enabled = true;

                if (_beepsController != null)
                {
                    _beepsController.TimeBeforePlayBeeps = (float)NumUDTimeBeforePlayBeeps.Value;
                    _beepsController.AmountOfBeeps = (int)NumUDAmountOfBeeps.Value;
                    _beepsController.TimeForEachBeep = (float)NumUDTimeForEachBeep.Value;
                    _beepsController.SetTimeForResumeMusics(BeepMediaPlayer, (float)NumUDTimeForResumeMusics.Value);
                }
            }

            _musicsController ??= new(ListViewMusics);

            if (_competitionController == null)
            {
                _competitionController = new(LblCompetitionTotalTime, ProgressBarCurrentIntervalElapsed,
                    LblIntervalsElapsed, ProgressBarCompetitionElapsedTime, LblCompetitionElapsedTime);

                if (_competitionController != null)
                {
                    _competitionController.TimeToChangeVolume = (byte)NumUDTimeToVolMin.Value;
                    _competitionController.StartWithBeeps = CheckBoxStartWithBeeps.Checked;
                    _competitionController.StopMusicsAtEnd = CheckBoxStopMusicsAtEnd.Checked;
                    _competitionController.SetCompetitionAmountIntervals((byte)NumUDCompetitionAmountIntervals.Value);
                    _competitionController.SetCompetitionIntervalSeconds((int)NumUDCompetitionIntervalSeconds.Value);
                }
            }
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _closingApplication = true;
            _timer.Stop();
            _timer.Dispose();
            BeepMediaPlayer.close();
            MusicMediaPlayer.close();
            BeepMediaPlayer.Dispose();
            MusicMediaPlayer.Dispose();
        }

        private void Timer_Tick(object? source, ElapsedEventArgs e)
        {
            if (_closingApplication)
                return;

            int currentMillisecond = e.SignalTime.Millisecond;

            int elapsedTime = currentMillisecond >= _lastMillisecond
                ? currentMillisecond - _lastMillisecond : 1000 + currentMillisecond - _lastMillisecond;

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
                    _musicsController.AutoChangeToNextMusic(MusicMediaPlayer);
                }

                if (_recreatePlaylist)
                {
                    TryToRecreatePlaylist();
                    _recreatePlaylist = false;
                }
            }

            if (_beepsController.CanPerformBeepsEvent &&
                (_competitionController.CanRunCompetition ^ !_competitionController.ProgramIsRunning))
            {
                _beepsController.TryPerformBeeps(BeepMediaPlayer, elapsedTime, LblTestMessages,
                    _finishCurrentIntervalCallback);

                if (!_competitionController.CanRunCompetition && !_beepsController.CanPerformBeepsEvent)
                {
                    PrepareBeepTest(false);
                }
            }
        }

        #region STRIP MENU
        #endregion

        #region BEEPS CONTROLLER
        private void ComboBoxBeepPair_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enableButtons = _beepsController?.ChangeBeepPairSelection(ComboBoxBeepPair.SelectedIndex) ?? false;

            NumUDTimeForResumeMusics_ValueChanged(sender, e);
            SetEnableBeepsControls(enableButtons);
            ComboBoxProgramming.Enabled = enableButtons;
            ComboBoxBeepPair.Enabled = ComboBoxBeepPair.Items.Count > 0;
        }

        private void TBBeepVolume_ValueChanged(object sender, EventArgs e)
        {
            int beepVolume = TBBeepVolume.Value * -1;

            LblBeepVolumePercent.Text = $"{beepVolume}%";
            BeepMediaPlayer.settings.volume = beepVolume;
        }

        private void BtnConfigTest_Click(object sender, EventArgs e)
        {
            _beepsController.CanPerformBeepsEvent = true;
            PrepareBeepTest(true);
        }

        private void PrepareBeepTest(bool startTest)
        {
            SetEnableBeepsControls(!startTest);
            BtnConfigTest.Visible = !startTest;
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
            BtnConfigTest.Enabled = enable;
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
            _beepsController?.SetTimeForResumeMusics(BeepMediaPlayer, (float)NumUDTimeForResumeMusics.Value);
        }

        private void BtnConfigTest_MouseHover(object sender, EventArgs e)
        {
            string toolTipMessage = _beepsController?.GetBtnConfigTestToolTipMsg() ?? string.Empty;

            ToolTip.SetToolTip(BtnConfigTest, toolTipMessage);
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
                _musicsController?.ChooseHowToClearPlaylist(MusicMediaPlayer, clearAllPlaylist);
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
            TogglePlayMusicBySelection.Checked = false;
            ListViewMusics.GridLines = enable;
        }

        private void ToggleMarkAndExclude_CheckedChanged(object sender, EventArgs e)
        {
            _musicsController?.DeleteCheckedMusics(!ToggleMarkAndExclude.Checked);

            bool hasItemsInTheList = ListViewMusics.Items.Count > 0;
            bool hasValidMusics = _musicsController?.HasValidMusics() ?? false;

            if (ComboBoxProgramming.SelectedIndex > 0 && !hasValidMusics)
                ComboBoxProgramming.SelectedIndex = 0;

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

            LblMusicVolMinPercent.Text = $"{TBMusicVolumeMin.Value * -1}%";
        }

        private void TBMusicVolumeMax_ValueChanged(object sender, EventArgs e)
        {
            // Se volume máximo (barra é negativa) passar do volume atual, atualiza volume atual.
            if (TBMusicCurrentVol.Value < TBMusicVolumeMax.Value)
                TBMusicCurrentVol.Value = TBMusicVolumeMax.Value;

            // Se volume máximo (barra é negativa) passar do volume mínimo, atualiza volume mínimo.
            if (TBMusicVolumeMin.Value < TBMusicVolumeMax.Value)
                TBMusicVolumeMin.Value = TBMusicVolumeMax.Value;

            LblMusicVolMaxPercent.Text = $"{TBMusicVolumeMax.Value * -1}%";
        }

        private void MusicMediaPlayer_PlayStateChange(object sender, AxWMPLib._WMPOCXEvents_PlayStateChangeEvent e)
        {
            if (_competitionController.CompetitionProgramSetup != CompetitionProgram.MusicsAndBeeps ||
                !_competitionController.CanRunCompetition)
            {
                return;
            }

            if ((WMPPlayState)e.newState == WMPPlayState.wmppsReady)
                _recreatePlaylist = true;

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

            LblMusicCurrentVolPercent.Text = $"{TBMusicCurrentVol.Value * -1}%";
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
                enableMoreControls = _musicsController.HasValidMusics();

                if (!enableMoreControls)
                {
                    StringBuilder sb = new("Não há músicas válidas na lista de músicas (em vermelho são inválidas).\n\n");
                    sb.Append("Selecione músicas válidas para tocar se deseja esta opção para a competição.");

                    MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ComboBoxProgramming.SelectedIndex = 0;
                }
            }

            ComboBoxInitialization.Enabled = enableMoreControls;
            ComboBoxRepeatPlaylist.Enabled = enableMoreControls;
            NumUDTimeToVolMin.Enabled = enableMoreControls;
            CheckBoxStopMusicsAtEnd.Enabled = enableMoreControls;
            CheckBoxStartWithBeeps.Enabled = true;
            EnableControlsToStartCompetition(!enableMoreControls);
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

        private void EnableControlsToStartCompetition(bool forceEnable = false)
        {
            bool enableMoreControls = forceEnable ||
                (ComboBoxInitialization.SelectedIndex >= 0 && ComboBoxRepeatPlaylist.SelectedIndex >= 0);

            if (_competitionController != null && !_competitionController.CanRunCompetition)
            {
                NumUDCompetitionAmountIntervals.Enabled = enableMoreControls;
                NumUDCompetitionIntervalSeconds.Enabled = enableMoreControls;
            }
            BtnStartCompetition.Enabled = enableMoreControls;
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

                _competitionController.StartCompetition(MusicMediaPlayer, _musicsController,
                    out _finishCurrentIntervalCallback);

                SetEnableBeepsControls(false);
                SetEnableMusicsControls(false);
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
            SetEnableBeepsControls(true);
            SetEnableMusicsControls(true);
            ComboBoxProgramming.Enabled = true;
            CheckEnableCompetitionControls();
            BtnStartCompetition.Text = "Iniciar";
            BtnStopCompetition.Enabled = false;
            ChooseHowToClearPlaylist(_competitionController.StopMusicsAtEnd);
        }
        #endregion

        private void ToolStripMenuItemOpenConfiguration_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItemSaveConfiguration_Click(object sender, EventArgs e)
        {

        }

        private void ToolStripMenuItemUpdateBeepsList_Click(object sender, EventArgs e)
        {
            _beepsController?.GetBeepsSounds(ComboBoxBeepPair);
            ComboBoxBeepPair_SelectedIndexChanged(sender, EventArgs.Empty);
        }

        private void ToolStripMenuItemEnableTextAndButtonsTips_Click(object sender, EventArgs e)
        {
            ToolStripMenuItemEnableTextAndButtonsTips.Checked = !ToolStripMenuItemEnableTextAndButtonsTips.Checked;
            ToolTip.Active = ToolStripMenuItemEnableTextAndButtonsTips.Checked;
        }

        private void ToolStripMenuItemCloseProgram_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
