using System;
using Unity.VisualScripting;
using UnityEngine;

namespace SpleenTween
{
    public class Spleen : MonoBehaviour
    {
        public static void Tween(GameObject targetObj, float from, float to, float duration, Action<float> onUpdate)
        {
            TweenInstance data = targetObj.AddComponent<TweenInstance>();
            data.InjectConstructor(from, to, duration, onUpdate, null);
        }
        public static void Tween(GameObject targetObj, float from, float to, float duration, Action<float> onUpdate, Action onComplete)
        {
            TweenInstance data = targetObj.AddComponent<TweenInstance>();
            data.InjectConstructor(from, to, duration, onUpdate, onComplete);
        }
    }

    public enum Ease
    {
        InSine
    }
}

