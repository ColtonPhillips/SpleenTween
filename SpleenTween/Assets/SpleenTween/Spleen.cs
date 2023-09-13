using System;
using UnityEngine;

namespace SpleenTween
{
    public class Spleen : MonoBehaviour
    {
        public static TweenInstance Tween(GameObject targetObj, float from, float to, float duration, Action<float> onUpdate)
        {
            TweenInstance tween = targetObj.AddComponent<TweenInstance>();
            tween.InjectConstructor(from, to, duration, onUpdate);
            return tween;
        }
    }

    public enum Ease
    {
        InSine
    }
}

