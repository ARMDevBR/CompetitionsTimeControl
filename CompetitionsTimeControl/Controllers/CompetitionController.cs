using AxWMPLib;
using Newtonsoft.Json;
using System.Text;

namespace CompetitionsTimeControl.Controllers
{
    [JsonObject(MemberSerialization.OptIn)]
    internal class CompetitionController
    {
        private const byte MaxAmountIntervals = 100; //Max amount to remove slash spaces.
        private const int ProgressBarValueOffset = 550;
        private const int OffsetTimerToStartBeepsBeforeInMs = 150;

        public enum CompetitionProgram { None = -1, OnlyBeeps, MusicsAndBeeps }
        public enum InitializationProgram { None = -1, FromListBeginning, FromMusicPlaying }
        public enum PlaylistRestart { None = -1, SequentialList, ShuffleList, InvertLastMode }
        private enum CompetitionProgramState
        {
            WaitToResumeProgram, WaitToRunProgram, WaitToAdjustMusicVolumeToMin, AdjustCurrentMusicVolumeToMin,
            StartBeepsEvent, WaitToAdjustMusicVolumeToMax, AdjustCurrentMusicVolumeToMax, FinishCompetition
        }

        [JsonProperty] public CompetitionProgram CompetitionProgramSetup { get; private set; }
        [JsonProperty] public InitializationProgram InitializationProgramOption { get; private set; }
        [JsonProperty] public PlaylistRestart PlaylistRestartMode { get; private set; }
        [JsonProperty] public byte TimeToChangeVolume { get; set; }
        [JsonProperty] public bool StartWithBeeps { get; set; }
        [JsonProperty] public bool StopMusicsAtEnd { get; set; }
        [JsonProperty] public byte CompetitionAmountIntervals { get; private set; }
        [JsonProperty] public int CompetitionIntervalSeconds { get; private set; }

        public bool IsPreparationComplete { get; private set; }
        public bool CanRunCompetition { get; private set; }
        public bool ProgramIsRunning { get; private set; }
        public bool CanPerformHalfIntervalBeep { get; private set; }

        private byte _nextTimeToChangeVolume;
        private byte _currentInterval;
        private bool _showFinishMessage;
        private bool _halfIntervalBeepWasPerformed;
        private int _timerAdjustMusicVolumeToMinInMs;
        private int _timerCompetitionIntervalInMs;
        private int _halfCompetitionIntervalInMs;
        private int _timerCompetitionTotalInMs;
        private int _competitionIntervalsInMs;
        private int _competitionTotalTimeInMs;
        private long _lastMillisecond;
        private CompetitionProgramState _programState;
        private CompetitionProgramState _lastProgramState;
        private readonly Label _lblIntervalAndCompetitionFormattedTime = null!;
        private readonly Label _lblIntervalsElapsed = null!;
        private readonly Label _lblCurrIntervalsElapsedTime = null!;
        private readonly Label _lblCompetitionElapsedTime = null!;
        private readonly ProgressBar _pbCurrentIntervalElapsed = null!;
        private readonly ProgressBar _pbCompetitionElapsedTime = null!;

        public CompetitionController(in Label lblIntervalAndCompetitionFormattedTime, in ProgressBar pbCurrentIntervalElapsed,
            in Label lblIntervalsElapsed, in ProgressBar pbCompetitionElapsedTime, in Label lblCurrIntervalsElapsedTime,
            in Label lblCompetitionElapsedTime)
        {
            CompetitionProgramSetup = CompetitionProgram.None;
            InitializationProgramOption = InitializationProgram.None;
            PlaylistRestartMode = PlaylistRestart.None;
            IsPreparationComplete = false;
            _showFinishMessage = true;
            _programState = CompetitionProgramState.WaitToRunProgram;
            _lastMillisecond = 0;
            _lblIntervalAndCompetitionFormattedTime = lblIntervalAndCompetitionFormattedTime;
            _pbCurrentIntervalElapsed = pbCurrentIntervalElapsed;
            _lblIntervalsElapsed = lblIntervalsElapsed;
            _pbCompetitionElapsedTime = pbCompetitionElapsedTime;
            _lblCurrIntervalsElapsedTime = lblCurrIntervalsElapsedTime;
            _lblCompetitionElapsedTime = lblCompetitionElapsedTime;
        }

