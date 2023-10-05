using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
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
        float easeVal;
        
        readonly Action<T> update;
        readonly Func<bool> nullCheck;

        Action onComplete;

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
        public Tween<T> OnComplete(Action onComplete)
        {
            this.onComplete += onComplete;
            return this;
        }

        bool ITween.Run()
        {
            if (NullTarget()) return false;

            time += Time.deltaTime;
            float lerp = time / duration;
            easeVal = lerp;

            bool completed = time >= duration;
            if(completed)
            {
                CompleteTween();
            }

            UpdateValue();
            return !completed;
        }

        private void UpdateValue()
        {
            if (typeof(T) == typeof(float))
            {
                float newVal = Mathf.LerpUnclamped((float)(object)from, (float)(object)to, easeVal);
                val = (T)(object)newVal;
            }
            else if (typeof(T) == typeof(Vector3))
            {
                Vector3 newVal = Vector3.LerpUnclamped((Vector3)(object)from, (Vector3)(object)to, easeVal);
                val = (T)(object)newVal;
            }
            else if (typeof(T) == typeof(Color))
            {
                Color newVal = Color.LerpUnclamped((Color)(object)from, (Color)(object)to, easeVal);
                val = (T)(object)newVal;
            }

            update?.Invoke(val);
        }

        void CompleteTween()
        {
            easeVal = 1;
            onComplete?.Invoke();
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

