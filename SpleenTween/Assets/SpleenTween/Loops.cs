using System;

namespace SpleenTween
{
    public enum Loops
    {
        Restart,
        Rewind,
        Yoyo,
        Incremental
    }

    public class Looping
    {
        public static float LoopValue(Loops loopType, float lerpValue)
        {
            return loopType switch
            {
                Loops.Restart => lerpValue,
                Loops.Rewind => 1 - lerpValue,
                Loops.Yoyo => 1 - lerpValue,
                Loops.Incremental => lerpValue,

                _ => throw new NotImplementedException(),
            };
        }
    }
}

