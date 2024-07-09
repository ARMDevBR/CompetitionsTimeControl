using AxWMPLib;
using System.Numerics;
using System.Security.Policy;
using System.Timers;
using WMPLib;
using Timer = System.Timers.Timer;

namespace CompetitionsTimeControl
{
    public partial class MainForm : Form
    {
        public const string SupportedExtensions = "*.mp3; *.wma; *.wav";

        private bool _canPerformBeeps;
        private bool _lastMediaEnded;
        private BeepsController _beepsController = null!;
        private Timer _timer;
        private int _lastMillisecond;
        int test;

        public MainForm()
        {
            _timer = new Timer(10);
            _timer.Elapsed += Timer_Tick;
            _timer.AutoReset = true;
            _timer.SynchronizingObject = this;
            _lastMillisecond = 0;

            _canPerformBeeps = false;
            InitializeComponent();
            ConfigureTooltip();
            _lastMediaEnded = false;
            ConfigureBeepMediaPlayer();
            ConfigureMusicMediaPlayer();
        }

        private void ConfigureBeepMediaPlayer()
        {
            BeepMediaPlayer.settings.autoStart = false;
            BeepMediaPlayer.uiMode = "none";
            BeepMediaPlayer.enableContextMenu = false;
        }

        private void ConfigureMusicMediaPlayer()
        {
            MusicMediaPlayer.settings.autoStart = false;
            //MusicMediaPlayer.settings.setMode("shuffle", true); // Random playlist
            MusicMediaPlayer.uiMode = "full";
            MusicMediaPlayer.enableContextMenu = false;
        }

        private void ConfigureTooltip()
        {
            //ToolTip.SetToolTip(LblBeepPair, "Define o par dos beeps (tonalidade) a tocar.");
            //ToolTip.SetToolTip(LblTimeBeforePlayBeeps, "Tempo antes dos beeps e após volume baixo das músicas.");
            //ToolTip.SetToolTip(LblAmountOfBeeps, "Quantidade de beeps (incluindo o último beep de início da prova).");
            //ToolTip.SetToolTip(LblTimeForEachBeep, "Tempo em segundos para tocar outro beep de contagem.");
            //ToolTip.SetToolTip(LblTimeForResumeMusics, "Tempo após último beep para começar a retomar volume alto das músicas.");

            //ToolTip.SetToolTip(LblMusicVolMax, "Define o volume enquanto não há beeps e apenas as músicas estão tocando.");
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
                _beepsController = new(ComboBoxBeepPair, LblTestMessages, BeepMediaPlayer);
                _timer.Enabled = true;

                if (_beepsController != null)
                {
                    _beepsController.TimeBeforePlayBeeps = (float)NumUDTimeBeforePlayBeeps.Value;
                    _beepsController.AmountOfBeeps = (int)NumUDAmountOfBeeps.Value;
                    _beepsController.TimeForEachBeep = (float)NumUDTimeForEachBeep.Value;
                    _beepsController.SetTimeForResumeMusics((float)NumUDTimeForResumeMusics.Value);
                }
            }
        }

        private void BtnPlayTest_Click(object sender, EventArgs e)
        {
            IWMPMedia media;

            var PlayListToRun = MusicMediaPlayer.currentPlaylist;

            media = MusicMediaPlayer.newMedia(@"D:\Users\ademi\Music\Pop-Dance antigas\All That She Wants.mp3");
            PlayListToRun.appendItem(media);
            media = MusicMediaPlayer.newMedia(@"D:\Users\ademi\Music\Pop-Dance antigas\5_Nina-TheReasonIsYou.wma");
            PlayListToRun.appendItem(media);
            media = MusicMediaPlayer.newMedia(@"D:\Users\ademi\Music\Pop-Dance antigas\Dr. Alban - It´s My Life.mp3");
            PlayListToRun.appendItem(media);
            //MusicMediaPlayer.currentPlaylist = PlayListToRun;

            //MusicMediaPlayer.Ctlcontrols.play();
            //MusicMediaPlayer.URL = @"D:\Users\ademi\Music\Pop-Dance antigas\5_Nina-TheReasonIsYou.wma";
        }

