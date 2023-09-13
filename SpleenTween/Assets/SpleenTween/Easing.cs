using UnityEngine;

namespace SpleenTween
{
    public enum Ease
    {
        Linear,

        InSine, OutSine, InOutSine,
        InQuad, OutQuad, InOutQuad,
        InCubic, OutCubic, InOutCubic,
        InQuart, OutQuart, InOutQuart,
        InQuint, OutQuint, InOutQuint,
        InExpo, OutExpo, InOutExpo,
        InCirc, OutCirc, InOutCirc,
        InBack, OutBack, InOutBack,
        InElastic, OutElastic, InOutElastic,
        InBounce, OutBounce, InOutBounce
    }

    public class Easing
    {
        public static float EasingValue(Ease easing, float lerpValue)
        {
            return easing switch
            {
                Ease.Linear => lerpValue,
                Ease.InSine => 1 - Mathf.Cos(lerpValue * Mathf.PI / 2),
                Ease.OutSine => Mathf.Sin(lerpValue * Mathf.PI / 2),
                Ease.InOutSine => -(Mathf.Cos(Mathf.PI * lerpValue) - 1) / 2,
            };
        }
    }
}

