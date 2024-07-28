using AxWMPLib;
using System.Text;
using WMPLib;

namespace CompetitionsTimeControl.Controllers
{
    internal class MusicsController
    {
        private const int MusicNameColumnWidth = 300;
        private const int MusicFormatColumnWidth = 75;
        private const int MusicDurationColumnWidth = 55;
        private const int MusicPathColumnWidth = 400;
        private const byte LoopsToKeepVisibleAndFocus = 4;
        private const byte AmountOfMusicsToAutoChangePlaylist = 2;

        public enum ChangeVolume { ToMin, ToMax }

        public byte? SecondsToChangeVolume { get; private set; }

        private readonly ListView _listViewMusics;

        private bool _stepsHaveBeenConfigured;
        private bool _playlistInRandomMode;
        private float _currentMusicVolume;
        private int _timerChangeVolumeInMilliSec;
        private int _counterSetVisibleAndFocus;
        private float _stepsToChangeMusic;
        private ChangeVolume _changeVolume;
        private List<int> _checkedMusicToDelete;
        private IWMPMedia _lastPlaylistMedia = null!;

        public MusicsController(ListView listViewMusics)
        {
            _stepsHaveBeenConfigured = false;
            _stepsToChangeMusic = 0;
            _checkedMusicToDelete = [];
            _listViewMusics = listViewMusics;
            SecondsToChangeVolume = null;
            GenerateListHeaders();
            _counterSetVisibleAndFocus = LoopsToKeepVisibleAndFocus;
        }

        public void GenerateListHeaders()
        {
            if (_listViewMusics == null)
                return;

            _listViewMusics.Columns.Add("MÚSICA", MusicNameColumnWidth, HorizontalAlignment.Left);
            _listViewMusics.Columns.Add("FORMATO", MusicFormatColumnWidth, HorizontalAlignment.Center);
            _listViewMusics.Columns.Add("TEMPO", MusicDurationColumnWidth, HorizontalAlignment.Center);
            _listViewMusics.Columns.Add("LOCAL", MusicPathColumnWidth, HorizontalAlignment.Left);
            _listViewMusics.View = View.Details;
            _listViewMusics.FullRowSelect = true;
            _listViewMusics.MultiSelect = false;
        }