        private void BtnStopTest_Click(object sender, EventArgs e)
        {
            MusicMediaPlayer.Ctlcontrols.stop();
            //MusicMediaPlayer.Ctlcontrols.next();
        }

        //private void Timer_Tick(object sender, EventArgs e)
        private void Timer_Tick(object? source, ElapsedEventArgs e)
        {
            int currentMillisecond = e.SignalTime.Millisecond;

            int elapsedTime = currentMillisecond >= _lastMillisecond
                ? currentMillisecond - _lastMillisecond : 1000 + currentMillisecond - _lastMillisecond;

            _lastMillisecond = currentMillisecond;

            bool keepPerformingBeeps = false;

            MusicMediaPlayer.settings.volume = TBMusicCurrentVol.Value * -1;

            _beepsController?.TryPerformBeeps(_canPerformBeeps, elapsedTime, out keepPerformingBeeps);

            if (_canPerformBeeps && !keepPerformingBeeps)
            {
                LblTempStatus.Text = "Acabou!";
            }

            _canPerformBeeps = keepPerformingBeeps;
        }

        private void ComboBoxBeepPair_SelectedIndexChanged(object sender, EventArgs e)
        {
            bool enableButtons = _beepsController?.ChangeBeepPairSelection(ComboBoxBeepPair.SelectedIndex) ?? false;

            NumUDTimeForResumeMusics_ValueChanged(sender, e);

            BtnConfigTest.Enabled = enableButtons;
            BtnCountdownBeepTest.Enabled = enableButtons;
            BtnStartBeepTest.Enabled = enableButtons;
        }

        private void TBBeepVolume_ValueChanged(object sender, EventArgs e)
        {
            int beepVolume = TBBeepVolume.Value * -1;

            LblBeepVolumePercent.Text = $"{beepVolume}%";
            BeepMediaPlayer.settings.volume = beepVolume;
        }

        private void BtnConfigTest_Click(object sender, EventArgs e)
        {
            PrepareBeepTest();
            /*if (_beepCount > 0)
            {
                _beepCount--;
                BeepMediaPlayer.URL = _beepsController?.LowBeepPath;
            }
            else
            {
                _beepCount = 4;
                BeepMediaPlayer.URL = _beepsController?.HighBeepPath;
            }*/
        }

        private void PrepareBeepTest()
        {
            _canPerformBeeps = true;
            SetEnableBeepsControls(false);
            BtnConfigTest.Visible = false;
            BtnCountdownBeepTest.Visible = false;
            BtnStartBeepTest.Visible = false;
            LblTestMessages.Visible = true;
        }

        private void SetEnableBeepsControls(bool enable)
        {
            ComboBoxBeepPair.Enabled = enable;
            NumUDTimeBeforePlayBeeps.Enabled = enable;
            NumUDAmountOfBeeps.Enabled = enable;
            NumUDTimeForEachBeep.Enabled = enable;
            NumUDTimeForResumeMusics.Enabled = enable;
        }

        private void BtnCountdownBeepTest_Click(object sender, EventArgs e)
        {
            BeepMediaPlayer.Ctlcontrols.stop();
            BeepMediaPlayer.URL = _beepsController?.LowBeepPath;
            BeepMediaPlayer.Ctlcontrols.play();
        }

        private void BtnStartBeepTest_Click(object sender, EventArgs e)
        {
            BeepMediaPlayer.Ctlcontrols.stop();
            BeepMediaPlayer.URL = _beepsController?.HighBeepPath;
            BeepMediaPlayer.Ctlcontrols.play();
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
            _beepsController?.SetTimeForResumeMusics((float)NumUDTimeForResumeMusics.Value);
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _timer.Stop();
            _timer.Dispose();
        }
    }
}
