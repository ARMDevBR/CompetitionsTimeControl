﻿using AxWMPLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CompetitionsTimeControl.Controllers
{

    internal class CompetitionController
    {
        private const byte MaxAmountIntervals = 100; //Max amount to remove slash spaces.
        private const int ProgressBarValueOffset = 550;

        public enum CompetitionProgram { OnlyBeeps, MusicsAndBeeps }
        private enum InitializationProgram { FromMusicPlaying, FromListBeginning }
        private enum PlaylistRestart { ShuffleList, SequentialList, InvertLastMode }
        private enum CompetitionProgramState
        {
            WaitToResumeProgram, WaitToRunProgram, WaitToAdjustMusicVolumeToMin, AdjustCurrentMusicVolumeToMin,
            WaitToPerformBeeps, WaitToAdjustMusicVolumeToMax, AdjustCurrentMusicVolumeToMax, FinishCompetition
        }

        public byte TimeToChangeVolume { get; set; }
        public bool StartWithBeeps { get; set; }
        public bool StopMusicsAtEnd { get; set; }
        public byte CompetitionAmountIntervals { get; private set; }
        public int CompetitionIntervalSeconds { get; private set; }
        public bool CanRunCompetition { get; private set; }
        public bool ProgramIsRunning { get; private set; }
        public CompetitionProgram CompetitionProgramSetup { get; private set; }

        private byte _nextTimeToChangeVolume;
        private byte _currentInterval;
        private bool _canAdjustMusicVolumeToMin;
        private int _timerAdjustMusicVolumeToMin;
        private int _timerCompetitionIntervalInMs;
        private int _timerCompetitionTotalInMs;
        private int _rechargeTimerAdjMusicVolToMin;
        private int _rechargeCompetitionIntervalInMs;
        private int _competitionIntervalsInMs;
        private int _competitionTotalTimeInMs;
        private CompetitionProgramState _programState;
        private CompetitionProgramState _lastProgramState;
        private InitializationProgram _initializationProgram;
        private readonly Label _lblCompetitionTotalTime = null!;
        private readonly Label _lblIntervalsElapsed = null!;
        private readonly Label _lblCompetitionElapsedTime = null!;
        private readonly ProgressBar _pbCurrentIntervalElapsed = null!;
        private readonly ProgressBar _pbCompetitionElapsedTime = null!;
        private PlaylistRestart _playlistRestart;

        public CompetitionController(in Label lblCompetitionTotalTime, in ProgressBar pbCurrentIntervalElapsed,
            in Label lblIntervalsElapsed, in ProgressBar pbCompetitionElapsedTime, in Label lblCompetitionElapsedTime)
        {
            _canAdjustMusicVolumeToMin = true;
            _programState = CompetitionProgramState.WaitToRunProgram;
            _lblCompetitionTotalTime = lblCompetitionTotalTime;
            _pbCurrentIntervalElapsed = pbCurrentIntervalElapsed;
            _lblIntervalsElapsed = lblIntervalsElapsed;
            _pbCompetitionElapsedTime = pbCompetitionElapsedTime;
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
            string timeStr = TimeSpan.FromSeconds(amountOfSeconds).ToString("c"); // "c" = 00:00:00, hh:mm:ss
            _lblCompetitionTotalTime.Text = $"Tempo total da competição {timeStr}";
        }

        public void ChangeCompetitionProgramSelection(int selectedIndex)
        {
            if (selectedIndex < 0 || selectedIndex > Enum.GetNames(typeof(CompetitionProgram)).Length - 1)
                return;

            CompetitionProgramSetup = (CompetitionProgram)selectedIndex;
        }

        public void ChangeInitializationProgramSelection(int selectedIndex, AxWindowsMediaPlayer musicMediaPlayer)
        {
            if (selectedIndex < 0 || selectedIndex > Enum.GetNames(typeof(InitializationProgram)).Length - 1)
                return;

            _initializationProgram = (InitializationProgram)selectedIndex;
        }

        public void ChangePlaylistRestartSelection(int selectedIndex)
        {
            if (selectedIndex < 0 || selectedIndex > Enum.GetNames(typeof(PlaylistRestart)).Length - 1)
                return;

            _playlistRestart = (PlaylistRestart)selectedIndex;
        }

        public bool ValidateBeepsEventTimes(BeepsController beepsController)
        {
            byte TwiceTimeToChangeVolume = (byte)(CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps
                ? 2 * TimeToChangeVolume : 0);
            float totalTimeForAllBeepEvent = beepsController.TotalTimeForAllBeepEventInMs;

            totalTimeForAllBeepEvent = TimerController.FromMillisecondsToSeconds((int)totalTimeForAllBeepEvent);

            float TimesToChangeVolumePlusAllBeepEvent = (float)Math.Round(
                (double)(totalTimeForAllBeepEvent + TwiceTimeToChangeVolume), 2);

            string sbFinalString = CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps
                ? " com os volumes do campo 'Tempo troca de volume'" : "";

            if (CompetitionIntervalSeconds < TimesToChangeVolumePlusAllBeepEvent)
            {
                StringBuilder sb = new(string.Concat($"O valor do campo 'Tempo de cada intervalo' ({CompetitionIntervalSeconds}",
                    $" segundos) NÃO pode ser menor que a somatória dos tempos do evento dos beeps{sbFinalString}!\n\n"));

                if (beepsController.TimeBeforePlayBeeps > 0)
                    sb.AppendLine($"  ➤  {beepsController.TimeBeforePlayBeeps.ToString()} segundos do 'Tempo extra antes dos beeps'");

                beepsController.GetBtnConfigTestToolTipMsg(); // Load "CountingBeepsDescription" and "LastBeepsDescription"
                sb.AppendLine($"  ➤  {beepsController.CountingBeepsDescription}");
                sb.AppendLine($"  ➤  {beepsController.LastBeepsDescription}");

                if (CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps)
                {
                    sb.Append($"  ➤  {TwiceTimeToChangeVolume} segundos (2x de {TimeToChangeVolume} segs) das transições de ");
                    sb.AppendLine($"volume para o mínimo (antes dos beeps) e para o máximo (ao finalizar os beeps)");
                }
                sb.AppendLine($"  ➤  TEMPO TOTAL: {TimesToChangeVolumePlusAllBeepEvent} segundos");

                sb.Append("\nAjuste os campos necessários para que a temporização fique adequada.");

                MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return false;
            }

            return true;
        }

        public bool CanStartCompetition()
        {
            bool ret = false;

            CanRunCompetition = false;
            ProgramIsRunning = false;

            string message = "As configurações estão prontas para começar a competição?";

            if (MessageBox.Show(message, "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                ret = true;
            }

            return ret;
        }

        public void StartCompetition(AxWindowsMediaPlayer musicMediaPlayer, MusicsController musicsController, out Action? finishCurrentIntervalCallback)
        {
            finishCurrentIntervalCallback = null;

            if (CanRunCompetition || ProgramIsRunning)
                return;
            
            if (CompetitionProgramSetup == CompetitionProgram.OnlyBeeps)
                musicMediaPlayer.currentPlaylist.clear();
            else
                CreatePlaylistByInitializationProgram(musicMediaPlayer, musicsController);
            
            _currentInterval = 0;
            AdjustLabelIntervalsElapsed();
            _lblCompetitionElapsedTime.Text = "00:00:00";
            _pbCurrentIntervalElapsed.Value = 0;
            _pbCompetitionElapsedTime.Value = 0;
            _lblIntervalsElapsed.BackColor = Color.LimeGreen;
            _programState = CompetitionProgramState.WaitToRunProgram;
            CanRunCompetition = true;
            finishCurrentIntervalCallback = FinishCurrentInterval;
        }

        private void CreatePlaylistByInitializationProgram(AxWindowsMediaPlayer musicMediaPlayer,
            MusicsController musicsController)
        {
            musicsController.CreatePlaylist(musicMediaPlayer,
                _initializationProgram == InitializationProgram.FromListBeginning);
        }

        public bool StopCompetition()
        {
            StringBuilder sb = new("Tem certeza que deseja encerrar a programação?\n\n");
            sb.Append("Todo o processo da competição será parado e os controles liberados para alterações.");

            if (MessageBox.Show(sb.ToString(), "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Warning,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
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

        private void FinishCurrentInterval()
        {
            if (_currentInterval < CompetitionAmountIntervals)
                _currentInterval++;

            if (_currentInterval != CompetitionAmountIntervals)
                _canAdjustMusicVolumeToMin = false;

            AdjustLabelIntervalsElapsed();
        }

        public void TryPerformCompetition(BeepsController beepsController, MusicsController musicsController,
            Action clearPlaylistAndStopMusicCallback, Action formStopCompetitionCallback, int timeToDecrement)
        {
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

                    if (!ProgramIsRunning)
                    {
                        float amountOfSeconds = CompetitionAmountIntervals * CompetitionIntervalSeconds;
                        _competitionTotalTimeInMs = TimerController.FromSecondsToMilliseconds(amountOfSeconds);
                        _timerCompetitionTotalInMs = _competitionTotalTimeInMs;
                        _competitionIntervalsInMs = TimerController.FromSecondsToMilliseconds(CompetitionIntervalSeconds);
                        _timerCompetitionIntervalInMs = _competitionIntervalsInMs;
                        _rechargeCompetitionIntervalInMs = _competitionIntervalsInMs;
                        _timerAdjustMusicVolumeToMin = _competitionIntervalsInMs - beepsController.TotalTimeBeforeLastBeepInMs;

                        if (CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps)
                            _timerAdjustMusicVolumeToMin -= TimerController.FromSecondsToMilliseconds(TimeToChangeVolume);

                        if (StartWithBeeps)
                        {
                            _currentInterval = 0;
                            _nextTimeToChangeVolume = 0;
                            _canAdjustMusicVolumeToMin = true;
                            beepsController.TriggerCompetitionStartWithBeeps = true;
                        }
                        else
                        {
                            FinishCurrentInterval(); // Start in 1;
                        }

                        _rechargeTimerAdjMusicVolToMin = _timerAdjustMusicVolumeToMin;
                        _pbCurrentIntervalElapsed.Maximum = _competitionIntervalsInMs;
                        _pbCompetitionElapsedTime.Maximum = _competitionTotalTimeInMs;
                    }

                    ProgramIsRunning = true;
                    _programState = CompetitionProgramState.WaitToAdjustMusicVolumeToMin;
                    break;
                
                case CompetitionProgramState.WaitToAdjustMusicVolumeToMin:
                    if (!(_canAdjustMusicVolumeToMin && _currentInterval < CompetitionAmountIntervals))
                        break;
                    
                    if (CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps)
                        musicsController.ChangeVolumeInSeconds(_nextTimeToChangeVolume, MusicsController.ChangeVolume.ToMin);
                        
                    _programState = CompetitionProgramState.AdjustCurrentMusicVolumeToMin;
                    break;
                
                case CompetitionProgramState.AdjustCurrentMusicVolumeToMin:
                    if (musicsController.SecondsToChangeVolume != null)
                        break;
                    
                    _programState = CompetitionProgramState.WaitToPerformBeeps;
                    break;
                
                case CompetitionProgramState.WaitToPerformBeeps:
                    beepsController.CanPerformBeepsEvent = true;
                    _nextTimeToChangeVolume = TimeToChangeVolume;
                    _programState = CompetitionProgramState.WaitToAdjustMusicVolumeToMax;
                    _lblIntervalsElapsed.BackColor = Color.Yellow;
                    break;

                case CompetitionProgramState.WaitToAdjustMusicVolumeToMax:
                    if (beepsController.CanPerformBeepsEvent)
                        break;
                    
                    if (CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps)
                        musicsController.ChangeVolumeInSeconds(_nextTimeToChangeVolume, MusicsController.ChangeVolume.ToMax);
                        
                    _programState = CompetitionProgramState.AdjustCurrentMusicVolumeToMax;
                    _lblIntervalsElapsed.BackColor = Color.LimeGreen;
                    break;

                case CompetitionProgramState.AdjustCurrentMusicVolumeToMax:
                    if (musicsController.SecondsToChangeVolume != null)
                        break;
                    
                    _programState = CompetitionProgramState.WaitToRunProgram;
                    break;

                case CompetitionProgramState.FinishCompetition:
                    ProgramIsRunning = false;
                    CanRunCompetition = false;
                    _programState = CompetitionProgramState.WaitToRunProgram;

                    if (CompetitionProgramSetup == CompetitionProgram.MusicsAndBeeps)
                        musicsController.ChangeVolumeInSeconds(0, MusicsController.ChangeVolume.ToMax);

                    if (StopMusicsAtEnd)
                        clearPlaylistAndStopMusicCallback();
                    
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

            int intervalElapsedInMs = Math.Clamp(_competitionIntervalsInMs - _timerCompetitionIntervalInMs,
                0, _pbCurrentIntervalElapsed.Maximum);

            _pbCurrentIntervalElapsed.Value = Math.Clamp(intervalElapsedInMs + ProgressBarValueOffset,
                0, _pbCurrentIntervalElapsed.Maximum);

            if (!_canAdjustMusicVolumeToMin && TimerController.PerformCountdown(ref _timerAdjustMusicVolumeToMin,
                ref _timerAdjustMusicVolumeToMin, _rechargeTimerAdjMusicVolToMin, timeToDecrement))
            {
                _canAdjustMusicVolumeToMin = true;
            }

            if (ProgramIsRunning)
            {
                TimerController.PerformCountdown(ref _timerCompetitionIntervalInMs, ref _timerCompetitionIntervalInMs,
                    _rechargeCompetitionIntervalInMs, timeToDecrement);
            }

            if (TimerController.PerformCountdown(ref _timerCompetitionTotalInMs, ref _timerCompetitionTotalInMs,
                _timerCompetitionTotalInMs, timeToDecrement))
            {
                _timerCompetitionIntervalInMs = 0;
                _timerCompetitionTotalInMs = 0;
                _programState = CompetitionProgramState.FinishCompetition;
            }

            _lblCompetitionElapsedTime.Text = TimeSpan.FromMilliseconds(competitionElapsedTimeInMs).ToString(@"hh\:mm\:ss");

            if (!CanRunCompetition)
            {
                string message = "Os controles estão liberados novamente.";
                formStopCompetitionCallback();
                MessageBox.Show(message, "COMPETIÇÃO CONCLUÍDA COM SUCESSO", MessageBoxButtons.OK);
            }
        }
    }
}
