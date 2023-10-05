using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace SpleenTween 
{
    public class Tween<T> : ITween
    {
        readonly T from;
        readonly T to;
        T val;

        float time;
        readonly float duration;

        readonly Action<T> update;
        readonly Func<bool> nullCheck;

        public Tween(T from, T to, float duration, Action<T> update)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            this.update = update;
        }
        public Tween(T from, T to, float duration, Action<T> update, Func<bool> nullCheck)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            this.update = update;
            this.nullCheck = nullCheck;
        }

        bool ITween.Run()
        {
            if (NullTarget()) return false;

            time += Time.deltaTime;
            float lerp = time / duration;

            UpdateValue(lerp);

            bool running = time < duration;
            return running;
        }

        private void UpdateValue(float lerp)
        {
            if (typeof(T) == typeof(float))
            {
                float floatVal = Mathf.LerpUnclamped((float)(object)from, (float)(object)to, lerp);
                val = (T)(object)floatVal;
            }
            else if (typeof(T) == typeof(Vector3))
            {
                Vector3 floatVal = Vector3.LerpUnclamped((Vector3)(object)from, (Vector3)(object)to, lerp);
                val = (T)(object)floatVal;
            }

            update?.Invoke(val);
        }

        bool NullTarget()
        {
            if (nullCheck != null)
            {
                if (nullCheck.Invoke() == true)
                {
                    return true;
                }
            }
            return false;
        }
    }
}

