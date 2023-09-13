using System;
using UnityEngine;

namespace SpleenTween
{
    public class Spleen : MonoBehaviour
    {
        public static TweenInstance Float(GameObject targetObj, float from, float to, float duration, Action<float> onUpdate, Ease easing)
        {
            TweenInstance tween = targetObj.AddComponent<TweenInstance>();
            tween.InjectFloat(from, to, duration, onUpdate, easing);
            return tween;
        }

        public static TweenInstance Position(GameObject targetObj, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            TweenInstance tween = targetObj.AddComponent<TweenInstance>();
            tween.InjectPosition(from, to, duration, easing);
            return tween;
        }
    }
}

