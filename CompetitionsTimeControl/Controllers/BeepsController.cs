using AxWMPLib;
using Newtonsoft.Json;
using WMPLib;

namespace CompetitionsTimeControl.Controllers
{
    internal class BeepPair(string highBeepPath, string lowBeepPath)
    {
        public string HighBeepPath { get; private set; } = highBeepPath;
        public string LowBeepPath { get; private set; } = lowBeepPath;
    }

    [JsonObject(MemberSerialization.OptIn)]
    internal class BeepsController
    {
        private const int AmountOfBeepsPerFolder = 2;
        private const string DefaultBeepsFolderPath = "BeepSounds";

        private enum BeepState { WaitToRunBeeps, WaitTimeBeforeBeeps, PerformBeeps, TimeForResumeMusicsVolume }

        public string HighBeepPath => _currentBeepPair?.HighBeepPath ?? "";
        public string LowBeepPath => _currentBeepPair?.LowBeepPath ?? "";

        [JsonProperty] public byte CurrentBeepPairIndex { get; private set; }
        [JsonProperty] public float TimeBeforePlayBeeps { get; set; }
        [JsonProperty] public int AmountOfBeeps { get; set; }
        [JsonProperty] public float TimeForEachBeep { get; set; }
        [JsonProperty] public float TimeForResumeMusics { get; private set; }
        [JsonProperty] public byte CurrentBeepsVolume { get; private set; }
        public string CountingBeepsDescription { get; private set; }
        public string LastBeepsDescription { get; private set; }

        public int TotalTimeBeforeLastBeepInMs =>
            TimerController.FromSecondsToMilliseconds(TimeForEachBeep * (AmountOfBeeps - 1) + TimeBeforePlayBeeps);
        
        public int TotalTimeForAllBeepEventInMs =>
            TimerController.FromSecondsToMilliseconds(TimeForEachBeep * (AmountOfBeeps - 1)
                + TimeBeforePlayBeeps + TimeForResumeMusics);

        public int HighBeepDuration { get; private set; }
        public bool CanPerformBeepsEvent { get; set; }

        private readonly string _beepsPath;

        private int _timerBeforePlayBeepsInMilliSec;
        private int _timerForAllBeepsInMilliSec;
        private int _timerForEachBeepInMilliSec;
        private int _rechargeForEachBeepInMilliSec;
        private int _timerForResumeMusicsInMilliSec;
        private int _beepCounter;
        private float _justTimeForResumeMusics;

        private BeepState _beepState;
        private BeepPair? _currentBeepPair;
        private List<BeepPair>? _beepPairsList;
        private Label _lblCompetitionTotalTime;

        public BeepsController(ComboBox comboBoxBeepPair, Label lblCompetitionTotalTime)
        {
            CountingBeepsDescription = "";
            LastBeepsDescription = "";
            _beepState = BeepState.WaitToRunBeeps;
            _beepsPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, DefaultBeepsFolderPath);
            GetBeepsSounds(comboBoxBeepPair);
            _lblCompetitionTotalTime = lblCompetitionTotalTime;
        }

