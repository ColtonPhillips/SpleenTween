using System;
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
        public static float EasingValue(Ease easing, float x)
        {
            return easing switch
            {
                Ease.Linear => x,

                Ease.InSine => 1 - Mathf.Cos(x * Mathf.PI / 2),
                Ease.OutSine => Mathf.Sin(x * Mathf.PI / 2),
                Ease.InOutSine => -(Mathf.Cos(Mathf.PI * x) - 1) / 2,

                Ease.InQuad => x * x,
                Ease.OutQuad => 1 - (1 - x) * (1 - x),
                Ease.InOutQuad => x < 0.5 ? 2 * x * x : 1 - Mathf.Pow(-2 * x + 2, 2) / 2,

                Ease.InCubic => x * x * x,
                Ease.OutCubic => 1 - Mathf.Pow(1 - x, 3),
                Ease.InOutCubic => x < 0.5 ? 4 * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 3) / 2,

                Ease.InQuart => x * x * x * x,
                Ease.OutQuart => 1 - Mathf.Pow(1 - x, 4),
                Ease.InOutQuart => x < 0.5 ? 8 * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 4) / 2,

                Ease.InQuint => x * x * x * x * x,
                Ease.OutQuint => 1 - Mathf.Pow(1 - x, 5),
                Ease.InOutQuint => x < 0.5 ? 16 * x * x * x * x * x : 1 - Mathf.Pow(-2 * x + 2, 5) / 2,

                Ease.InExpo => x == 0 ? 0 : Mathf.Pow(2, 10 * x - 10),
                Ease.OutExpo => x == 1 ? 1 : 1 - Mathf.Pow(2, -10 * x),
                Ease.InOutExpo => x == 0 ? 0 : x == 1 ? 1 : x < 0.5 ? Mathf.Pow(2, 20 * x - 10) / 2 : (2 - Mathf.Pow(2, -20 * x + 10)) / 2,

                Ease.InCirc => 1 - Mathf.Sqrt(1 - Mathf.Pow(x, 2)),
                Ease.OutCirc => Mathf.Sqrt(1 - Mathf.Pow(x - 1, 2)),
                Ease.InOutCirc => x < 0.5 ? (1 - Mathf.Sqrt(1 - Mathf.Pow(2 * x, 2))) / 2 : (Mathf.Sqrt(1 - Mathf.Pow(-2 * x + 2, 2)) + 1) / 2,

                Ease.InBack => (1.70158f + 1) * x * x * x - 1.70158f * x * x,
                Ease.OutBack => 1 + (1.70158f + 1) * Mathf.Pow(x - 1, 3) + 1.70158f * Mathf.Pow(x - 1, 2),
                Ease.InOutBack => x < 0.5 ? (Mathf.Pow(2 * x, 2) * (((1.70158f * 1.525f) + 1) * 2 * x - (1.70158f * 1.525f))) / 2 : (Mathf.Pow(2 * x - 2, 2) * (((1.70158f * 1.525f) + 1) * (x * 2 - 2) + (1.70158f * 1.525f)) + 2) / 2,

                Ease.InElastic => x == 0 ? 0 : x == 1 ? 1 : -Mathf.Pow(2, 10 * x - 10) * Mathf.Sin((x * 10 - 10.75f) * ((2 * Mathf.PI) / 3)),
                Ease.OutElastic => x == 0 ? 0 : x == 1 ? 1 : Mathf.Pow(2, -10 * x) * Mathf.Sin((x * 10 - 0.75f) * ((2 * Mathf.PI) / 3)) + 1,
                Ease.InOutElastic => x == 0 ? 0 : x == 1 ? 1 : x < 0.5 ? -(Mathf.Pow(2, 20 * x - 10) * Mathf.Sin((20 * x - 11.125f) * ((2 * Mathf.PI) / 4.5f))) / 2 : (Mathf.Pow(2, -20 * x + 10) * Mathf.Sin((20 * x - 11.125f) * (2 * Mathf.PI) / 4.5f)) / 2 + 1,

                Ease.InBounce => InBounce(x),
                Ease.OutBounce => OutBounce(x),
                Ease.InOutBounce => InOutBounce(x),

                _ => throw new NotImplementedException(),
            };
        }

        static float InBounce(float x)
        {
            return 1 - OutBounce(1 - x);
        }
        static float OutBounce(float x)
        {
            float n1 = 7.5625f;
            float d1 = 2.75f;

            if (x < 1f / d1)
            {
                return n1 * x * x;
            }
            else if (x < 2f / d1)
            {
                return n1 * (x -= 1.5f / d1) * x + 0.75f;
            }
            else if (x < 2.5 / d1)
            {
                return n1 * (x -= 2.25f / d1) * x + 0.9375f;
            }
            else
            {
                return n1 * (x -= 2.625f / d1) * x + 0.984375f;
            }
        }
        static float InOutBounce(float x)
        {
            return x < 0.5f ? (1 - OutBounce(1f - 2f * x)) / 2f : (1f + OutBounce(2f * x - 1f)) / 2f;
        }
    }
}

