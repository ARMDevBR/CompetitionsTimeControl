namespace CompetitionsTimeControl.Controllers;

internal static class TimerController
{
    private static System.Threading.Timer? ThreadTimer;
    public static bool ThreadTimerIsRunning { get ; private set; }

    internal static void CreateThreadTimer(TimerCallback threadTimerTick)
    {
        ThreadTimerIsRunning = false;
        ThreadTimer = new System.Threading.Timer(threadTimerTick, null, Timeout.Infinite, 10);
    }

    internal static void StartThreadTimer()
    {
        ChangeThreadTimer(0, 10);
        ThreadTimerIsRunning = true;
    }
    internal static void StopThreadTimer()
    {
        ThreadTimerIsRunning = false;
        ChangeThreadTimer(Timeout.Infinite, Timeout.Infinite);
    }

    private static void ChangeThreadTimer(int dueTime, int period)
    {
        ThreadTimer?.Change(dueTime, period);
    }

    internal static void DisposeThreadTimer()
    {
        ThreadTimer?.Dispose();
    }

    internal static bool PerformCountdown(ref int counterValue, ref int nextCounterValue, in int nextCounterRecharge,
        int timeToDecrement, bool nextRechargeIscurrCounter, Action? extraFunctionCallback = null)
    {
        bool ret = false;

        counterValue -= timeToDecrement;

        if (counterValue <= 0)
        {
            if (nextRechargeIscurrCounter)
                nextCounterValue += nextCounterRecharge;
            else
                nextCounterValue = nextCounterRecharge;
            
            ret = true;
        }
        extraFunctionCallback?.Invoke();

        return ret;
    }

    internal static int FromSecondsToMilliseconds(float seconds)
    {
        return (int)(seconds * 1000);
    }

    internal static float FromMillisecondsToSeconds(int milliseconds)
    {
        return milliseconds * 0.001f;
    }
}

