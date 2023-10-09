using SpleenTween;

public enum Loop
{
    Restart,
    Rewind,
    Yoyo,
    Increment
}

public class Looping
{
    public static void RestartLoopTypes<T>(Loop loopType, ref T from, ref T to)
    {
        switch (loopType)
        {
            case Loop.Increment: RestartIncrementLoop(ref from, ref to); break;
            default: break;
        }
    }

    public static bool IsLoopWeird(Loop loopType)
    {
        return loopType == Loop.Yoyo || loopType == Loop.Rewind;
    }

    static void RestartIncrementLoop<T>(ref T from, ref T to)
    {
        T diff = SpleenExt.SubtractGeneric(to, from);
        from = SpleenExt.AddGeneric(from, diff);
        to = SpleenExt.AddGeneric(to, diff);
    }
}
