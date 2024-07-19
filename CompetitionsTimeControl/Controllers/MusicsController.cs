using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public bool AddMusicsToList(AxWMPLib.AxWindowsMediaPlayer musicMediaPlayer)
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

        public void ListViewMusicsItemChecked(in ListViewItem item)
        {
            if (item.Checked)
                _checkedMusicToDelete.Add(item.Index);
            else if (_checkedMusicToDelete.Contains(item.Index))
                _ = _checkedMusicToDelete.Remove(item.Index);
            
            //SetMarkAndExcludeTextAndToolTip();
        }
    }
}
