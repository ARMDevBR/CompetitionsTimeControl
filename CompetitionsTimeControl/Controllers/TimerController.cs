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

    internal static int FromSecondsToMiliseconds(int seconds)
    {
        return seconds * 1000;
    }
}

