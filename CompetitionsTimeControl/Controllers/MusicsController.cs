﻿using AxWMPLib;
using System.Text;

namespace CompetitionsTimeControl.Controllers
{
    internal class MusicsController
    {
        private const int MusicNameColumnWidth = 300;
        private const int MusicFormatColumnWidth = 75;
        private const int MusicDurationColumnWidth = 55;
        private const int MusicPathColumnWidth = 400;

        private readonly ListView _listViewMusics;

        private List<int> _checkedMusicToDelete;

        public MusicsController(ListView listViewMusics)
        {
            _checkedMusicToDelete = [];
            _listViewMusics = listViewMusics;
            GenerateListHeaders();
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

                RepaintListViewGrid();
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

        private void RepaintListViewGrid()
        {
            var items = _listViewMusics.Items;

            foreach (ListViewItem item in items)
                item.BackColor = (item.Index & 1) > 0 ? Color.Azure : Color.Beige;
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
            
            if (_listViewMusics.SelectedItems.Count > 0 && lvi != null)
            {
                ListViewItem.ListViewSubItemCollection? listviewSubItemColl = lvi.SubItems;

                TryPlayMusicBySelectionAndEnable(listviewSubItemColl, musicMediaPlayer);
            }
            else
            {
                musicMediaPlayer.Ctlcontrols.pause();
                musicMediaPlayer.Ctlenabled = false;
            }
        }

        private void TryPlayMusicBySelectionAndEnable(ListViewItem.ListViewSubItemCollection? listviewSubItemColl,
            AxWindowsMediaPlayer musicMediaPlayer)
        {
            if (listviewSubItemColl == null)
                return;

            string music = listviewSubItemColl[0].Text;
            string format = listviewSubItemColl[1].Text;
            string path = listviewSubItemColl[3].Text;

            if (!string.Equals(musicMediaPlayer.URL, $@"{path}\{music}.{format}"))
                musicMediaPlayer.URL = $@"{path}\{music}.{format}";

            musicMediaPlayer.Ctlcontrols.play();
            musicMediaPlayer.Ctlenabled = true;
        }

        private void CreatePlaylist(AxWindowsMediaPlayer musicMediaPlayer, string url)
        {
            musicMediaPlayer.currentPlaylist.appendItem(musicMediaPlayer.newMedia(url));
        }

        public void TogglePlayMusicBySelectionCheckedChanged(CheckBox togglePlayMusicBySelection, ToolTip toolTip,
            AxWindowsMediaPlayer musicMediaPlayer)
        {
            if (togglePlayMusicBySelection.Checked)
            {
                if (_listViewMusics.SelectedItems.Count > 0)
                {
                    TryPlayMusicBySelectionAndEnable(_listViewMusics.SelectedItems[0].SubItems, musicMediaPlayer);
                }

                togglePlayMusicBySelection.Text = "Cancelar";
                toolTip.SetToolTip(togglePlayMusicBySelection, "Desabilita tocar a música ao selecioná-la na lista.");
            }
            else
            {
                musicMediaPlayer.Ctlenabled = false;
                musicMediaPlayer.currentPlaylist.clear();
                togglePlayMusicBySelection.Text = "Selecionar e ouvir";
                toolTip.SetToolTip(togglePlayMusicBySelection, "Habilita tocar a música ao selecioná-la na lista.");
            }
        }

        public void TogglePlaylistModeCheckedChanged(AxWindowsMediaPlayer musicMediaPlayer,
            CheckBox togglePlaylistMode, ToolTip toolTip)
        {
            musicMediaPlayer.settings.setMode("shuffle", togglePlaylistMode.Checked);
            //MusicMediaPlayer.settings.setMode("loop", TogglePlaylistMode.Checked); // Ver se é loop na playlist

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

        public void SelectPlayingMusic(AxWindowsMediaPlayer musicMediaPlayer)
        {
            ListViewItem? listViewItem = _listViewMusics.FindItemWithText(
                Path.GetFileNameWithoutExtension(musicMediaPlayer.currentMedia.sourceURL));

            if (listViewItem == null)
                return;

            listViewItem.Selected = true;
            listViewItem.EnsureVisible();
            _listViewMusics.Select();
        }
    }
}
