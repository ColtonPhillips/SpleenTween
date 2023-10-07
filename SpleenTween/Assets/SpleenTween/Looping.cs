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
        switch(loopType)
        {
            case Loop.Yoyo:
                RestartYoyoLoop(ref from, ref to);
                break;

            case Loop.Increment:
                RestartIncrementLoop(ref from, ref to);
                break;

            default:
                break;
        }
    }

    static void RestartYoyoLoop<T>(ref T from, ref T to)
    {
        (from, to) = (to, from);
    }

    static void RestartIncrementLoop<T>(ref T from, ref T to)
    {
        if (typeof(T) == typeof(float))
        {
            float diff = (float)(object)to - (float)(object)from;
            float newFrom = (float)(object)from + diff;
            float newTo = (float)(object)to + diff;

            from = (T)(object)newFrom;
            to = (T)(object)newTo;
        }
        else if (typeof(T) == typeof(Vector3))
        {
            Vector3 diff = (Vector3)(object)to - (Vector3)(object)from;
            Vector3 newFrom = (Vector3)(object)from + diff;
            Vector3 newTo = (Vector3)(object)to + diff;

            from = (T)(object)newFrom;
            to = (T)(object)newTo;
        }
        else if (typeof(T) == typeof(Color))
        {
            Color diff = (Color)(object)to - (Color)(object)from;
            Color newFrom = (Color)(object)from + diff;
            Color newTo = (Color)(object)to + diff;

            from = (T)(object)newFrom;
            to = (T)(object)newTo;
        }
    }
}
