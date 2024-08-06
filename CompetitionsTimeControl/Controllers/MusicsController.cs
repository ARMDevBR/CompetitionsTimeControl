using AxWMPLib;
using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Windows.Forms;
using WMPLib;

namespace CompetitionsTimeControl.Controllers
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class MusicsController
    {
        private const int MusicNameColumnWidth = 300;
        private const int MusicFormatColumnWidth = 75;
        private const int MusicDurationColumnWidth = 55;
        private const int MusicPathColumnWidth = 400;
        private const byte AmountOfMusicsToAutoChangePlaylist = 2;
        private const double OffsetTimeToChangeMusicInMS = 0.167d;

        private static readonly Color ListViewItemInvalidForeColor = Color.Red;

        public enum ChangeVolume { ToMin, ToMax }

        private enum AutoChangeMusicState { WaitForStopAndChange, PlayNextMusic }

        public byte? SecondsToChangeVolume { get; private set; }
        public bool HasJustOneValidMusic { get; private set; }
        [JsonProperty] public List<string> MusicsPathsList { get; private set; }
        [JsonProperty] public bool PlaylistInRandomMode { get; private set; }
        [JsonProperty] public bool IsSimplifiedView { get; private set; }
        [JsonProperty] public byte LimitMinMusicVolume { get; set; }
        [JsonProperty] public byte LimitMaxMusicVolume { get; set; }
        [JsonProperty] public byte CurrentMusicVolume { get; set; }

        private readonly ListView _listViewMusics;

        private bool _stepsHaveBeenConfigured;
        private float _currentMusicVolume;
        private int _timerChangeVolumeInMilliSec;
        private float _stepsToChangeMusic;
        private ChangeVolume _changeVolume;
        private AutoChangeMusicState _autoChangeMusicState;
        private readonly List<int> _checkedMusicToDelete;
        private IWMPMedia _currentPlaylistMedia = null!;
        private IWMPMedia _lastPlaylistMedia = null!;
        private string _lastDirectory;

        public MusicsController(ListView listViewMusics)
        {
            _stepsHaveBeenConfigured = false;
            _stepsToChangeMusic = 0;
            _checkedMusicToDelete = [];
            _listViewMusics = listViewMusics;
            MusicsPathsList = [];
            SecondsToChangeVolume = null;
            _lastDirectory = "";
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
            OpenFileDialog openFileDialog = new()
            {
                Filter = $"Arquivos de som | {MainForm.SupportedExtensions}",
                Title = "Selecione as músicas desejadas",
                Multiselect = true,
                InitialDirectory = _lastDirectory
            };

            openFileDialog.ShowDialog();

            if (_listViewMusics == null || openFileDialog.FileNames == null || openFileDialog.FileNames.Length <= 0)
                return false;

            _lastDirectory = Path.GetDirectoryName(openFileDialog.FileNames[0]) ?? "";

            return FillListView(openFileDialog.FileNames, musicMediaPlayer);
        }

        public bool FillListView(string[] musicsArray, AxWindowsMediaPlayer musicMediaPlayer, bool clearAllBefore = false)
        {
            bool ret = false;

            if (clearAllBefore)
                ClearAllLists();

            StringBuilder? sbRepeatedMusics = null;
            StringBuilder? sbInvalidMusics = null;

            _listViewMusics.GridLines = true;
            int index = _listViewMusics.Items.Count;

            foreach (string pathFileName in musicsArray)
            {
                string nameWithExtension = Path.GetFileName(pathFileName);
                string musicName = nameWithExtension[..^4];
                string musicExtension = nameWithExtension[^3..];
                string mediaDurationStr = musicMediaPlayer.newMedia(pathFileName).durationString;
                string path = Path.GetDirectoryName(pathFileName) ?? "";

                if (_listViewMusics.FindItemWithText(musicName) != null)
                {
                    sbRepeatedMusics ??= new("As seguintes músicas já existem na lista e não foram incluídas novamente:\n\n");

                    sbRepeatedMusics?.AppendLine($"  ➤  {musicName}");
                    continue;
                }

                ListViewItem listViewItem = new([musicName, musicExtension, mediaDurationStr, path])
                {
                    BackColor = (index++ & 1) > 0 ? Color.Azure : Color.Beige
                };
                
                if (!IsValidMusicFile(listViewItem, pathFileName, musicExtension))
                {
                    sbInvalidMusics ??= new("As seguintes músicas são inválidas ou não existem mais:\n\n");
                    sbInvalidMusics?.AppendLine($"  ➤  {musicName}");
                }

                _listViewMusics.Items.Add(listViewItem);
                MusicsPathsList.Add(pathFileName);

                ret = true;
            }

            if (sbRepeatedMusics != null)
            {
                sbRepeatedMusics.Append("\nVerifique a última pasta selecionada e renomeie as músicas se necessário.");

                MessageBox.Show(sbRepeatedMusics.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (sbInvalidMusics != null)
            {
                sbInvalidMusics.Append("\nElas foram marcadas em vermelho para que possam ser verificadas individualmente.");

                MessageBox.Show(sbInvalidMusics.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

            return ret;
        }

        private static bool IsValidMusicFile(ListViewItem listViewItem, string musicPath, string extension)
        {
            bool ret = true;

            if (!File.Exists(musicPath) || !AudioFileController.IsCorrectFileType(musicPath, extension))
            {
                SetInvalidItem(listViewItem);
                ret = false;
            }
            return ret;
        }

        public bool TryClearListViewMusics()
        {
            StringBuilder sb = new("Todas músicas serão excluídas da lista!\n\n");
            sb.Append("Deseja continuar com a operação?");

            if (MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2) == DialogResult.No)
                return false;

            ClearAllLists();
            return true;
        }

        private void ClearAllLists()
        {
            _checkedMusicToDelete.Clear();
            MusicsPathsList.Clear();
            _listViewMusics.Items.Clear();
            _listViewMusics.CheckBoxes = false;
        }

        public void SetMarkAndExcludeTextAndToolTip(CheckBox toggleMarkAndExclude, ToolTip toolTip)
        {
            _listViewMusics.CheckBoxes = toggleMarkAndExclude.Checked;

            if (toggleMarkAndExclude.Checked)
            {
                if (_checkedMusicToDelete.Count > 0)
                {
                    toggleMarkAndExclude.Text = "Excluir ?";

                    toolTip.SetToolTip(toggleMarkAndExclude, string.Concat(
                        "- Ao clicar novamente, as músicas com checkbox marcado serão excluídas da lista.\n",
                        "- Desmarque todas as músicas que não queira excluir da lista."));
                }
                else
                {
                    toggleMarkAndExclude.Text = "Check ✓";

                    toolTip.SetToolTip(toggleMarkAndExclude, string.Concat(
                        "- Ao clicar novamente, a função de exclusão individual será cancelada.\n",
                        "- Marque o checkbox das músicas que deseja excluir da lista."));
                }
            }
            else
            {
                toggleMarkAndExclude.Text = "Excluir seleção";
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
                    int indexToRemove = _checkedMusicToDelete[i];
                    _listViewMusics.Items.RemoveAt(indexToRemove);
                    MusicsPathsList.RemoveAt(indexToRemove);
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
                if (IsInvalidItem(lvi))
                {
                    ShowInvalidMusicMessage(true);
                }
                else if (_listViewMusics.SelectedItems.Count > 0)
                {
                    TryPlayMusicBySelectionAndEnable(lvi, musicMediaPlayer);
                }
            }
            else
            {
                musicMediaPlayer.Ctlcontrols.pause();
                musicMediaPlayer.Ctlenabled = false;
            }
        }

        private static bool IsInvalidItem(ListViewItem lvi) => lvi.ForeColor == ListViewItemInvalidForeColor;
        private static void SetInvalidItem(ListViewItem lvi) => lvi.ForeColor = ListViewItemInvalidForeColor;

        private static void ShowInvalidMusicMessage(bool wasInvalidBefore)
        {
            StringBuilder sb = new("Esta música pode estar corrompida ou não existir mais na origem.\n\n");

            if (wasInvalidBefore)
                sb.Append("As músicas listadas em vermelho NÃO são válidas e podem ser excluídas da lista.");
            else
                sb.Append("Ela foi marcada em vermelho para que possa ser verificada individualmente.");

            MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }
        
        public bool HasValidMusics()
        {
            ListView.ListViewItemCollection listViewItemColl = _listViewMusics.Items;
            bool ret = false;
            
            for (int i = 0; i < listViewItemColl.Count; i++)
            {
                if (!IsInvalidItem(listViewItemColl[i]))
                {
                    HasJustOneValidMusic = !ret;
                    ret = true;
                }
            }
            return ret;
        }

        private void TryPlayMusicBySelectionAndEnable(ListViewItem lvi, AxWindowsMediaPlayer musicMediaPlayer,
            bool forceRestartMusic = false)
        {
            string musicPath = MusicsPathsList[lvi.Index];

            if (!IsValidMusicFile(lvi, musicPath, Path.GetExtension(musicPath)[1..]))
            {
                ShowInvalidMusicMessage(false);
                return;
            }

            forceRestartMusic = forceRestartMusic || musicMediaPlayer.playState == WMPPlayState.wmppsUndefined;

            IWMPMedia currentMedia = musicMediaPlayer.currentMedia;

            if (forceRestartMusic || currentMedia == null || !currentMedia.sourceURL.Equals(musicPath))
            {
                musicMediaPlayer.URL = musicPath;
            }

            musicMediaPlayer.Ctlcontrols.play();
            musicMediaPlayer.Ctlenabled = true;
        }

        public bool HasMusicsToPlaylist(out bool skipCanStartMessage)
        {
            skipCanStartMessage = false;

            ListView.ListViewItemCollection listViewItemColl = _listViewMusics.Items;

            int itemsCount = listViewItemColl.Count;
            StringBuilder? sbInvalidMusics = null;

            bool ret = itemsCount > 0;

            for (int i = 0; i < itemsCount; i++)
            {
                ListViewItem lvi = listViewItemColl[i];

                if (IsInvalidItem(lvi))
                    continue;

                string musicPath = MusicsPathsList[lvi.Index];
                string musicName = Path.GetFileName(musicPath)[..^4];

                if (!IsValidMusicFile(lvi, musicPath, Path.GetExtension(musicPath)[1..]))
                {
                    sbInvalidMusics ??= new("As seguintes músicas são inválidas ou não existem mais:\n\n");
                    sbInvalidMusics?.AppendLine($"  ➤  {musicName}");
                    ret = false;
                }
            }

            if (sbInvalidMusics != null)
            {
                sbInvalidMusics.Append("\nElas foram marcadas em vermelho para que possam ser verificadas individualmente.");
                MessageBox.Show(sbInvalidMusics.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                if (HasValidMusics())
                {
                    sbInvalidMusics.Clear();
                    sbInvalidMusics.Append("Deseja iniciar a competição com as músicas válidas existentes?");

                    ret = MessageBox.Show(sbInvalidMusics.ToString(), "ATENÇÃO", MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question) == DialogResult.Yes;
                    
                    skipCanStartMessage = ret;
                }
            }

            return ret;
        }

        public void CreatePlaylist(AxWindowsMediaPlayer musicMediaPlayer, bool fromListBeginning)
        {
            string urlMusicPlaying = "";

            if (!fromListBeginning && musicMediaPlayer.playState is WMPPlayState.wmppsPlaying or WMPPlayState.wmppsPaused
                or WMPPlayState.wmppsStopped)
            {
                urlMusicPlaying = musicMediaPlayer.currentMedia.sourceURL;
            }
            else
            {
                musicMediaPlayer.Ctlcontrols.stop();
                musicMediaPlayer.currentPlaylist.clear();
            }

            ListView.ListViewItemCollection listViewItemColl = _listViewMusics.Items;

            int itemsCount = listViewItemColl.Count;
                
            int[] nextIndex = Enumerable.Range(0, itemsCount).ToArray();
            
            if (itemsCount > 1 && !HasJustOneValidMusic && PlaylistInRandomMode)
            {
                int n = nextIndex.Length;
                Random rnd = new();

                while (n > 1)
                {
                    int k = rnd.Next(n--);
                    (nextIndex[k], nextIndex[n]) = (nextIndex[n], nextIndex[k]);
                }
            }

            for (int i = 0; i < itemsCount; i++)
            {
                if (IsInvalidItem(listViewItemColl[nextIndex[i]]))
                    continue;

                string url = MusicsPathsList[nextIndex[i]];

                if (urlMusicPlaying != "" && urlMusicPlaying == url)
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
                togglePlayMusicBySelection.Text = "  Parar";
                togglePlayMusicBySelection.ImageIndex = 4;
                toolTip.SetToolTip(togglePlayMusicBySelection, "Desabilita tocar qualquer música selecionada na lista.");

                if (!startingCompetition && _listViewMusics.SelectedItems.Count > 0)
                {
                    ListViewItem lvi = _listViewMusics.SelectedItems[0];

                    if (IsInvalidItem(lvi))
                    {
                        ShowInvalidMusicMessage(true);
                    }
                    else
                    {
                        // Trocar a contagem por itens válidos
                        TryPlayMusicBySelectionAndEnable(lvi, musicMediaPlayer, _listViewMusics.Items.Count == 1);
                    }
                }
            }
            else
            {
                togglePlayMusicBySelection.Text = "Ouvir seleção";
                togglePlayMusicBySelection.ImageIndex = 3;
                toolTip.SetToolTip(togglePlayMusicBySelection, "Habilita tocar a música ao selecioná-la na lista.");

                if (!startingCompetition)
                {
                    DisablePlayerAndClearPlaylist(musicMediaPlayer);
                }
            }
        }

        public void TogglePlaylistModeCheckedChanged(CheckBox togglePlaylistMode, ToolTip toolTip)
        {
            PlaylistInRandomMode = togglePlaylistMode.Checked;

            if (togglePlaylistMode.Checked)
            {
                togglePlaylistMode.ImageIndex = 6;
                toolTip.SetToolTip(togglePlaylistMode, "Playlist vai tocar em ordem aleatória.");
            }
            else
            {
                togglePlaylistMode.ImageIndex = 5;
                toolTip.SetToolTip(togglePlaylistMode, "Playlist vai tocar sequencialmente.");
            }
        }

        public void ToggleSeeDetailsCheckedChanged(CheckBox toggleSeeDetails)
        {
            IsSimplifiedView = !toggleSeeDetails.Checked;

            if (toggleSeeDetails.Checked)
            {
                _listViewMusics.Columns[0].Width = MusicNameColumnWidth;
                _listViewMusics.Columns[1].Width = MusicFormatColumnWidth;
                _listViewMusics.Columns[2].Width = MusicDurationColumnWidth;
                _listViewMusics.Columns[3].Width = MusicPathColumnWidth;
                toggleSeeDetails.ImageIndex = 7;
            }
            else
            {
                _listViewMusics.Columns[0].Width = MusicNameColumnWidth + MusicFormatColumnWidth + MusicFormatColumnWidth;
                _listViewMusics.Columns[0].Width -= 35;
                _listViewMusics.Columns[1].Width = 0;
                _listViewMusics.Columns[2].Width = 0;
                _listViewMusics.Columns[3].Width = 0;
                toggleSeeDetails.ImageIndex = 8;
            }
        }

        /*public void SetMinVolumePercentage(byte limitMinMusicVolume)
        {
            LimitMinMusicVolume = limitMinMusicVolume;
        }

        public void SetMaxVolumePercentage(byte limitMaxMusicVolume)
        {
            LimitMaxMusicVolume = limitMaxMusicVolume;
        }

        public void SetVolumePercentage(byte musicVolume)
        {
            CurrentMusicVolume = musicVolume;
        }*/

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
                _listViewMusics.Focus();
                listViewItem.EnsureVisible();
                setVisibleAndFocus = false;
            }
        }

        public void SetCurrentPlaylistMedia(AxWindowsMediaPlayer musicMediaPlayer)
        {
            _currentPlaylistMedia = musicMediaPlayer.currentMedia;
        }

        /// <summary>
        /// Work through to fix Media Player Visualizations bug that occurs when a song in
        /// a playlist ends and switches to the next song, canceling the visualization.
        /// 
        /// OBS: Using the previous or next command does not cancel the visualization.
        /// </summary>
        /// <param name="musicMediaPlayer"> Reference to form AxWindowsMediaPlayer control.</param>
        /// <param name="timeToDecrement"> Timer resolution in Milliseconds.</param>
        public void AutoChangeToNextMusic(AxWindowsMediaPlayer musicMediaPlayer, int elapsedTime)
        {
            switch (_autoChangeMusicState)
            {
                case AutoChangeMusicState.WaitForStopAndChange:
                    if (_currentPlaylistMedia == null)// ||
                        //musicMediaPlayer.playState != WMPPlayState.wmppsPlaying)// ||
                        //musicMediaPlayer.currentPlaylist.count < AmountOfMusicsToAutoChangePlaylist)
                    {
                        return;
                    }

                    double durationAndCurrPositionDifference = _currentPlaylistMedia.duration -
                        musicMediaPlayer.Ctlcontrols.currentPosition;

                    double offsetTimeToChangeMusicInSecs = OffsetTimeToChangeMusicInMS +
                        TimerController.FromMillisecondsToSeconds(elapsedTime); // About 180ms

                    if (durationAndCurrPositionDifference <= offsetTimeToChangeMusicInSecs)
                    {
                        musicMediaPlayer.Ctlcontrols.stop();

                        if (HasJustOneValidMusic)
                            musicMediaPlayer.URL = _currentPlaylistMedia.sourceURL;
                        else
                            musicMediaPlayer.Ctlcontrols.next();

                        _autoChangeMusicState = AutoChangeMusicState.PlayNextMusic;
                    }
                    break;
                
                case AutoChangeMusicState.PlayNextMusic:
                    if (musicMediaPlayer.playState == WMPPlayState.wmppsStopped ||
                        musicMediaPlayer.playState == WMPPlayState.wmppsReady)
                    {
                        musicMediaPlayer.Ctlcontrols.play();
                    }
                    
                    if (musicMediaPlayer.playState == WMPPlayState.wmppsPlaying)
                        _autoChangeMusicState = AutoChangeMusicState.WaitForStopAndChange;
                    break;
            }
        }

        public bool IsTheLastPlaylistMusic()
        {
            return _currentPlaylistMedia != null && _currentPlaylistMedia.sourceURL != "" &&
                _currentPlaylistMedia.sourceURL.Equals(_lastPlaylistMedia?.sourceURL ?? "");
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
                _timerChangeVolumeInMilliSec, timeToDecrement, true))
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

        public static void ChooseHowToClearPlaylist(AxWindowsMediaPlayer musicMediaPlayer, bool clearAllPlaylist)
        {
            if (clearAllPlaylist)
                DisablePlayerAndClearPlaylist(musicMediaPlayer);
            else
                KeepOnlyCurrentMusicInThePlaylist(musicMediaPlayer);
        }

        public static void DisablePlayerAndClearPlaylist(AxWindowsMediaPlayer musicMediaPlayer)
        {
            musicMediaPlayer.Ctlenabled = false;
            musicMediaPlayer.currentPlaylist.clear();
        }

        private static void KeepOnlyCurrentMusicInThePlaylist(AxWindowsMediaPlayer musicMediaPlayer)
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

        public static void PrepareForPause(TrackBar tbMusicCurrentVol) => tbMusicCurrentVol.Enabled = true;
    }
}
