namespace CompetitionsTimeControl.Controllers;

internal static class TimerController
{
    internal static bool PerformCountdown(ref int counterValue, ref int nextCounterValue, in int nextCounterRecharge,
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

    internal static int FromSecondsToMilliseconds(float seconds)
    {
        return (int)(seconds * 1000);
    }

    internal static float FromMillisecondsToSeconds(int milliseconds)
    {
        return milliseconds * 0.001f;
    }
}

