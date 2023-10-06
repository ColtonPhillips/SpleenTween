using System.Collections;
using System.Collections.Generic;
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
    public static float RewindValue(float lerp, bool forward) => !forward ? 1 - lerp : lerp;
}
