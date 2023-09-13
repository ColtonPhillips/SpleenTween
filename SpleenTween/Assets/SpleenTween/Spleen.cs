using SpleenTween;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpleenTween
{
    public class Spleen : MonoBehaviour
    {
        public List<TweenFloat> activeTweensFloat = new();
        public static Spleen Tween;
        private void Awake()
        {
            if (Tween == null)
                Tween = this;
            else
                Destroy(gameObject);
        }

        private void Update()
        {
            for (int i = activeTweensFloat.Count - 1; i >= 0; i--)
            {
                if (!activeTweensFloat[i].Tweening())
                {
                    activeTweensFloat.RemoveAt(i);
                }
            }
        }

        public Tween Float(float from, float to, float duration, Ease easing, Action<float>onUpdate)
        {
            TweenFloat tween = new TweenFloat(from, to, duration, easing, onUpdate);
            activeTweensFloat.Add(tween);
            return tween;
        }
    }
}

