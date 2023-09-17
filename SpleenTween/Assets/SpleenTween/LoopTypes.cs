using System;

namespace SpleenTween
{
    public enum Loop
    {
        Restart,
        Reverse,
        Yoyo,
        Incremental
    }

    public class LoopTypes
    {
        public static float LoopValue(Loop loopType, float lerpValue)
        {
            return loopType switch
            {
                Loop.Restart => lerpValue,
                Loop.Yoyo => 1 - lerpValue,

                _ => throw new NotImplementedException(),
            };
        }
    }
}

