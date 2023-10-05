using System;
using System.Collections;
using System.Collections.Generic;
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

        public Tween(T from, T to, float duration, Action<T> update)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            this.update = update;
        }

        public void Run(out bool done)
        {
            time += Time.deltaTime;
            float lerp = time / duration;

            Update(lerp);

            done = time >= duration;
            if (!done) return;
        }

        private void Update(float lerp)
        {
            if (typeof(T) == typeof(float))
            {
                float floatVal = Mathf.Lerp((float)(object)from, (float)(object)to, lerp);
                val = (T)(object)floatVal;
            }
            else if (typeof(T) == typeof(Vector3))
            {
                Vector3 floatVal = Vector3.Lerp((Vector3)(object)from, (Vector3)(object)to, lerp);
                val = (T)(object)floatVal;
            }

            update?.Invoke(val);
        }
    }
}