        public void SetCompetitionAmountIntervals(byte amountIntervals)
        {
            CompetitionAmountIntervals = amountIntervals;
            _currentInterval = 0;
            AdjustLabelIntervalsElapsed();
            AdjustLabelCompetitionTotalTime();
        }

        private void AdjustLabelIntervalsElapsed()
        {
            if (_currentInterval < MaxAmountIntervals)
                _lblIntervalsElapsed.Text = $"{_currentInterval} / {CompetitionAmountIntervals}";
            else
                _lblIntervalsElapsed.Text = $"{_currentInterval}/{CompetitionAmountIntervals}";
        }

        public void SetCompetitionIntervalSeconds(int IntervalSeconds)
        {
            CompetitionIntervalSeconds = IntervalSeconds;
            AdjustLabelCompetitionTotalTime();
        }

        private void AdjustLabelCompetitionTotalTime()
        {
            float amountOfSeconds = CompetitionAmountIntervals * CompetitionIntervalSeconds;
            string intervalTimeStr = TimeSpan.FromSeconds(CompetitionIntervalSeconds).ToString(@"mm\:ss"); // mm:ss
            string competitionTimeStr = TimeSpan.FromSeconds(amountOfSeconds).ToString("c"); // "c" = 00:00:00, hh:mm:ss
            _lblIntervalAndCompetitionFormattedTime.Text = $"Tiro de: {intervalTimeStr} - Prova de: {competitionTimeStr}";
        }

        public void ChangeCompetitionProgramSelection(int selectedIndex)
        {
            if (selectedIndex < -1 || selectedIndex > Enum.GetNames(typeof(CompetitionProgram)).Length - 2)
                return;

            CompetitionProgramSetup = (CompetitionProgram)selectedIndex;
        }

        public void ChangeInitializationProgramSelection(int selectedIndex)
        {
            if (selectedIndex < -1 || selectedIndex > Enum.GetNames(typeof(InitializationProgram)).Length - 2)
                return;

            InitializationProgramOption = (InitializationProgram)selectedIndex;
        }

        public void ChangePlaylistRestartSelection(int selectedIndex)
        {
            if (selectedIndex < -1 || selectedIndex > Enum.GetNames(typeof(PlaylistRestart)).Length - 2)
                return;

            PlaylistRestartMode = (PlaylistRestart)selectedIndex;
        }

