using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;

namespace CompetitionsTimeControl
{
    internal class BeepPair(string highBeepPath, string lowBeepPath)
    {
        public string HighBeepPath { get; private set; } = highBeepPath;
        public string LowBeepPath { get; private set; } = lowBeepPath;
    }

    internal class BeepsController
    {
        private const int AmountOfBeepsPerFolder = 2;
        private const string DefaultBeepsFolderPath = "BeepSounds";

        private enum BeepState{ WaitToRunBeeps, WaitTimeBeforeBeeps, PerformBeeps, TimeForResumeMusicsVolume }

        public string HighBeepPath => _currentBeepPair?.HighBeepPath ?? "";
        public string LowBeepPath => _currentBeepPair?.LowBeepPath ?? "";

        public float TimeBeforePlayBeeps { get; set; }
        public int AmountOfBeeps { get; set; }
        public float TimeForEachBeep { get; set; }
        public float TimeForResumeMusics { get; private set; }

        private int _timeBeforePlayBeepsInMiliSec;
        private int _timeForEachBeepInMiliSec;
        private int _timeForResumeMusicsInMiliSec;
        private int _countdownAmountOfBeeps;

        private AxWMPLib.AxWindowsMediaPlayer _refBeepMediaPlayer;
        private BeepState _beepState;
        private bool _performBeeps;
        private string _beepsPath;
        private Label _lblTestMessages;
        private List<BeepPair>? _beepPairsList;
        private BeepPair? _currentBeepPair;

        public BeepsController(ComboBox comboBoxBeepPair, Label lblTestMessages,
            AxWMPLib.AxWindowsMediaPlayer beepMediaPlayer)
        {
            _beepState = BeepState.WaitToRunBeeps;
            _beepsPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, DefaultBeepsFolderPath);
            _lblTestMessages = lblTestMessages;
            _refBeepMediaPlayer = beepMediaPlayer;
            GetBeepsSounds(comboBoxBeepPair);
        }

        private void GetBeepsSounds(ComboBox comboBoxBeepPair)
        {
            string[] beepsPathsArray = Directory.GetDirectories(_beepsPath);

            foreach (string beepPath in beepsPathsArray)
            {
                string[] beepsArray = Directory.GetFiles(beepPath, "??Beep.*", SearchOption.TopDirectoryOnly)
                    //.Where(s => MainForm.SupportedExtensions.Contains(Path.GetExtension(s).ToLower())).ToArray();
                    .Where(s => MainForm.SupportedExtensions.Contains(Path.GetExtension(s), StringComparison.CurrentCultureIgnoreCase)).ToArray();

                if (beepsArray.Length == AmountOfBeepsPerFolder)
                {
                    _beepPairsList ??= new List<BeepPair>();

                    comboBoxBeepPair.Items.Add(beepPath.Remove(0, _beepsPath.Length + 1));
                    _beepPairsList.Add(new BeepPair(beepsArray[0], beepsArray[1]));
                }
                else
                {
                    MessageBox.Show("Não foi encontrado o par de arquivos de beeps ou há arquivos em excesso.\n" +
                        $"\nVerifique a pasta '{beepPath}'",
                        "ATENÇÃO",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
        }

        public void SetTimeForResumeMusics(float value)
        {
            IWMPMedia media = _refBeepMediaPlayer.newMedia(HighBeepPath);

            TimeForResumeMusics = value + (float)media.duration;
        }

        public bool ChangeBeepPairSelection(int selectedIndex)
        {
            if (_beepPairsList == null || selectedIndex < 0 || selectedIndex > _beepPairsList.Count - 1)
                return false;
            
            _currentBeepPair = _beepPairsList[selectedIndex];

            return _currentBeepPair != null;
        }

        public void TryPerformBeeps(bool canPerform, int timeToDecrement, out bool keepPerforming)
        {

            switch (_beepState)
            {
                case BeepState.WaitToRunBeeps:
                    keepPerforming = canPerform;

                    if (canPerform)
                    {
                        _beepState = BeepState.WaitTimeBeforeBeeps;
                        _timeBeforePlayBeepsInMiliSec = (int)(TimeBeforePlayBeeps * 1000);
                        _timeForEachBeepInMiliSec = (int)(TimeForEachBeep * (AmountOfBeeps - 1) * 1000);
                        _timeForResumeMusicsInMiliSec = (int)(TimeForResumeMusics * 1000);
                        _countdownAmountOfBeeps = AmountOfBeeps;
                        break;
                    }
                    return;
                
                case BeepState.WaitTimeBeforeBeeps:
                    keepPerforming = true;

                    if (canPerform)
                    {
                        if (_timeBeforePlayBeepsInMiliSec > 0)
                            _timeBeforePlayBeepsInMiliSec -= timeToDecrement;

                        if (_timeBeforePlayBeepsInMiliSec <= 0)
                        {
                            _timeBeforePlayBeepsInMiliSec = 0;
                            _beepState = BeepState.PerformBeeps;
                        }
                        break;
                    }

                    keepPerforming = false;
                    _beepState = BeepState.WaitToRunBeeps;
                    break;
                
                case BeepState.PerformBeeps:
                    keepPerforming = true;

                    if (canPerform)
                    {
                        if (_timeForEachBeepInMiliSec > 0)
                            _timeForEachBeepInMiliSec -= timeToDecrement;

                        if (_timeForEachBeepInMiliSec <= 0)
                        {
                            _timeForEachBeepInMiliSec = 0;
                            _beepState = BeepState.TimeForResumeMusicsVolume;
                        }
                        break;
                    }

                    keepPerforming = false;
                    _beepState = BeepState.WaitToRunBeeps;
                    break;

                case BeepState.TimeForResumeMusicsVolume:
                    keepPerforming = true;

                    if (canPerform)
                    {
                        if (_timeForResumeMusicsInMiliSec > 0)
                            _timeForResumeMusicsInMiliSec -= timeToDecrement;

                        if (_timeForResumeMusicsInMiliSec <= 0)
                        {
                            _timeForResumeMusicsInMiliSec = 0;
                            keepPerforming = false;
                            _beepState = BeepState.WaitToRunBeeps;
                        }
                        break;
                    }

                    keepPerforming = false;
                    _beepState = BeepState.WaitToRunBeeps;
                    break;

                default:
                    keepPerforming = false;
                    _beepState = BeepState.WaitToRunBeeps;
                    return;
            }

            _lblTestMessages.Text = string.Concat(
                $"Antes dos beeps {GetTimeSpanFormat(_timeBeforePlayBeepsInMiliSec)} | ",
                $"Até último beep {GetTimeSpanFormat(_timeForEachBeepInMiliSec)} | ",
                $"Após beeps {GetTimeSpanFormat(_timeForResumeMusicsInMiliSec)}");
        }

        private string GetTimeSpanFormat(double value) => TimeSpan.FromMilliseconds(value).ToString(@"ss\.fff");
    }
}
