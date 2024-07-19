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

        private bool _canPerformBeeps;
        private bool _lastMediaEnded;
        private BeepsController _beepsController = null!;
        private MusicsController _musicsController = null!;
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
                _beepsController = new(ComboBoxBeepPair, LblTestMessages, BeepMediaPlayer);
                _timer.Enabled = true;

                if (_beepsController != null)
                {
                    _beepsController.TimeBeforePlayBeeps = (float)NumUDTimeBeforePlayBeeps.Value;
                    _beepsController.AmountOfBeeps = (int)NumUDAmountOfBeeps.Value;
                    _beepsController.TimeForEachBeep = (float)NumUDTimeForEachBeep.Value;
                    _beepsController.SetTimeForResumeMusics(BeepMediaPlayer, (float)NumUDTimeForResumeMusics.Value);
                }
            }

            if (_musicsController == null)
            {
                _musicsController = new(ListViewMusics);

                if (_musicsController != null)
                {
                    _musicsController.GenerateListHeaders();
                }
            }
        }

        private void BtnPlayTest_Click(object sender, EventArgs e)
        {
            // Toca uma música da playlist do media player pelo indice
            /*IWMPMedia media = MusicMediaPlayer.currentPlaylist.Item[2];
            MusicMediaPlayer.Ctlcontrols.playItem(media);
            MessageBox.Show(media.sourceURL);*/

            // Seleciona item na lista
            //ListViewMusics.Items[2].Selected = true;
            ListViewItem? lvi = ListViewMusics.FindItemWithText(
                Path.GetFileNameWithoutExtension(MusicMediaPlayer.currentMedia.sourceURL));

            if (lvi == null)
                return;

            lvi.Selected = true;
            lvi.EnsureVisible();
            ListViewMusics.Select();
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

            _beepsController?.TryPerformBeeps(BeepMediaPlayer, _canPerformBeeps, elapsedTime, out keepPerformingBeeps,
                LblTestMessages);

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
            SetEnableBeepsControls(enableButtons);
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
            _beepsController?.SetTimeForResumeMusics(BeepMediaPlayer, (float)NumUDTimeForResumeMusics.Value);
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

        private void BtnConfigTest_MouseHover(object sender, EventArgs e)
        {
            string toolTipMessage = _beepsController?.GetBtnConfigTestToolTipMsg() ?? string.Empty;

            ToolTip.SetToolTip(BtnConfigTest, toolTipMessage);
        }

        private void BtnAddMusics_Click(object sender, EventArgs e)
        {
            if (_musicsController.AddMusicsToList(MusicMediaPlayer))
                EnableControlsHavingItems(ListViewMusics.Items.Count > 0);
        }

        private void BtnClearMusicsList_Click(object sender, EventArgs e)
        {
            StringBuilder sb = new("Todas músicas serão excluídas da lista!\n\n");
            sb.Append("Deseja continuar com a operação?");

            if (MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return;

            EnableControlsHavingItems(false);
        }

        private void EnableControlsHavingItems(bool set)
        {
            if (!set)
            {
                _checkedMusicToDelete.Clear();
                ListViewMusics.Items.Clear();
                ListViewMusics.CheckBoxes = false;
                ToggleMarkAndExclude.Checked = false;
            }

            BtnClearMusicsList.Enabled = set;
            ToggleMarkAndExclude.Enabled = set;
            TogglePlayMusicBySelection.Enabled = set;
            TogglePlayMusicBySelection.Checked = false;
            ListViewMusics.GridLines = set;
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
                sb.Append("\nDeseja continuar com a operação?");

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
                    int[] musicToUncheck = [.. _checkedMusicToDelete]; //_checkedMusicToDelete.ToArray();

                    for (int i = musicToUncheck.Length - 1; i >= 0; i--)
                    {
                        ListViewMusics.Items[musicToUncheck[i]].Checked = false;
                    }
                }
            }

            EnableControlsHavingItems(ListViewMusics.Items.Count > 0);
            TogglePlayMusicBySelection.Enabled = !ToggleMarkAndExclude.Checked && ListViewMusics.Items.Count > 0;
            ListViewMusics.CheckBoxes = ToggleMarkAndExclude.Checked;
            SetMarkAndExcludeTextAndToolTip();
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

        private void ListViewMusics_ItemChecked(object sender, ItemCheckedEventArgs e)
        {
            _musicsController?.ListViewMusicsItemChecked(e.Item);
            /*if (e.Item.Checked)
                _checkedMusicToDelete.Add(e.Item.Index);
            else if (_checkedMusicToDelete.Contains(e.Item.Index))
                _ = _checkedMusicToDelete.Remove(e.Item.Index);*/

            SetMarkAndExcludeTextAndToolTip();
        }

        private void ListViewMusics_ItemSelectionChanged(object sender, ListViewItemSelectionChangedEventArgs e)
        {
            ListViewItem? lvi = e.Item;

            if (TogglePlayMusicBySelection.Checked && ListViewMusics.SelectedItems.Count > 0 && lvi != null)
            {
                ListViewItem.ListViewSubItemCollection? listviewSubItemColl = lvi.SubItems;

                TryPlayMusicBySelectionAndEnable(listviewSubItemColl);
            }
            else
            {
                PauseMusicBySelectionAndDisable();
            }
        }

        private void TryPlayMusicBySelectionAndEnable(ListViewItem.ListViewSubItemCollection? listviewSubItemColl)
        {
            if (listviewSubItemColl == null)
                return;

            string music = listviewSubItemColl[0].Text;
            string format = listviewSubItemColl[1].Text;
            string path = listviewSubItemColl[3].Text;

            if (!string.Equals(MusicMediaPlayer.URL, $@"{path}\{music}.{format}"))
                MusicMediaPlayer.URL = $@"{path}\{music}.{format}";

            MusicMediaPlayer.Ctlcontrols.play();
            MusicMediaPlayer.Ctlenabled = true;
        }

        private void PauseMusicBySelectionAndDisable()
        {
            MusicMediaPlayer.Ctlcontrols.pause();
            MusicMediaPlayer.Ctlenabled = false;
        }

        private void CreatePlaylist(string url)
        {
            MusicMediaPlayer.currentPlaylist.
                appendItem(MusicMediaPlayer.newMedia(url));
        }

        private void TogglePlayMusicBySelection_CheckedChanged(object sender, EventArgs e)
        {
            if (TogglePlayMusicBySelection.Checked)
            {
                if (ListViewMusics.SelectedItems.Count > 0)
                {
                    TryPlayMusicBySelectionAndEnable(ListViewMusics.SelectedItems[0].SubItems);
                }

                TogglePlayMusicBySelection.Text = "Cancelar";
                ToolTip.SetToolTip(TogglePlayMusicBySelection, "Desabilita tocar a música ao selecioná-la na lista.");
            }
            else
            {
                MusicMediaPlayer.Ctlenabled = false;
                MusicMediaPlayer.currentPlaylist.clear();
                TogglePlayMusicBySelection.Text = "Selecionar e ouvir";
                ToolTip.SetToolTip(TogglePlayMusicBySelection, "Habilita tocar a música ao selecioná-la na lista.");
            }
        }

        private void TogglePlaylistMode_CheckedChanged(object sender, EventArgs e)
        {
            MusicMediaPlayer.settings.setMode("shuffle", TogglePlaylistMode.Checked);
            //MusicMediaPlayer.settings.setMode("loop", TogglePlaylistMode.Checked); // Ver se é loop na playlist

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