        public void GetBeepsSounds(ComboBox comboBoxBeepPair)
        {
            if (!Directory.Exists(_beepsPath))
            {
                MessageBox.Show(string.Concat("Não foi encontrado a pasta raiz com as patas dos pares de beeps.\n",
                    $"\nVerifique a pasta '{AppDomain.CurrentDomain.BaseDirectory}'.",
                    $"\nA pasta '{DefaultBeepsFolderPath}' deve existir neste local."),
                    "ATENÇÃO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                comboBoxBeepPair.Items.Clear();
                _beepPairsList?.Clear();
                return;
            }

            string[] beepsPathsArray = Directory.GetDirectories(_beepsPath);

            if (beepsPathsArray.Length <= 0)
            {
                MessageBox.Show(string.Concat("A pasta raiz não possui nenhuma pasta com os pares de beeps.\n",
                    $"\nVerifique a pasta '{_beepsPath}'."),
                    "ATENÇÃO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }

            comboBoxBeepPair.Items.Clear();
            _beepPairsList?.Clear();

            foreach (string beepPath in beepsPathsArray)
            {
                string[] beepsArray = Directory.GetFiles(beepPath, "??Beep.*", SearchOption.TopDirectoryOnly)
                    //.Where(s => MainForm.SupportedExtensions.Contains(Path.GetExtension(s).ToLower())).ToArray();
                    .Where(s => MainForm.SupportedExtensions.Contains(Path.GetExtension(s), StringComparison.CurrentCultureIgnoreCase)).ToArray();

                if (beepsArray.Length == AmountOfBeepsPerFolder)
                {
                    _beepPairsList ??= [];

                    comboBoxBeepPair.Items.Add(beepPath.Remove(0, _beepsPath.Length + 1));
                    comboBoxBeepPair.Enabled = true;
                    _beepPairsList.Add(new BeepPair(beepsArray[0], beepsArray[1]));
                }
                else
                {
                    MessageBox.Show(string.Concat("Não foi encontrado o par de arquivos de beeps ou há arquivos em excesso.\n",
                        $"\nVerifique a pasta '{beepPath}'"),
                        "ATENÇÃO",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
        }

        public void SetVolumePercentage(byte beepVolume)
        {
            CurrentBeepsVolume = beepVolume;
        }

        public void SetTimeForResumeMusics(AxWindowsMediaPlayer beepMediaPlayer, float value)
        {
            IWMPMedia media = beepMediaPlayer.newMedia(HighBeepPath);
            HighBeepDuration = (int)media.duration;
            _justTimeForResumeMusics = value;

            TimeForResumeMusics = _justTimeForResumeMusics + HighBeepDuration;
        }

        public bool ChangeBeepPairSelection(int selectedIndex)
        {
            if (_beepPairsList == null || selectedIndex < 0 || selectedIndex > _beepPairsList.Count - 1)
                return false;

            CurrentBeepPairIndex = (byte)selectedIndex;
            _currentBeepPair = _beepPairsList[selectedIndex];

            return _currentBeepPair != null;
        }

        public string GetBtnConfigTestToolTipMsg()
        {
            string headerMessage = "\nRESULTADO ESPERADO DO TESTE:\n";

            int beforeBeeps = (int)TimeBeforePlayBeeps;
            int beepsAmount = AmountOfBeeps - 1;
            float sEachBeep = TimeForEachBeep;
            float sCountingBeeps = (float)(beepsAmount * sEachBeep);
            int sHighBeepDuration = HighBeepDuration;
            int sResumeMusics = (int)_justTimeForResumeMusics;
            int sTotalResumeMusics = sHighBeepDuration + sResumeMusics;

            bool isBeepsAmountPlural = beepsAmount > 1;
            string beepsAmountPlural = isBeepsAmountPlural ? "s" : "";
            char beepsAmountEorO = isBeepsAmountPlural ? 'e' : 'o';

            string secondMessage = string.Format("{0} segundo{1} d{2} beep{3} de contagem ({4} beep{5} de {6} segundo{7});",
                sCountingBeeps, sCountingBeeps > 1 ? "s" : "", beepsAmountEorO, beepsAmountPlural, beepsAmount, beepsAmountPlural, sEachBeep, sEachBeep > 1 ? "s" : "");

            CountingBeepsDescription = secondMessage;

            string thirdMessage = string.Format("{0} segundo{1} de espera final. Sendo: ({2} segundo{3} de duração do beep de início + {4} segundo{5} de \"Tempo para retomar músicas\")",
                sTotalResumeMusics, sTotalResumeMusics > 1 ? "s" : "", sHighBeepDuration, sHighBeepDuration > 1 ? "s" : "", sResumeMusics, sResumeMusics == 1 ? "" : "s");

            LastBeepsDescription = thirdMessage;

            if (beforeBeeps > 0)
                headerMessage = $"{headerMessage}\n - {beforeBeeps} segundo{(beforeBeeps > 1 ? "s" : "")} de espera do \"Tempo extra antes dos beeps\";";

            headerMessage = $"{headerMessage}\n - {secondMessage}\n - {thirdMessage}";

            return headerMessage;
        }

        public void TryPerformBeeps(AxWindowsMediaPlayer beepMediaPlayer, int timeToDecrement,
            in Label lblTestMessages, Action? finishCurrentIntervalCallback)
        {
            switch (_beepState)
            {
                case BeepState.WaitToRunBeeps:
                    _timerForEachBeepInMilliSec = TimerController.FromSecondsToMilliseconds(TimeForEachBeep);
                    _timerBeforePlayBeepsInMilliSec = TimerController.FromSecondsToMilliseconds(TimeBeforePlayBeeps);
                    _rechargeForEachBeepInMilliSec = _timerForEachBeepInMilliSec;
                    _timerForAllBeepsInMilliSec = _timerForEachBeepInMilliSec * (AmountOfBeeps - 1);
                    _timerForResumeMusicsInMilliSec = TimerController.FromSecondsToMilliseconds(TimeForResumeMusics);
                    _beepState = BeepState.WaitTimeBeforeBeeps;
                    _beepCounter = 0;
                    break;

                case BeepState.WaitTimeBeforeBeeps:
                    if (TimerController.PerformCountdown(ref _timerBeforePlayBeepsInMilliSec, ref _timerForEachBeepInMilliSec,
                        in _timerBeforePlayBeepsInMilliSec, timeToDecrement, true))
                    {
                        _timerBeforePlayBeepsInMilliSec = 0;
                        _beepState = BeepState.PerformBeeps;
                        _beepCounter++;
                        //_lblCompetitionTotalTime.Text = $"WaitTimeBeforeBeeps - Beep {_beepCounter}";
                        PlayBeep(beepMediaPlayer, LowBeepPath);
                    }
                    break;

                case BeepState.PerformBeeps:
                    /*void eachBeepCountdown()
                    {
                        if (TimerController.PerformCountdown(ref _timerForEachBeepInMilliSec, ref _timerForEachBeepInMilliSec,
                            _rechargeForEachBeepInMilliSec, timeToDecrement, false))
                        {
                            _beepCounter++;
                            //_lblCompetitionTotalTime.Text = $"PerformBeeps - Beep {_beepCounter}";
                            PlayBeep(beepMediaPlayer, _beepCounter < AmountOfBeeps ? LowBeepPath : HighBeepPath);
                        }
                    }*/

                    if (TimerController.PerformCountdown(ref _timerForEachBeepInMilliSec, ref _timerForEachBeepInMilliSec,
                        _rechargeForEachBeepInMilliSec, timeToDecrement, false))
                    {
                        _beepCounter++;
                        if (_beepCounter < AmountOfBeeps)
                        {
                            PlayBeep(beepMediaPlayer, LowBeepPath);
                        }
                        else
                        {
                            PlayBeep(beepMediaPlayer, HighBeepPath);
                            finishCurrentIntervalCallback?.Invoke();
                            _beepState = BeepState.TimeForResumeMusicsVolume;
                        }
                    }

                    // All beeps countdown
                    if (TimerController.PerformCountdown(ref _timerForAllBeepsInMilliSec, ref _timerForResumeMusicsInMilliSec,
                        in _timerForAllBeepsInMilliSec, timeToDecrement, true))
                    {
                        _timerForAllBeepsInMilliSec = 0;
                        //_beepState = BeepState.TimeForResumeMusicsVolume;
                        //_lblCompetitionTotalTime.Text = $"All beeps countdown";
                        //finishCurrentIntervalCallback?.Invoke();
                    }
                    break;

                case BeepState.TimeForResumeMusicsVolume:
                    if (TimerController.PerformCountdown(ref _timerForResumeMusicsInMilliSec, ref _timerForResumeMusicsInMilliSec,
                        in _timerForResumeMusicsInMilliSec, timeToDecrement, true))
                    {
                        _timerForResumeMusicsInMilliSec = 0;
                        CancelBeepsEvent();
                    }
                    break;

                default:
                    CancelBeepsEvent();
                    break;
            }

            if (lblTestMessages.Visible)
            {
                lblTestMessages.Text = string.Concat(
                    $"Antes dos beeps {GetTimeSpanFormat(_timerBeforePlayBeepsInMilliSec)} | ",
                    $"Até último beep {GetTimeSpanFormat(_timerForAllBeepsInMilliSec)} | ",
                    $"Após beeps {GetTimeSpanFormat(_timerForResumeMusicsInMilliSec)}");
            }
        }

        public void CancelBeepsEvent()
        {
            CanPerformBeepsEvent = false;
            _beepState = BeepState.WaitToRunBeeps;
        }

        public void PlayBeep(AxWindowsMediaPlayer beepMediaPlayer, string beepPath)
        {
            beepMediaPlayer.Ctlcontrols.stop();
            beepMediaPlayer.URL = beepPath;
        }

        private string GetTimeSpanFormat(double value) => TimeSpan.FromMilliseconds(value).ToString(@"ss\.fff");
    }
}
