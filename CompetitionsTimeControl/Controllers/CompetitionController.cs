using AxWMPLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompetitionsTimeControl.Controllers
{

    internal class CompetitionController
    {
        private const byte MaxAmountIntervals = 100; //Max amount to remove slash spaces.

        public enum CompetitionProgram { OnlyBeeps, MusicsAndBeeps }
        private enum InitializationProgram { FromMusicPlaying, FromListBeginning }
        private enum PlaylistRestart { ShuffleList, SequentialList, InvertLastMode }
        private enum CompetitionProgramState
        {
            WaitToResumeProgram, WaitToRunProgram, WaitToAdjustMusicVolumeToMin, AdjustCurrentMusicVolumeToMin,
            WaitToPerformBeeps, WaitToAdjustMusicVolumeToMax, AdjustCurrentMusicVolumeToMax
        }

        public byte TimeToChangeVolume { get; set; }
        public bool StartWithBeeps { get; set; }
        public bool StopMusicsAtEnd { get; set; }
        public byte CompetitionAmountIntervals { get; private set; }
        public int CompetitionIntervalSeconds { get; private set; }
        public bool CanStartCompetition { get; private set; }
        public bool ProgramIsRunning { get; private set; }
        public CompetitionProgram CompetitionProgramSetup { get; private set; }

        private byte _nextTimeToChangeVolume;
        private byte _currentInterval;
        private CompetitionProgramState _programState;
        private CompetitionProgramState _lastProgramState;
        private InitializationProgram _initializationProgram;
        private readonly Label _lblCompetitionTotalTime = null!;
        private readonly Label _lblIntervalsElapsed = null!;
        private readonly ProgressBar _pbCurrentIntervalElapsed = null!;
        private PlaylistRestart _playlistRestart;

        public CompetitionController(in Label lblCompetitionTotalTime,
            in ProgressBar pbCurrentIntervalElapsed, in Label lblIntervalsElapsed)
        {
            _programState = CompetitionProgramState.WaitToRunProgram;
            _lblCompetitionTotalTime = lblCompetitionTotalTime;
            _pbCurrentIntervalElapsed = pbCurrentIntervalElapsed;
            _lblIntervalsElapsed = lblIntervalsElapsed;
        }

        public void SetCompetitionAmountIntervals(byte amountIntervals)
        {
            CompetitionAmountIntervals = amountIntervals;
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
            double amountOfSeconds = CompetitionAmountIntervals * CompetitionIntervalSeconds;
            string timeStr = TimeSpan.FromSeconds(amountOfSeconds).ToString("c"); // "c" = 00:00:00, hh:mm:ss           
            _lblCompetitionTotalTime.Text = $"Tempo total da competição {timeStr}";
        }

        public void ChangeCompetitionProgramSelection(int selectedIndex)
        {
            if (selectedIndex < 0 || selectedIndex > Enum.GetNames(typeof(CompetitionProgram)).Length - 1)
                return;

            CompetitionProgramSetup = (CompetitionProgram)selectedIndex;
        }

        public void ChangeInitializationProgramSelection(int selectedIndex)
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

        public bool StartCompetition()
        {
            string message = "As configurações estão prontas para começar a competição?";

            CanStartCompetition = false;
            ProgramIsRunning = false;

            if (MessageBox.Show(message, "ATENÇÃO", MessageBoxButtons.YesNo, MessageBoxIcon.Question,
                MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                CanStartCompetition = true;
            }

            return CanStartCompetition;
        }

        public void TryPerformCompetition(BeepsController beepsController, MusicsController musicsController, int timeToDecrement)
        {
            if (!CanStartCompetition)
            {
                _programState = CompetitionProgramState.WaitToRunProgram;
                return;
            }

            switch (_programState)
            {
                case CompetitionProgramState.WaitToResumeProgram:
                    //_programState = _lastProgramState;
                    break;
                
                case CompetitionProgramState.WaitToRunProgram:
                    _nextTimeToChangeVolume = TimeToChangeVolume;
                    if (!ProgramIsRunning && StartWithBeeps)
                    {
                        _nextTimeToChangeVolume = 0;
                    }
                    ProgramIsRunning = true;
                    _programState = CompetitionProgramState.WaitToAdjustMusicVolumeToMin;
                    break;
                
                case CompetitionProgramState.WaitToAdjustMusicVolumeToMin:
                    // Setar música no mínimo.
                    musicsController.ChangeVolumeInSeconds(_nextTimeToChangeVolume, MusicsController.ChangeVolume.ToMin);
                    _programState = CompetitionProgramState.AdjustCurrentMusicVolumeToMin;
                    break;
                
                case CompetitionProgramState.AdjustCurrentMusicVolumeToMin:
                    if (musicsController.SecondsToChangeVolume == null)
                    {
                        _programState = CompetitionProgramState.WaitToPerformBeeps;
                    }
                    break;
                
                case CompetitionProgramState.WaitToPerformBeeps:
                    beepsController.CanPerformBeeps = true;
                    _nextTimeToChangeVolume = TimeToChangeVolume;
                    _programState = CompetitionProgramState.WaitToAdjustMusicVolumeToMax;
                    break;

                case CompetitionProgramState.WaitToAdjustMusicVolumeToMax:
                    if (!beepsController.CanPerformBeeps)
                    {
                        //test finish
                        musicsController.ChangeVolumeInSeconds(_nextTimeToChangeVolume, MusicsController.ChangeVolume.ToMax);
                        _programState = CompetitionProgramState.AdjustCurrentMusicVolumeToMax;
                    }
                    break;

                case CompetitionProgramState.AdjustCurrentMusicVolumeToMax:
                    // Setar música no máximo.
                    if (musicsController.SecondsToChangeVolume == null)
                    {
                        CanStartCompetition = false;
                        _programState = CompetitionProgramState.WaitToAdjustMusicVolumeToMin;
                    }
                    break;

                default:
                    _programState = CompetitionProgramState.WaitToRunProgram;
                    break;
            }
        }
    }
}
