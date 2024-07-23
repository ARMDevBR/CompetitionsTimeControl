using AxWMPLib;
using WMPLib;

namespace CompetitionsTimeControl.Controllers
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

        private enum BeepState { WaitToRunBeeps, WaitTimeBeforeBeeps, PerformBeeps, TimeForResumeMusicsVolume }

        public string HighBeepPath => _currentBeepPair?.HighBeepPath ?? "";
        public string LowBeepPath => _currentBeepPair?.LowBeepPath ?? "";

        public float TimeBeforePlayBeeps { get; set; }
        public int AmountOfBeeps { get; set; }
        public float TimeForEachBeep { get; set; }
        public float TimeForResumeMusics { get; private set; }

        public int TotalTimeBeforeLastBeepInMs =>
             (int)((TimeForEachBeep * (AmountOfBeeps - 1) + TimeBeforePlayBeeps) * 1000);

        public int HighBeepDuration { get; private set; }
        public bool CanPerformBeeps { get; set; }

        private readonly string _beepsPath;

        private int _timerBeforePlayBeepsInMiliSec;
        private int _timerForAllBeepsInMiliSec;
        private int _timerForEachBeepInMiliSec;
        private int _rechargeForEachBeepInMiliSec;
        private int _timerForResumeMusicsInMiliSec;
        private int _beepCounter;
        private float _justTimeForResumeMusics;

        private BeepState _beepState;
        private BeepPair? _currentBeepPair;
        private List<BeepPair>? _beepPairsList;

        public BeepsController(ComboBox comboBoxBeepPair)
        {
            _beepState = BeepState.WaitToRunBeeps;
            _beepsPath = string.Concat(AppDomain.CurrentDomain.BaseDirectory, DefaultBeepsFolderPath);
            GetBeepsSounds(comboBoxBeepPair);
        }

        private void GetBeepsSounds(ComboBox comboBoxBeepPair)
        {
            if (!Directory.Exists(_beepsPath))
            {
                MessageBox.Show(string.Concat("Não foi encontrado a pasta raiz com as patas dos pares de beeps.\n",
                    $"\nVerifique a pasta '{AppDomain.CurrentDomain.BaseDirectory}'.",
                    $"\nA pasta '{DefaultBeepsFolderPath}' deve existir neste local."),
                    "ATENÇÃO",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

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
                    MessageBox.Show(string.Concat("Não foi encontrado o par de arquivos de beeps ou há arquivos em excesso.\n",
                        $"\nVerifique a pasta '{beepPath}'"),
                        "ATENÇÃO",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                }
            }
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

            string thirdMessage = string.Format("{0} segundo{1} de espera final. Sendo:\n     ({2} segundo{3} de duração do beep de início + {4} segundo{5} de \"Tempo para retomar músicas\")",
                sTotalResumeMusics, sTotalResumeMusics > 1 ? "s" : "", sHighBeepDuration, sHighBeepDuration > 1 ? "s" : "", sResumeMusics, sResumeMusics == 1 ? "" : "s");

            if (beforeBeeps > 0)
                headerMessage = $"{headerMessage}\n - {beforeBeeps} segundo{(beforeBeeps > 1 ? "s" : "")} de espera do \"Tempo extra antes dos beeps\";";

            headerMessage = $"{headerMessage}\n - {secondMessage}\n - {thirdMessage}";

            return headerMessage;
        }

        public void TryPerformBeeps(AxWindowsMediaPlayer beepMediaPlayer, int timeToDecrement, in Label lblTestMessages)
        {
            switch (_beepState)
            {
                case BeepState.WaitToRunBeeps:
                    _beepState = BeepState.WaitTimeBeforeBeeps;
                    _timerBeforePlayBeepsInMiliSec = TimerController.FromSecondsToMiliseconds((int)TimeBeforePlayBeeps);
                    _timerForEachBeepInMiliSec = TimerController.FromSecondsToMiliseconds((int)TimeForEachBeep);
                    _rechargeForEachBeepInMiliSec = _timerForEachBeepInMiliSec;
                    _timerForAllBeepsInMiliSec = _timerForEachBeepInMiliSec * (AmountOfBeeps - 1);
                    _timerForResumeMusicsInMiliSec = TimerController.FromSecondsToMiliseconds((int)TimeForResumeMusics);
                    _beepCounter = 0;
                    break;

                case BeepState.WaitTimeBeforeBeeps:
                    if (TimerController.PerformCountdown(ref _timerBeforePlayBeepsInMiliSec, ref _timerForEachBeepInMiliSec,
                        in _timerBeforePlayBeepsInMiliSec, timeToDecrement))
                    {
                        _timerBeforePlayBeepsInMiliSec = 0;
                        _beepState = BeepState.PerformBeeps;
                        _beepCounter++;
                        PlayBeep(beepMediaPlayer, LowBeepPath);
                    }
                    break;

                case BeepState.PerformBeeps:
                    void eachBeepCountdown()
                    {
                        if (TimerController.PerformCountdown(ref _timerForEachBeepInMiliSec, ref _timerForEachBeepInMiliSec,
                            _rechargeForEachBeepInMiliSec, timeToDecrement))
                        {
                            _beepCounter++;
                            PlayBeep(beepMediaPlayer, _beepCounter < AmountOfBeeps ? LowBeepPath : HighBeepPath);
                        }
                    }

                    // All beeps countdown
                    if (TimerController.PerformCountdown(ref _timerForAllBeepsInMiliSec, ref _timerForResumeMusicsInMiliSec,
                        in _timerForAllBeepsInMiliSec, timeToDecrement, eachBeepCountdown))
                    {
                        _timerForAllBeepsInMiliSec = 0;
                        _beepState = BeepState.TimeForResumeMusicsVolume;
                    }
                    break;

                case BeepState.TimeForResumeMusicsVolume:
                    if (TimerController.PerformCountdown(ref _timerForResumeMusicsInMiliSec, ref _timerForResumeMusicsInMiliSec,
                        in _timerForResumeMusicsInMiliSec, timeToDecrement))
                    {
                        _timerForResumeMusicsInMiliSec = 0;
                        CanPerformBeeps = false;
                        _beepState = BeepState.WaitToRunBeeps;
                    }
                    break;

                default:
                    CanPerformBeeps = false;
                    _beepState = BeepState.WaitToRunBeeps;
                    break;
            }

            if (lblTestMessages.Visible)
            {
                lblTestMessages.Text = string.Concat(
                    $"Antes dos beeps {GetTimeSpanFormat(_timerBeforePlayBeepsInMiliSec)} | ",
                    $"Até último beep {GetTimeSpanFormat(_timerForAllBeepsInMiliSec)} | ",
                    $"Após beeps {GetTimeSpanFormat(_timerForResumeMusicsInMiliSec)}");
            }
        }

        public void PlayBeep(AxWindowsMediaPlayer beepMediaPlayer, string beepPath)
        {
            beepMediaPlayer.Ctlcontrols.stop();
            beepMediaPlayer.URL = beepPath;
        }

        private string GetTimeSpanFormat(double value) => TimeSpan.FromMilliseconds(value).ToString(@"ss\.fff");
    }
}
