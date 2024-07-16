using AxWMPLib;
using System.Numerics;
using System.Security.Policy;
using System.Text;
using System.Timers;
using WMPLib;
using static System.Net.Mime.MediaTypeNames;
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

        private bool _canPerformBeeps;
        private bool _lastMediaEnded;
        private BeepsController _beepsController = null!;
        private List<int> _checkedMusicToDelete;
        private Timer _timer;
        private int _lastMillisecond;

        public MainForm()
        {
            _checkedMusicToDelete = [];
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
            GenerateList();
            FillList();
        }

        private void GenerateList()
        {
            ListViewMusics.Columns.Add("MÚSICA", MusicNameColumnWidth, HorizontalAlignment.Left);
            ListViewMusics.Columns.Add("FORMATO", MusicFormatColumnWidth, HorizontalAlignment.Center);
            ListViewMusics.Columns.Add("TEMPO", MusicDurationColumnWidth, HorizontalAlignment.Center);
            ListViewMusics.Columns.Add("LOCAL", MusicPathColumnWidth, HorizontalAlignment.Left);
            ListViewMusics.View = View.Details;
            ListViewMusics.FullRowSelect = true;
            ListViewMusics.MultiSelect = false;
        }

        private void FillList()
        {
            ListViewMusics.GridLines = true;

            ListViewItem listViewItem = new(["5_Nina-TheReasonIsYou", "wma", "03:58", @"D:\Users\ademi\Music\Pop-Dance antigas"])
            {
                BackColor = Color.Beige
            };

            ListViewMusics.Items.Add(listViewItem);

            listViewItem = new ListViewItem(["a_Corona-IDon_tWannaBeAStar", "mp3", "03:58", @"D:\Users\ademi\Music\Pop-Dance antigas"]);
            listViewItem.BackColor = Color.Azure;

            ListViewMusics.Items.Add(listViewItem);

            listViewItem = new ListViewItem(["All That She Wants", "mp3", "03:30", @"D:\Users\ademi\Music\Pop-Dance antigas"]);
            listViewItem.BackColor = Color.Beige;

            ListViewMusics.Items.Add(listViewItem);

            listViewItem = new ListViewItem(["b_Co.Ro feat. Taleesa - Because the night", "mp3", "04:03", @"D:\Users\ademi\Music\Pop-Dance antigas"]);
            listViewItem.BackColor = Color.Azure;

            ListViewMusics.Items.Add(listViewItem);
        }

        private void ConfigureBeepMediaPlayer()
        {
            BeepMediaPlayer.settings.autoStart = true;
            BeepMediaPlayer.uiMode = "none";
            BeepMediaPlayer.enableContextMenu = false;
        }

        private void ConfigureMusicMediaPlayer()
        {
            MusicMediaPlayer.settings.autoStart = false;
            MusicMediaPlayer.uiMode = "full";
            MusicMediaPlayer.enableContextMenu = false;
        }

        private void ConfigureTooltip()
        {
            //BtnConfigTest // Configurar tip dinâmico para obter valores dos tempos.
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

            //elapsedTime = 16; // Teste de contador no debug.

            bool keepPerformingBeeps = false;

            _beepsController?.TryPerformBeeps(_canPerformBeeps, elapsedTime, out keepPerformingBeeps);

            MusicMediaPlayer.settings.volume = TBMusicCurrentVol.Value * -1;

            if (_canPerformBeeps && !keepPerformingBeeps)
            {
                PrepareBeepTest(false);
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
            _canPerformBeeps = true;
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
        }

        private void BtnCountdownBeepTest_Click(object sender, EventArgs e)
        {
            _beepsController?.PlayBeep(_beepsController.LowBeepPath);
        }

        private void BtnStartBeepTest_Click(object sender, EventArgs e)
        {
            _beepsController?.PlayBeep(_beepsController.HighBeepPath);
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
            BeepMediaPlayer.Ctlcontrols.stop();
            MusicMediaPlayer.Ctlcontrols.stop();
            BeepMediaPlayer.Dispose();
            MusicMediaPlayer.Dispose();
        }

        private void BtnConfigTest_MouseHover(object sender, EventArgs e)
        {
            string toolTipMessage = _beepsController?.GetBtnConfigTestToolTipMsg() ?? string.Empty;

            ToolTip.SetToolTip(BtnConfigTest, toolTipMessage);
        }

        private void ListViewMusics_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            if (e.Item.Checked)
                _checkedMusicToDelete.Add(e.Item.Index);
            else if (_checkedMusicToDelete.Contains(e.Item.Index))
                _ = _checkedMusicToDelete.Remove(e.Item.Index);

            SetMarkAndExcludeTextAndToolTip();
        }

        private void BtnAddMusics_Click(object sender, EventArgs e) // Simulando delete
        {
        }

        private void BtnClearMusicsList_Click(object sender, EventArgs e)
        {
            ToggleMarkAndExclude.Checked = false;
            ToggleMarkAndExclude.Enabled = false;
            ListViewMusics.CheckBoxes = false;
            ListViewMusics.GridLines = false;
            ListViewMusics.Items.Clear();
        }

        private void ToggleMarkAndExclude_CheckedChanged(object sender, EventArgs e)
        {
            if (!ToggleMarkAndExclude.Checked && _checkedMusicToDelete.Count > 0)
            {
                StringBuilder sb = new("As seguintes músicas serão excluídas da lista:\n\n");
                _checkedMusicToDelete.Sort();

                for (int i = 0; i < _checkedMusicToDelete.Count; i++)
                {
                    sb.AppendLine($"  ➤  {ListViewMusics.Items[_checkedMusicToDelete[i]].Text}");
                }
                sb.AppendLine("\nDeseja continuar com a operação?");

                if (MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.YesNo,
                    MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    for (int i = _checkedMusicToDelete.Count - 1; i >= 0; i--)
                    {
                        ListViewMusics.Items.RemoveAt(_checkedMusicToDelete[i]);
                        _checkedMusicToDelete.RemoveAt(i);
                    }

                    RepaintListViewGrid();
                }
                else
                {
                    int[] musicToUncheck = _checkedMusicToDelete.ToArray();

                    for (int i = musicToUncheck.Length - 1; i >= 0; i--)
                    {
                        ListViewMusics.Items[musicToUncheck[i]].Checked = false;
                    }
                }
            }

            ListViewMusics.CheckBoxes = ToggleMarkAndExclude.Checked;
            SetMarkAndExcludeTextAndToolTip();

            ToggleMarkAndExclude.Enabled = ListViewMusics.Items.Count > 0;
            ListViewMusics.GridLines = ListViewMusics.Items.Count > 0;
        }

        private void RepaintListViewGrid()
        {
            var items = ListViewMusics.Items;

            foreach (ListViewItem item in items)
                item.BackColor = (item.Index & 1) > 0 ? Color.Azure : Color.Beige;
        }

        private void SetMarkAndExcludeTextAndToolTip()
        {
            if (ToggleMarkAndExclude.Checked)
            {
                if (_checkedMusicToDelete.Count > 0)
                {
                    ToggleMarkAndExclude.Text = "Excluir marcadas";

                    ToolTip.SetToolTip(ToggleMarkAndExclude, string.Concat(
                        "- Ao clicar novamente, as músicas com checkbox marcado serão excluídas da lista.\n",
                        "- Desmarque todas as músicas que não queira excluir da lista."));
                }
                else
                {
                    ToggleMarkAndExclude.Text = "Selecione músicas";

                    ToolTip.SetToolTip(ToggleMarkAndExclude, string.Concat(
                        "- Ao clicar novamente, a função de exclusão individual será cancelada.\n",
                        "- Marque o checkbox das músicas que deseja excluir da lista."));
                }
            }
            else
            {
                ToggleMarkAndExclude.Text = "Marque e exclua";
                ToolTip.SetToolTip(ToggleMarkAndExclude, "Clique para ativar a função de exclusão das músicas na lista.");
            }
        }
        string test;
        private void ListViewMusics_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListViewItem? lvi = e.Item;
            ListViewItem.ListViewSubItemCollection? si = lvi?.SubItems;
            //si.

            if (true && lvi != null)
            {
                MessageBox.Show(lvi.Text);

                MessageBox.Show(lvi.SubItems[0].Text);
            }
        }

        private void TogglePlaylistMode_CheckedChanged(object sender, EventArgs e)
        {
            MusicMediaPlayer.settings.setMode("shuffle", TogglePlaylistMode.Checked);

            if (TogglePlaylistMode.Checked)
            {
                TogglePlaylistMode.Text = "Lista aleatória";
                ToolTip.SetToolTip(TogglePlaylistMode, "Playlist vai tocar em ordem aleatória.");
            }
            else
            {
                TogglePlaylistMode.Text = "Lista sequencial";
                ToolTip.SetToolTip(TogglePlaylistMode, "Playlist vai tocar sequencialmente.");
            }
        }

        private void ToggleSeeDetails_CheckedChanged(object sender, EventArgs e)
        {
            if (ToggleSeeDetails.Checked)
            {
                ListViewMusics.Columns[0].Width = MusicNameColumnWidth;
                ListViewMusics.Columns[1].Width = MusicFormatColumnWidth;
                ListViewMusics.Columns[2].Width = MusicDurationColumnWidth;
                ListViewMusics.Columns[3].Width = MusicPathColumnWidth;
                ToggleSeeDetails.Text = "Ver simplificada";
            }
            else
            {
                ListViewMusics.Columns[0].Width = MusicNameColumnWidth + MusicFormatColumnWidth + MusicFormatColumnWidth;
                ListViewMusics.Columns[0].Width -= 20;
                ListViewMusics.Columns[1].Width = 0;
                ListViewMusics.Columns[2].Width = 0;
                ListViewMusics.Columns[3].Width = 0;
                ToggleSeeDetails.Text = "Ver detalhada";
            }
        }
    }
}
