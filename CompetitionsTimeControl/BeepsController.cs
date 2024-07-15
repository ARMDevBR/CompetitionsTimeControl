using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WMPLib;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        public int TotalTimeBeforeLastBeepInMs =>
             (int)((TimeForEachBeep * (AmountOfBeeps - 1) + TimeBeforePlayBeeps) * 1000);

        public int HighBeepDuration { get; private set; }

        private readonly AxWMPLib.AxWindowsMediaPlayer _refBeepMediaPlayer;
        private readonly string _beepsPath;
        private readonly Label _lblTestMessages;

        private int _timerBeforePlayBeepsInMiliSec;
        private int _timerForAllBeepsInMiliSec;
        private int _timerForEachBeepInMiliSec;
        private int _rechargeForEachBeepInMiliSec;
        private int _timerForResumeMusicsInMiliSec;

        private BeepState _beepState;
        private BeepPair? _currentBeepPair;
        private List<BeepPair>? _beepPairsList;

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
            HighBeepDuration = (int)media.duration;

            TimeForResumeMusics = value + HighBeepDuration;
        }

        public bool ChangeBeepPairSelection(int selectedIndex)
        {
            if (_beepPairsList == null || selectedIndex < 0 || selectedIndex > _beepPairsList.Count - 1)
                return false;
            
            _currentBeepPair = _beepPairsList[selectedIndex];

            return _currentBeepPair != null;
        }

        private void PlayBeep(string beepPath)
        {
            _refBeepMediaPlayer.Ctlcontrols.stop();
            _refBeepMediaPlayer.URL = beepPath;
            //_refBeepMediaPlayer.Ctlcontrols.play();
        }

        public void TryPerformBeeps(bool canPerform, int timeToDecrement, out bool keepPerforming)
        {
            if (!canPerform)
            {
                keepPerforming = false;
                _beepState = BeepState.WaitToRunBeeps;
                return;
            }
            keepPerforming = true;

            switch (_beepState)
            {
                case BeepState.WaitToRunBeeps:
                    _beepState = BeepState.WaitTimeBeforeBeeps;
                    _timerBeforePlayBeepsInMiliSec = (int)(TimeBeforePlayBeeps * 1000);
                    _timerForEachBeepInMiliSec = (int)(TimeForEachBeep * 1000);
                    _rechargeForEachBeepInMiliSec = _timerForEachBeepInMiliSec;
                    _timerForAllBeepsInMiliSec = _timerForEachBeepInMiliSec * (AmountOfBeeps - 1);
                    _timerForResumeMusicsInMiliSec = (int)(TimeForResumeMusics * 1000);
                    _refBeepMediaPlayer.settings.autoStart = true;
                    break;

                case BeepState.WaitTimeBeforeBeeps:
                    if (PerformCountdown(ref _timerBeforePlayBeepsInMiliSec, ref _timerForEachBeepInMiliSec,
                        in _timerBeforePlayBeepsInMiliSec, timeToDecrement))
                    {
                        _timerBeforePlayBeepsInMiliSec = 0;
                        _beepState = BeepState.PerformBeeps;
                        PlayBeep(LowBeepPath);
                    }
                    break;

                case BeepState.PerformBeeps:
                    void eachBeepCountdown()
                    {
                        if (PerformCountdown(ref _timerForEachBeepInMiliSec, ref _timerForEachBeepInMiliSec,
                            in _rechargeForEachBeepInMiliSec, timeToDecrement))
                        {
                            PlayBeep((_timerForAllBeepsInMiliSec > timeToDecrement) ? LowBeepPath : HighBeepPath);
                        }
                    }

                    // All beeps countdown
                    if (PerformCountdown(ref _timerForAllBeepsInMiliSec, ref _timerForResumeMusicsInMiliSec,
                        in _timerForAllBeepsInMiliSec, timeToDecrement, eachBeepCountdown))
                    {
                        _timerForAllBeepsInMiliSec = 0;
                        _beepState = BeepState.TimeForResumeMusicsVolume;
                    }
                    break;

                case BeepState.TimeForResumeMusicsVolume:
                    if (PerformCountdown(ref _timerForResumeMusicsInMiliSec, ref _timerForResumeMusicsInMiliSec,
                        in _timerForResumeMusicsInMiliSec, timeToDecrement))
                    {
                        _timerForResumeMusicsInMiliSec = 0;
                        keepPerforming = false;
                        _beepState = BeepState.WaitToRunBeeps;
                    }
                    break;

                default:
                    keepPerforming = false;
                    _beepState = BeepState.WaitToRunBeeps;
                    break;
            }

            if (!keepPerforming)
                _refBeepMediaPlayer.settings.autoStart = false;

            _lblTestMessages.Text = string.Concat(
                $"Antes dos beeps {GetTimeSpanFormat(_timerBeforePlayBeepsInMiliSec)} | ",
                $"Até último beep {GetTimeSpanFormat(_timerForAllBeepsInMiliSec)} | ",
                $"Após beeps {GetTimeSpanFormat(_timerForResumeMusicsInMiliSec)}");
        }

        private bool PerformCountdown(ref int counterValue, ref int nextCounterValue, in int nextCounterRecharge,
            int timeToDecrement, Action? extraFunctionCallback = null)
        {
            bool ret = false;

            counterValue -= timeToDecrement;
            
            if (counterValue <= 0)
            {
                nextCounterValue += nextCounterRecharge;
                ret = true;
            }
            extraFunctionCallback?.Invoke();

            return ret;
        }

        private string GetTimeSpanFormat(double value) => TimeSpan.FromMilliseconds(value).ToString(@"ss\.fff");
    }
}
