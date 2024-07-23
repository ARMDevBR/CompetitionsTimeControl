using CompetitionsTimeControl.Controllers;
using System.Text;
using System.Timers;
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

        private bool _lastMediaEnded;
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
            _lastMediaEnded = false;
            ConfigureBeepMediaPlayer();
            ConfigureMusicMediaPlayer();
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

        /*private void MusicMediaPlayer_PlayStateChange(object sender, _WMPOCXEvents_PlayStateChangeEvent e)
        {
            //if (e.newState == )
            if (MusicMediaPlayer.playState == WMPPlayState.wmppsMediaEnded)
            {
                //MessageBox.Show(MusicMediaPlayer.playState.ToString());
                //MusicMediaPlayer.Ctlcontrols.stop();
                _lastMediaEnded = true;
            }

            if (MusicMediaPlayer.playState == WMPPlayState.wmppsStopped)
            {
                //MessageBox.Show(MusicMediaPlayer.playState.ToString());
                MusicMediaPlayer.Ctlcontrols.previous();
                _lastMediaEnded = false;
                MusicMediaPlayer.Ctlcontrols.next();
                MusicMediaPlayer.Ctlcontrols.play();
            }
        }

        private void MusicMediaPlayer_MediaChange(object sender, AxWMPLib._WMPOCXEvents_MediaChangeEvent e)
        {
            if (_lastMediaEnded)
            {
                //MessageBox.Show(MusicMediaPlayer.playState.ToString());
                MusicMediaPlayer.Ctlcontrols.stop();
            }
            // Get an interface to the changed media item that is returned in the event data. 
            WMPLib.IWMPMedia3 changedItem = (WMPLib.IWMPMedia3)e.item;
            //MessageBox.Show(changedItem.name);
            // Display the name of the changed media item.
            LblTempStatus.Text = changedItem.name;
        }*/

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
                _competitionController = new(LblCompetitionTotalTime,
                    ProgressBarCurrentIntervalElapsed, LblIntervalsElapsed);

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
            _timer.Stop();
            _timer.Dispose();
            BeepMediaPlayer.close();
            MusicMediaPlayer.close();
            BeepMediaPlayer.Dispose();
            MusicMediaPlayer.Dispose();
        }

        private void Timer_Tick(object? source, ElapsedEventArgs e)
        {
            int currentMillisecond = e.SignalTime.Millisecond;

            int elapsedTime = currentMillisecond >= _lastMillisecond
                ? currentMillisecond - _lastMillisecond : 1000 + currentMillisecond - _lastMillisecond;

            _lastMillisecond = currentMillisecond;

            //elapsedTime = 16; // Teste de contador no debug.

            MusicMediaPlayer.fullScreen = false;
            MusicMediaPlayer.settings.volume = TBMusicCurrentVol.Value * -1;

            if (_beepsController == null || _musicsController == null || _competitionController == null)
                return;

            if (_beepsController.CanPerformBeeps)
            {
                _beepsController.TryPerformBeeps(BeepMediaPlayer, elapsedTime, LblTestMessages);

                if (!_competitionController.CanStartCompetition && !_beepsController.CanPerformBeeps)
                {
                    PrepareBeepTest(false);
                }
            }

            if (_competitionController.CanStartCompetition)
            {
                _competitionController.TryPerformCompetition(_beepsController, _musicsController, elapsedTime);

                if (_musicsController.SecondsToChangeVolume != null)
                {
                    _musicsController.TryChangeVolume(MusicMediaPlayer, TBMusicCurrentVol, TBMusicVolumeMin,
                        TBMusicVolumeMax, elapsedTime);
                }
            }
        }

        #region BEEPS CONTROLLER
        private void ComboBoxBeepPair_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enableButtons = _beepsController?.ChangeBeepPairSelection(ComboBoxBeepPair.SelectedIndex) ?? false;

            NumUDTimeForResumeMusics_ValueChanged(sender, e);
            SetEnableBeepsControls(enableButtons);
            ComboBoxProgramming.Enabled = enableButtons;
        }

        private void TBBeepVolume_ValueChanged(object sender, EventArgs e)
        {
            int beepVolume = TBBeepVolume.Value * -1;

            LblBeepVolumePercent.Text = $"{beepVolume}%";
            BeepMediaPlayer.settings.volume = beepVolume;
        }

        private void BtnConfigTest_Click(object sender, EventArgs e)
        {
            _beepsController.CanPerformBeeps = true;
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
        private void BtnAddMusics_Click(object sender, EventArgs e)
        {
            if (_musicsController?.AddMusicsToList(MusicMediaPlayer) ?? false)
                EnableControlsHavingItems(ListViewMusics.Items.Count > 0);
        }

        private void BtnClearMusicsList_Click(object sender, EventArgs e)
        {
            if (_musicsController?.TryClearListViewMusics() ?? false)
                EnableControlsHavingItems(false);
        }

        private void EnableControlsHavingItems(bool set)
        {
            if (!set)
                ToggleMarkAndExclude.Checked = false;

            BtnClearMusicsList.Enabled = set;
            ToggleMarkAndExclude.Enabled = set;
            TogglePlayMusicBySelection.Enabled = set;
            TogglePlayMusicBySelection.Checked = false;
            ListViewMusics.GridLines = set;
        }

        private void ToggleMarkAndExclude_CheckedChanged(object sender, EventArgs e)
        {
            _musicsController?.DeleteCheckedMusics(!ToggleMarkAndExclude.Checked);
            EnableControlsHavingItems(ListViewMusics.Items.Count > 0);
            TogglePlayMusicBySelection.Enabled = !ToggleMarkAndExclude.Checked && ListViewMusics.Items.Count > 0;
            _musicsController?.SetMarkAndExcludeTextAndToolTip(ToggleMarkAndExclude, ToolTip);
        }

        private void ListViewMusics_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            _musicsController?.ListViewMusicsItemChecked(e.Item);
            _musicsController?.SetMarkAndExcludeTextAndToolTip(ToggleMarkAndExclude, ToolTip);
        }

        private void ListViewMusics_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            _musicsController?.PlayPauseSelectedMusic(TogglePlayMusicBySelection.Checked, e.Item, MusicMediaPlayer);
        }

        private void TogglePlayMusicBySelection_CheckedChanged(object sender, EventArgs e)
        {
            _musicsController?.TogglePlayMusicBySelectionCheckedChanged(
                TogglePlayMusicBySelection, ToolTip, MusicMediaPlayer);
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
            bool enableMoreControls = false;

            if (_competitionController == null)
                return;

            _competitionController.ChangeCompetitionProgramSelection(ComboBoxProgramming.SelectedIndex);

            if (_competitionController.CompetitionProgramSetup == CompetitionController.CompetitionProgram.MusicsAndBeeps)
            {
                enableMoreControls = ListViewMusics.Items.Count > 0;

                if (!enableMoreControls)
                {
                    StringBuilder sb = new("Não há músicas válidas na lista de músicas.\n\n");
                    sb.Append("Selecione músicas para tocar se deseja esta opção para a competição.");

                    MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    ComboBoxProgramming.SelectedIndex = 0;
                }
            }

            ComboBoxInitialization.Enabled = enableMoreControls;
            ComboBoxRepeatPlaylist.Enabled = enableMoreControls;
            NumUDTimeToVolMin.Enabled = enableMoreControls;
            CheckBoxStopMusicsAtEnd.Enabled = enableMoreControls;

            CheckBoxStartWithBeeps.Enabled = true;
            NumUDCompetitionAmountIntervals.Enabled = true;
            NumUDCompetitionIntervalSeconds.Enabled = true;
            BtnStartCompetition.Enabled = true;
        }

        private void ComboBoxInitialization_SelectedIndexChanged(object sender, EventArgs e)
        {
            _competitionController?.ChangeInitializationProgramSelection(ComboBoxInitialization.SelectedIndex);
        }

        private void ComboBoxRepeatPlaylist_SelectedIndexChanged(object sender, EventArgs e)
        {
            _competitionController?.ChangePlaylistRestartSelection(ComboBoxRepeatPlaylist.SelectedIndex);
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

        private void BtnStartCompetition_Click(object sender, EventArgs e)
        {
            if (_competitionController?.StartCompetition() ?? false)
            {
                SetEnableBeepsControls(false);
            }
        }

        private void BtnStopCompetition_Click(object sender, EventArgs e)
        {
        }
        #endregion
    }
}
