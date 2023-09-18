using System;

namespace SpleenTween
{
    public enum Loop
    {
        Restart,
        Reverse,
        Yoyo,
        Relative
    }

    public class Loops
    {
        public static float LoopValue(Loop loopType, float lerpValue)
        {
            return loopType switch
            {
                Loop.Restart => lerpValue,
                Loop.Reverse => 1 - lerpValue,
                Loop.Yoyo => 1 - lerpValue,

                _ => throw new NotImplementedException(),
            };
        }
    }
}

