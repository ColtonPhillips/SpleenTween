using System;

namespace SpleenTween
{
    public enum LoopType
    {
        Restart,
        Rewind,
        Yoyo,
        Incremental
    }

    public class Looping
    {
        public static float LoopValue(LoopType loopType, float lerpValue)
        {
            return loopType switch
            {
                LoopType.Restart => lerpValue,
                LoopType.Rewind => 1 - lerpValue,
                LoopType.Yoyo => 1 - lerpValue,
                LoopType.Incremental => lerpValue,

                _ => throw new NotImplementedException(),
            };
        }
    }
}

