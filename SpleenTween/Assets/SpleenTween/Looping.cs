using SpleenTween;
using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    static void RestartYoyoLoop<T>(ref T from, ref T to)
    {
        (from, to) = (to, from);
    }

    static void RestartIncrementLoop<T>(ref T from, ref T to)
    {
        T diff = SpleenExt.SubtractGeneric<T>(to, from);
        from = SpleenExt.AddGeneric<T>(from, diff);
        to = SpleenExt.AddGeneric<T>(to, diff);
    }
}