        public bool AddMusicsToList(AxWindowsMediaPlayer musicMediaPlayer)
        {
            StringBuilder? sb = null;

            OpenFileDialog openFileDialog = new()
            {
                Filter = $"Arquivos de som | {MainForm.SupportedExtensions}",
                Multiselect = true
            };

            openFileDialog.ShowDialog();

            if (_listViewMusics == null || openFileDialog.FileNames == null || openFileDialog.FileNames.Length <= 0)
                return false;

            _listViewMusics.GridLines = true;
            int index = _listViewMusics.Items.Count;

            foreach (string pathFileName in openFileDialog.FileNames)
            {
                string nameWithFormat = Path.GetFileName(pathFileName);
                string musicName = nameWithFormat[..^4];
                string musicFormat = nameWithFormat[^3..];
                string mediaDurationStr = musicMediaPlayer.newMedia(pathFileName).durationString;
                string path = Path.GetDirectoryName(pathFileName) ?? "";

                if (_listViewMusics.FindItemWithText(musicName) != null)
                {
                    sb ??= new("As seguintes músicas já existem na lista e não foram incluídas novamente:\n\n");

                    sb?.AppendLine($"  ➤  {musicName}");
                    continue;
                }

                ListViewItem listViewItem = new([musicName, musicFormat, mediaDurationStr, path])
                {
                    BackColor = (index++ & 1) > 0 ? Color.Azure : Color.Beige
                };

                if (index is 2 or 4)
                {
                    listViewItem.ForeColor = Color.Red; // Test for bad music in the list
                }

                _listViewMusics.Items.Add(listViewItem);
            }

            if (sb != null)
            {
                sb.Append("\nVerifique a última pasta selecionada e renomeie as músicas se necessário.");

                MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return true;
        }

        public bool TryClearListViewMusics()
        {
            StringBuilder sb = new("Todas músicas serão excluídas da lista!\n\n");
            sb.Append("Deseja continuar com a operação?");

            if (MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return false;

            ClearCheckedMusicToDelete();
            _listViewMusics.Items.Clear();
            _listViewMusics.CheckBoxes = false;
            return true;
        }

        public void ClearCheckedMusicToDelete() => _checkedMusicToDelete.Clear();

        public void SetMarkAndExcludeTextAndToolTip(CheckBox toggleMarkAndExclude, ToolTip toolTip)
        {
            _listViewMusics.CheckBoxes = toggleMarkAndExclude.Checked;

            if (toggleMarkAndExclude.Checked)
            {
                if (_checkedMusicToDelete.Count > 0)
                {
                    toggleMarkAndExclude.Text = "Excluir marcadas";

                    toolTip.SetToolTip(toggleMarkAndExclude, string.Concat(
                        "- Ao clicar novamente, as músicas com checkbox marcado serão excluídas da lista.\n",
                        "- Desmarque todas as músicas que não queira excluir da lista."));
                }
                else
                {
                    toggleMarkAndExclude.Text = "Selecione músicas";

                    toolTip.SetToolTip(toggleMarkAndExclude, string.Concat(
                        "- Ao clicar novamente, a função de exclusão individual será cancelada.\n",
                        "- Marque o checkbox das músicas que deseja excluir da lista."));
                }
            }
            else
            {
                toggleMarkAndExclude.Text = "Marque e exclua";
                toolTip.SetToolTip(toggleMarkAndExclude, "Clique para ativar a função de exclusão das músicas na lista.");
            }
        }

        public void DeleteCheckedMusics(bool canDelete)
        {
            if (!canDelete || _checkedMusicToDelete.Count <= 0)
            {
                _checkedMusicToDelete.Clear();
                return;
            }

            StringBuilder sb = new("As seguintes músicas serão excluídas da lista:\n\n");
            _checkedMusicToDelete.Sort();

            for (int i = 0; i < _checkedMusicToDelete.Count; i++)
            {
                sb.AppendLine($"  ➤  {_listViewMusics.Items[_checkedMusicToDelete[i]].Text}");
            }
            sb.Append("\nDeseja continuar com a operação?");

            if (MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                for (int i = _checkedMusicToDelete.Count - 1; i >= 0; i--)
                {
                    _listViewMusics.Items.RemoveAt(_checkedMusicToDelete[i]);
                    _checkedMusicToDelete.RemoveAt(i);
                }

                RepaintListViewGrid(true);
            }
            else
            {
                int[] musicToUncheck = [.. _checkedMusicToDelete]; //_checkedMusicToDelete.ToArray();

                for (int i = musicToUncheck.Length - 1; i >= 0; i--)
                {
                    _listViewMusics.Items[musicToUncheck[i]].Checked = false;
                }
            }
        }

        public void RepaintListViewGrid(bool colorsForEnabledList)
        {
            var items = _listViewMusics.Items;

            foreach (ListViewItem item in items)
            {
                if (colorsForEnabledList)
                    item.BackColor = (item.Index & 1) > 0 ? Color.Azure : Color.Beige;
                else
                    item.BackColor = Color.LightGray;
            }
        }

        public void ListViewMusicsItemChecked(in ListViewItem item)
        {
            if (item.Checked)
                _checkedMusicToDelete.Add(item.Index);
            else if (_checkedMusicToDelete.Contains(item.Index))
                _ = _checkedMusicToDelete.Remove(item.Index);
        }

        public void PlayPauseSelectedMusic(bool canPlayPause, ListViewItem? lvi, AxWindowsMediaPlayer musicMediaPlayer)
        {
            if (!canPlayPause)
                return;

            if (lvi != null && lvi.Selected)
            {
                if (lvi.ForeColor == Color.Red)
                {
                    ShowInvalidMusicMessage();
                }
                else if (_listViewMusics.SelectedItems.Count > 0)
                {
                    ListViewItem.ListViewSubItemCollection? listviewSubItemColl = lvi.SubItems;

                    TryPlayMusicBySelectionAndEnable(listviewSubItemColl, musicMediaPlayer);
                }
            }
            else
            {
                musicMediaPlayer.Ctlcontrols.pause();
                musicMediaPlayer.Ctlenabled = false;
            }
        }

        private void ShowInvalidMusicMessage()
        {
            StringBuilder sb = new("Esta música pode estar corrompida ou não existir mais na origem.\n\n");
            sb.Append("As músicas listadas em vermelho NÃO são válidas e podem ser excluídas da lista.");

            MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        public bool HasValidMusics()
        {
            ListView.ListViewItemCollection listViewItemColl = _listViewMusics.Items;
            
            for (int i = 0; i < listViewItemColl.Count; i++)
            {
                if (listViewItemColl[i].ForeColor == SystemColors.WindowText)
                    return true;
            }
            return false;
        }

        private void TryPlayMusicBySelectionAndEnable(ListViewItem.ListViewSubItemCollection? listviewSubItemColl,
            AxWindowsMediaPlayer musicMediaPlayer, bool forceRestartMusic = false)
        {
            if (listviewSubItemColl == null)
                return;

            string music = listviewSubItemColl[0].Text;
            string format = listviewSubItemColl[1].Text;
            string path = listviewSubItemColl[3].Text;

            forceRestartMusic = forceRestartMusic || musicMediaPlayer.playState == WMPPlayState.wmppsUndefined;

            if (!string.Equals(musicMediaPlayer.URL, $@"{path}\{music}.{format}") || forceRestartMusic)
                musicMediaPlayer.URL = $@"{path}\{music}.{format}";

            musicMediaPlayer.Ctlcontrols.play();
            musicMediaPlayer.Ctlenabled = true;
        }

        public void CreatePlaylist(AxWindowsMediaPlayer musicMediaPlayer, bool fromListBeginning)
        {
            string urlMusicPlaying = "";

            if (!fromListBeginning && musicMediaPlayer.playState is WMPPlayState.wmppsPlaying or WMPPlayState.wmppsPaused)
                urlMusicPlaying = musicMediaPlayer.currentMedia.sourceURL;
            else
                musicMediaPlayer.currentPlaylist.clear();

            ListView.ListViewItemCollection listViewItemColl = _listViewMusics.Items;
                
            int[] nextIndex = Enumerable.Range(0, listViewItemColl.Count).ToArray();

            if (_playlistInRandomMode)
            {
                Random rnd = new Random();
                nextIndex = [.. nextIndex.OrderBy(x => rnd.Next())];
            }

            for (int i = 0; i < listViewItemColl.Count; i++)
            {
                ListViewItem lvi = listViewItemColl[nextIndex[i]];

                if (lvi.ForeColor == Color.Red)
                    continue;

                ListViewItem.ListViewSubItemCollection listviewSubItemColl = lvi.SubItems;

                string music = listviewSubItemColl[0].Text;
                string format = listviewSubItemColl[1].Text;
                string path = listviewSubItemColl[3].Text;
                string url = $@"{path}\{music}.{format}";

                if (url == urlMusicPlaying)
                    continue;

                _lastPlaylistMedia = musicMediaPlayer.newMedia(url);
                musicMediaPlayer.currentPlaylist.appendItem(_lastPlaylistMedia);
            }

            if (musicMediaPlayer.currentPlaylist.count > 0)
            {
                musicMediaPlayer.Ctlenabled = true;
                musicMediaPlayer.Ctlcontrols.play();
            }
        }

        public void TogglePlayMusicBySelectionCheckedChanged(CheckBox togglePlayMusicBySelection, ToolTip toolTip,
            AxWindowsMediaPlayer musicMediaPlayer, bool startingCompetition)
        {
            if (togglePlayMusicBySelection.Checked)
            {
                togglePlayMusicBySelection.Text = "Cancelar";
                toolTip.SetToolTip(togglePlayMusicBySelection, "Desabilita tocar a música ao selecioná-la na lista.");

                if (_listViewMusics.SelectedItems.Count > 0)
                {
                    ListViewItem lvi = _listViewMusics.SelectedItems[0];

                    if (lvi.ForeColor == Color.Red)
                    {
                        ShowInvalidMusicMessage();
                    }
                    else
                    {
                        TryPlayMusicBySelectionAndEnable(lvi.SubItems, musicMediaPlayer,
                            _listViewMusics.Items.Count == 1);
                    }
                }
            }
            else
            {
                togglePlayMusicBySelection.Text = "Selecionar e ouvir";
                toolTip.SetToolTip(togglePlayMusicBySelection, "Habilita tocar a música ao selecioná-la na lista.");

                if (!startingCompetition)
                {
                    StopMusicAndClearPlaylist(musicMediaPlayer);
                }
            }
        }

        public void TogglePlaylistModeCheckedChanged(AxWindowsMediaPlayer musicMediaPlayer,
            CheckBox togglePlaylistMode, ToolTip toolTip)
        {
            //musicMediaPlayer.settings.setMode("shuffle", togglePlaylistMode.Checked);
            _playlistInRandomMode = togglePlaylistMode.Checked;

            if (togglePlaylistMode.Checked)
            {
                togglePlaylistMode.Text = "Lista aleatória";
                toolTip.SetToolTip(togglePlaylistMode, "Playlist vai tocar em ordem aleatória.");
            }
            else
            {
                togglePlaylistMode.Text = "Lista sequencial";
                toolTip.SetToolTip(togglePlaylistMode, "Playlist vai tocar sequencialmente.");
            }
        }

        public void ToggleSeeDetailsCheckedChanged(CheckBox toggleSeeDetails)
        {
            if (toggleSeeDetails.Checked)
            {
                _listViewMusics.Columns[0].Width = MusicNameColumnWidth;
                _listViewMusics.Columns[1].Width = MusicFormatColumnWidth;
                _listViewMusics.Columns[2].Width = MusicDurationColumnWidth;
                _listViewMusics.Columns[3].Width = MusicPathColumnWidth;
                toggleSeeDetails.Text = "Ver simplificada";
            }
            else
            {
                _listViewMusics.Columns[0].Width = MusicNameColumnWidth + MusicFormatColumnWidth + MusicFormatColumnWidth;
                _listViewMusics.Columns[0].Width -= 20;
                _listViewMusics.Columns[1].Width = 0;
                _listViewMusics.Columns[2].Width = 0;
                _listViewMusics.Columns[3].Width = 0;
                toggleSeeDetails.Text = "Ver detalhada";
            }
        }

        public void SelectPlayingMusic(AxWindowsMediaPlayer musicMediaPlayer, ref bool setVisibleAndFocus)
        {
            if (musicMediaPlayer.playState != WMPPlayState.wmppsPlaying)
                return;

            ListViewItem? listViewItem = _listViewMusics.FindItemWithText(
                Path.GetFileNameWithoutExtension(musicMediaPlayer.currentMedia.sourceURL));

            if (listViewItem == null)
                return;

            listViewItem.Selected = true;
            
            if (setVisibleAndFocus)
            {
                if (_counterSetVisibleAndFocus-- > 0)
                {
                    listViewItem.EnsureVisible();
                    _listViewMusics.Focus();
                }

                if (_counterSetVisibleAndFocus <= 0)
                {
                    setVisibleAndFocus = false;
                    _counterSetVisibleAndFocus = LoopsToKeepVisibleAndFocus;
                }
            }
        }

        /// <summary>
        /// Work through to fix Media Player Visualizations bug that occurs when a song in
        /// a playlist ends and switches to the next song, canceling the visualization.
        /// 
        /// OBS: Using the previous or next command does not cancel the visualization.
        /// </summary>
        /// <param name="musicMediaPlayer"> Refernece to form AxWindowsMediaPlayer control.</param>
        public void AutoChangeToNextMusic(AxWindowsMediaPlayer musicMediaPlayer)
        {
            if (musicMediaPlayer.playState != WMPPlayState.wmppsPlaying ||
                musicMediaPlayer.currentPlaylist.count < AmountOfMusicsToAutoChangePlaylist)
            {
                return;
            }

            IWMPMedia currentMedia = musicMediaPlayer.currentMedia;
            bool isTheLastMusic = string.Equals(currentMedia.sourceURL, _lastPlaylistMedia.sourceURL);

            if (!isTheLastMusic && (currentMedia.duration - musicMediaPlayer.Ctlcontrols.currentPosition) <= 0.02d) //20ms
                musicMediaPlayer.Ctlcontrols.next();
        }

        public void ChangeVolumeInSeconds(byte seconds, ChangeVolume changeVolume)
        {
            _changeVolume = changeVolume;
            SecondsToChangeVolume = seconds;
            _stepsHaveBeenConfigured = false;
        }

        public void TryChangeVolume(AxWindowsMediaPlayer musicMediaPlayer, TrackBar tbMusicCurrentVol,
            TrackBar tbMusicVolumeMin, TrackBar tbMusicVolumeMax, int timeToDecrement)
        {
            if (SecondsToChangeVolume == null)
                return;

            if (!_stepsHaveBeenConfigured)
            {
                int volumeTarget = _changeVolume == ChangeVolume.ToMin
                    ? tbMusicVolumeMin.Value : tbMusicVolumeMax.Value;

                bool SecondsToChangeIsZero = SecondsToChangeVolume.Value == 0;

                tbMusicCurrentVol.Enabled = SecondsToChangeIsZero;
                tbMusicVolumeMin.Enabled = SecondsToChangeIsZero;
                tbMusicVolumeMax.Enabled = SecondsToChangeIsZero;

                if (SecondsToChangeIsZero)
                {
                    // Instant Set.
                    SecondsToChangeVolume = null;
                    tbMusicCurrentVol.Value = volumeTarget;
                    _currentMusicVolume = tbMusicCurrentVol.Value;
                    return;
                }

                _timerChangeVolumeInMilliSec = TimerController.FromSecondsToMilliseconds(SecondsToChangeVolume.Value);
                _stepsToChangeMusic = (float)(volumeTarget - tbMusicCurrentVol.Value) / _timerChangeVolumeInMilliSec;
                _currentMusicVolume = tbMusicCurrentVol.Value;
                _stepsHaveBeenConfigured = true;
            }

            // After resume competition from pause
            tbMusicCurrentVol.Enabled = false;
            tbMusicVolumeMin.Enabled = false;
            tbMusicVolumeMax.Enabled = false;

            if (!TimerController.PerformCountdown(ref _timerChangeVolumeInMilliSec, ref _timerChangeVolumeInMilliSec,
                _timerChangeVolumeInMilliSec, timeToDecrement))
            {
                _currentMusicVolume += (_stepsToChangeMusic * timeToDecrement);
                tbMusicCurrentVol.Value = (int)_currentMusicVolume;
                return;
            }

            tbMusicCurrentVol.Value = _changeVolume == ChangeVolume.ToMin
                ? tbMusicVolumeMin.Value : tbMusicVolumeMax.Value;

            SecondsToChangeVolume = null;
            _currentMusicVolume = tbMusicCurrentVol.Value;
            _stepsHaveBeenConfigured = false;
            tbMusicCurrentVol.Enabled = true;
            tbMusicVolumeMin.Enabled = true;
            tbMusicVolumeMax.Enabled = true;
        }

        public void ChooseHowToClearPlaylist(AxWindowsMediaPlayer musicMediaPlayer, bool clearAllPlaylist)
        {
            if (clearAllPlaylist)
                StopMusicAndClearPlaylist(musicMediaPlayer);
            else
                KeepOnlyCurrentMusicInThePlaylist(musicMediaPlayer);
        }

        public void StopMusicAndClearPlaylist(AxWindowsMediaPlayer musicMediaPlayer)
        {
            musicMediaPlayer.Ctlenabled = false;
            musicMediaPlayer.Ctlcontrols.stop();
            musicMediaPlayer.currentPlaylist.clear();
        }

        private void KeepOnlyCurrentMusicInThePlaylist(AxWindowsMediaPlayer musicMediaPlayer)
        {
            int amountOfMusics = musicMediaPlayer.currentPlaylist.count;
            string currentMediaUrl = musicMediaPlayer.currentMedia.sourceURL;

            for (int i = amountOfMusics - 1; i >= 0; i--)
            {
                IWMPMedia media = musicMediaPlayer.currentPlaylist.Item[i];

                if (currentMediaUrl != media.sourceURL)
                    musicMediaPlayer.currentPlaylist.removeItem(media);
            }
        }

        public void PrepareForPause(TrackBar tbMusicCurrentVol) => tbMusicCurrentVol.Enabled = true;
    }
}