        public bool HasTimeForHalfIntervalBeep(BeepsController beepsController)
        {
            if (!beepsController.HasHalfIntervalBeep)
                return true;

            bool isMusicAndBeeps = CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps;

            float totalHalfIntervalBeepTime = CompetitionIntervalSeconds * 0.5f + beepsController.LowBeepDuration + 1;

            float totalPreparationForNextIntervalTime = beepsController.TimeBeforePlayBeeps +
                beepsController.TimeForEachBeep * (beepsController.AmountOfBeeps - 1);

            if (isMusicAndBeeps)
                totalPreparationForNextIntervalTime += TimeToChangeVolume;

            float totalTime = totalHalfIntervalBeepTime + totalPreparationForNextIntervalTime;

            if (TimerController.FromSecondsToMilliseconds(totalTime) >
                TimerController.FromSecondsToMilliseconds(CompetitionIntervalSeconds))
            {
                StringBuilder sb = new("NÃO haverá tempo suficiente para o uso do 'Beep de meio intervalo'!\n\n");

                sb.AppendLine("A somatória dos campos abaixo:\n");

                sb.AppendLine($"  ➤  {CompetitionIntervalSeconds * 0.5f:F2} segundos para iniciar o 'Beep de meio intervalo'.");
                sb.AppendLine($"       (Metade de {CompetitionIntervalSeconds} segundos do 'Tempo de cada intervalo').\n");
                sb.AppendLine($"  ➤  {beepsController.LowBeepDuration} segundo(s) do som do beep.");
                sb.AppendLine($"  ➤  1 segundo de folga para concluir o beep.");

                if (isMusicAndBeeps)
                    sb.AppendLine($"  ➤  {TimeToChangeVolume} segundos para reduzir o volume da música.");

                if (beepsController.TimeBeforePlayBeeps > 0)
                    sb.AppendLine($"  ➤  {beepsController.TimeBeforePlayBeeps} segundo(s) do 'Tempo extra antes dos beeps'");
                
                sb.AppendLine($"  ➤  {beepsController.CountingBeepsDescription}");
                sb.AppendLine($"  ➤  TEMPO TOTAL: {totalTime:F2} segundos\n");

                sb.AppendLine($"NÃO pode ser MAIOR que {CompetitionIntervalSeconds} segundos do 'Tempo de cada intervalo'.\n");

                sb.AppendLine("Faça uma ou mais destas sugestões abaixo para corrigir o problema:\n");

                sb.AppendLine("  ➤  Diminua o 'Tempo extra antes dos beeps'.");
                sb.AppendLine("  ➤  Diminua a 'Quantidade de beeps'.");
                sb.AppendLine("  ➤  Diminua o 'Tempo para cada beep'.");
                
                if (isMusicAndBeeps)
                    sb.AppendLine("  ➤  Diminua o 'Tempo troca de volume'.");
                
                sb.AppendLine("  ➤  Aumente o 'Tempo de cada intervalo'.\n");

                sb.Append("Se não for utilizar o 'Beep de meio intervalo', desative a opção.");

                MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            return true;
        }

        public bool ValidateBeepsEventTimes(BeepsController beepsController)
        {
            byte twiceTimeToChangeVolume = (byte)(CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps
                ? 2 * TimeToChangeVolume : 0);
            float totalTimeForAllBeepEvent = beepsController.TotalTimeForAllBeepEventInMs;

            totalTimeForAllBeepEvent = TimerController.FromMillisecondsToSeconds((int)totalTimeForAllBeepEvent);

            float TimesToChangeVolumePlusAllBeepEvent = (float)Math.Round(
                (double)(totalTimeForAllBeepEvent + twiceTimeToChangeVolume), 2);

            string sbFinalString = CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps
                ? " com os volumes do campo 'Tempo troca de volume'" : "";

            beepsController.GetBtnConfigTestToolTipMsg(); // Load "CountingBeepsDescription" and "LastBeepsDescription"

            if (CompetitionIntervalSeconds < TimesToChangeVolumePlusAllBeepEvent)
            {
                StringBuilder sb = new(string.Concat($"O valor do campo 'Tempo de cada intervalo' ({CompetitionIntervalSeconds}",
                    $" segundos) NÃO pode ser menor que a somatória dos tempos do evento dos beeps{sbFinalString}!\n\n"));

                if (beepsController.TimeBeforePlayBeeps > 0)
                    sb.AppendLine($"  ➤  {beepsController.TimeBeforePlayBeeps} segundo(s) do 'Tempo extra antes dos beeps'");

                sb.AppendLine($"  ➤  {beepsController.CountingBeepsDescription}");
                sb.AppendLine($"  ➤  {beepsController.LastBeepsDescription}");

                if (CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps)
                {
                    sb.Append($"  ➤  {twiceTimeToChangeVolume} segundos (2x de {TimeToChangeVolume} segs) das transições de ");
                    sb.AppendLine($"volume para o mínimo (antes dos beeps) e para o máximo (ao finalizar os beeps)");
                }
                sb.AppendLine($"  ➤  TEMPO TOTAL: {TimesToChangeVolumePlusAllBeepEvent} segundos");

                sb.Append("\nAjuste os campos necessários para que a temporização fique adequada.");

                MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        public bool ValidateHasMusicsToPlaylist(MusicsController musicsController,
            out bool skipCanStartMessage)
        {
            skipCanStartMessage = false;

            return CompetitionProgramSetup == CompetitionProgram.OnlyBeeps ||
                musicsController.HasMusicsToPlaylist(out skipCanStartMessage);
        }

        public bool CanStartCompetition(bool skipCanStartMessage)
        {
            bool ret = false;

            IsPreparationComplete = false;
            CanRunCompetition = false;
            ProgramIsRunning = false;

            string message = "As configurações estão prontas para começar a competição?";

            if (skipCanStartMessage || MessageBox.Show(message, "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ret = true;
            }

            return ret;
        }

        public void BlockListviewAndCreatePlaylist(AxWindowsMediaPlayer musicMediaPlayer, MusicsController musicsController)
        {
            if (CanRunCompetition || ProgramIsRunning)
                return;

            musicsController.RepaintListViewGrid(false);

            if (CompetitionProgramSetup == CompetitionProgram.OnlyBeeps)
                MusicsController.DisablePlayerAndClearPlaylist(musicMediaPlayer);
            else
            {
                musicsController.CreatePlaylist(musicMediaPlayer,
                    InitializationProgramOption == InitializationProgram.FromListBeginning);
            }
        }

        public void PrepareToStart(out Action<bool>? finishCurrentIntervalCallback,
            out Action? setLblIntervalsElapsedYellowColor)
        {
            finishCurrentIntervalCallback = null;
            setLblIntervalsElapsedYellowColor = null;

            if (CanRunCompetition || ProgramIsRunning)
                return;

            _currentInterval = 0;
            AdjustLabelIntervalsElapsed();
            _lblCurrIntervalsElapsedTime.Text = "00:00:00";
            _lblCompetitionElapsedTime.Text = "00:00:00";
            _pbCurrentIntervalElapsed.Value = 0;
            _pbCompetitionElapsedTime.Value = 0;
            _lblIntervalsElapsed.BackColor = Color.LimeGreen;
            _programState = CompetitionProgramState.WaitToRunProgram;
            _showFinishMessage = true;
            IsPreparationComplete = true;
            _halfIntervalBeepWasPerformed = false;
            finishCurrentIntervalCallback = FinishCurrentInterval;
            setLblIntervalsElapsedYellowColor = SetLblIntervalsElapsedYellowColor;
        }

        public void StartCompetition()
        {
            IsPreparationComplete = false;
            CanRunCompetition = true;
        }

        public bool StopCompetition()
        {
            StringBuilder sb = new("Tem certeza que deseja encerrar a programação?\n\n");
            sb.Append("Todo o processo da competição será parado e os controles liberados para alterações.");

            if (MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                _showFinishMessage = false;
                _programState = CompetitionProgramState.FinishCompetition;
                return true;
            }

            return false;
        }

        public void TogglePause()
        {
            if (ProgramIsRunning)
            {
                StringBuilder sb = new("Tem certeza que deseja pausar a competição?\n\n");
                sb.Append("Todas as tarefas da competição serão congeladas. Os controles NÃO serão liberados para alterações.");

                if (MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    ProgramIsRunning = false;
                    _programState = CompetitionProgramState.WaitToResumeProgram;
                }
            }
            else
            {
                StringBuilder sb = new("Deseja retomar a competição?\n\n");
                sb.Append("Todas as tarefas da competição continuarão do momento em que estavam.");

                if (MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button2) == DialogResult.Yes)
                {
                    _programState = _lastProgramState;
                    ProgramIsRunning = true;
                }
            }
        }

        private void FinishCurrentInterval(bool canPerformHalfIntervalBeep)
        {
            if (_currentInterval < CompetitionAmountIntervals)
                _currentInterval++;

            CanPerformHalfIntervalBeep = canPerformHalfIntervalBeep;
            AdjustLabelIntervalsElapsed();
        }

        private void SetLblIntervalsElapsedYellowColor() => _lblIntervalsElapsed.BackColor = Color.Yellow;

        public void KeepLastMillisecondUpdated(long elapsedMilliseconds) => _lastMillisecond = elapsedMilliseconds;

        public void TryPerformCompetition(BeepsController beepsController, MusicsController musicsController,
            Action<bool> chooseHowToClearPlaylist, Action formStopCompetitionCallback, Action performHalfBeepCallback,
            long elapsedMilliseconds)
        {
            long currentMillisecond = elapsedMilliseconds; // up to 2 trillion hours

            int timeToDecrement = (int)Math.Abs(currentMillisecond - _lastMillisecond);

            _lastMillisecond = currentMillisecond;

            if (!CanRunCompetition)
            {
                _programState = CompetitionProgramState.WaitToRunProgram;
                return;
            }

            switch (_programState)
            {
                case CompetitionProgramState.WaitToResumeProgram:
                    return;
                
                case CompetitionProgramState.WaitToRunProgram:
                    _nextTimeToChangeVolume = TimeToChangeVolume;
                    _programState = CompetitionProgramState.WaitToAdjustMusicVolumeToMin;

                    if (!ProgramIsRunning)
                    {
                        float amountOfSeconds = CompetitionAmountIntervals * CompetitionIntervalSeconds;
                        _competitionTotalTimeInMs = TimerController.FromSecondsToMilliseconds(amountOfSeconds);
                        _timerCompetitionTotalInMs = _competitionTotalTimeInMs;
                        _competitionIntervalsInMs = TimerController.FromSecondsToMilliseconds(CompetitionIntervalSeconds);
                        _timerCompetitionIntervalInMs = _competitionIntervalsInMs;
                        _timerAdjustMusicVolumeToMinInMs = _competitionIntervalsInMs -
                            beepsController.TotalTimeBeforeLastBeepInMs - OffsetTimerToStartBeepsBeforeInMs;

                        if (CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps)
                            _timerAdjustMusicVolumeToMinInMs -= TimerController.FromSecondsToMilliseconds(TimeToChangeVolume);

                        _pbCurrentIntervalElapsed.Maximum = _competitionIntervalsInMs;
                        _pbCompetitionElapsedTime.Maximum = _competitionTotalTimeInMs;

                        if (beepsController.HasHalfIntervalBeep)
                            _halfCompetitionIntervalInMs = (int)(_competitionIntervalsInMs * 0.5f);

                        ProgramIsRunning = true;

                        if (StartWithBeeps)
                        {
                            _currentInterval = 0;
                            _programState = CompetitionProgramState.StartBeepsEvent;
                            if (CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps)
                                musicsController.ChangeVolumeInSeconds(0, MusicsController.ChangeVolume.ToMin);
                        }
                        else
                        {
                            FinishCurrentInterval(beepsController.HasHalfIntervalBeep); // Start in 1;
                        }
                    }

                    break;
                
                case CompetitionProgramState.WaitToAdjustMusicVolumeToMin:
                    if (!(_timerCompetitionIntervalInMs > _timerAdjustMusicVolumeToMinInMs && _currentInterval < CompetitionAmountIntervals))
                        break;
                    
                    if (CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps)
                        musicsController.ChangeVolumeInSeconds(_nextTimeToChangeVolume, MusicsController.ChangeVolume.ToMin);
                        
                    _programState = CompetitionProgramState.AdjustCurrentMusicVolumeToMin;
                    break;
                
                case CompetitionProgramState.AdjustCurrentMusicVolumeToMin:
                    if (musicsController.SecondsToChangeVolume != null)
                        break;
                    
                    _programState = CompetitionProgramState.StartBeepsEvent;
                    break;
                
                case CompetitionProgramState.StartBeepsEvent:
                    beepsController.CanPerformBeepsEvent = true;
                    _nextTimeToChangeVolume = TimeToChangeVolume;
                    _programState = CompetitionProgramState.WaitToAdjustMusicVolumeToMax;
                    break;

                case CompetitionProgramState.WaitToAdjustMusicVolumeToMax:
                    if (beepsController.CanPerformBeepsEvent)
                        break;
                    
                    if (CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps)
                        musicsController.ChangeVolumeInSeconds(_nextTimeToChangeVolume, MusicsController.ChangeVolume.ToMax);
                        
                    _programState = CompetitionProgramState.AdjustCurrentMusicVolumeToMax;
                    break;

                case CompetitionProgramState.AdjustCurrentMusicVolumeToMax:
                    _lblIntervalsElapsed.BackColor = Color.LimeGreen;
                    if (musicsController.SecondsToChangeVolume != null)
                        break;
                    
                    _programState = CompetitionProgramState.WaitToRunProgram;
                    break;

                case CompetitionProgramState.FinishCompetition:
                    chooseHowToClearPlaylist(StopMusicsAtEnd);
                    ProgramIsRunning = false;
                    CanRunCompetition = false;
                    _programState = CompetitionProgramState.WaitToRunProgram;
                    break;
                
                default:
                    _programState = CompetitionProgramState.FinishCompetition;
                    break;
            }

            _lastProgramState = _programState;

            if (_currentInterval <= 0)
                return;
            
            int competitionElapsedTimeInMs = Math.Clamp(_competitionTotalTimeInMs - _timerCompetitionTotalInMs,
                0, _pbCompetitionElapsedTime.Maximum);

            _pbCompetitionElapsedTime.Value = Math.Clamp(competitionElapsedTimeInMs + ProgressBarValueOffset,
                0, _pbCompetitionElapsedTime.Maximum);

            int intervalElapsedTimeInMs = competitionElapsedTimeInMs -
                (_currentInterval - 1) * _pbCurrentIntervalElapsed.Maximum;

            _pbCurrentIntervalElapsed.Value = Math.Clamp(intervalElapsedTimeInMs + ProgressBarValueOffset,
                0, _pbCurrentIntervalElapsed.Maximum);

            _timerCompetitionIntervalInMs = intervalElapsedTimeInMs;

            if (beepsController.HasHalfIntervalBeep)
            {
                if (CanPerformHalfIntervalBeep)
                {
                    if (intervalElapsedTimeInMs >= _halfCompetitionIntervalInMs &&
                        !_halfIntervalBeepWasPerformed)
                    {
                        performHalfBeepCallback();
                        _halfIntervalBeepWasPerformed = true;

                        if (CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps && (
                            _programState == CompetitionProgramState.WaitToAdjustMusicVolumeToMin ||
                            _programState == CompetitionProgramState.AdjustCurrentMusicVolumeToMin))
                        {
                            musicsController.ChangeVolumeInSeconds(0, MusicsController.ChangeVolume.ToMin);
                        }
                    }
                    else if (intervalElapsedTimeInMs >= _halfCompetitionIntervalInMs +
                        TimerController.FromSecondsToMilliseconds(beepsController.LowBeepDuration))
                    {
                        CanPerformHalfIntervalBeep = false;
                        _halfIntervalBeepWasPerformed = false;
                        _lblIntervalsElapsed.BackColor = Color.LimeGreen;

                        if (CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps && (
                            _programState == CompetitionProgramState.WaitToAdjustMusicVolumeToMin ||
                            _programState == CompetitionProgramState.AdjustCurrentMusicVolumeToMin))
                        {
                            musicsController.ChangeVolumeInSeconds(0, MusicsController.ChangeVolume.ToMax);
                        }
                    }
                }
            }

            if (TimerController.PerformCountdown(ref _timerCompetitionTotalInMs, ref _timerCompetitionTotalInMs,
                _timerCompetitionTotalInMs, timeToDecrement, true))
            {
                _timerCompetitionIntervalInMs = 0;
                _timerCompetitionTotalInMs = 0;
                _programState = CompetitionProgramState.FinishCompetition;

                if (CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps)
                    musicsController.ChangeVolumeInSeconds(0, MusicsController.ChangeVolume.ToMax);
            }

            _lblCurrIntervalsElapsedTime.Text = TimeSpan.FromMilliseconds(intervalElapsedTimeInMs).ToString(@"hh\:mm\:ss");
            _lblCompetitionElapsedTime.Text = TimeSpan.FromMilliseconds(competitionElapsedTimeInMs).ToString(@"hh\:mm\:ss");

            if (!CanRunCompetition && _showFinishMessage)
            {
                string message = "Os controles estão liberados novamente.";
                formStopCompetitionCallback();
                MessageBox.Show(message, "COMPETIÇÃO CONCLUÍDA COM SUCESSO", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
            }
        }
    }
}
