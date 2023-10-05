using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace SpleenTween 
{
    public class Tween<T> : ITween
    {
        T from;
        T to;
        T val;

        float time;
        float duration;

        Action<T> update;
        Func<bool> stopIfTargetNull;

        public Tween(T from, T to, float duration, Action<T> update)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            this.update = update;
        }
        public Tween(T from, T to, float duration, Action<T> update, Func<bool> stopIfTargetNull)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            this.update = update;
            this.stopIfTargetNull = stopIfTargetNull;
        }

        bool ITween.Run()
        {
            if(stopIfTargetNull != null)
            {
                bool targetNull = stopIfTargetNull.Invoke();
                Debug.Log(targetNull);

                if (targetNull)
                {
                    return false;
                }
            }

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
    }
}

