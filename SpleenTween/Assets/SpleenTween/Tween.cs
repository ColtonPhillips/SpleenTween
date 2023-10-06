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
        T from;
        T to;
        T val;

        float time;
        readonly float duration;
        float lerpValue;

        readonly Ease easeType = Ease.Linear;

        readonly Action<T> update;
        readonly Func<bool> nullCheck;

        Action onComplete;

        Loop loopType;
        int loopCycles = 0;

        bool forward = true;

        public Tween(T from, T to, float duration, Ease easeType, Action<T> update)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            this.easeType = easeType;
            this.update = update;
        }
        public Tween(T from, T to, float duration, Ease easeType, Action<T> update, Func<bool> nullCheck)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            this.update = update;
            this.easeType = easeType;
            this.nullCheck = nullCheck;
        }

        public Tween<T> OnComplete(Action onComplete)
        {
            this.onComplete += onComplete;
            return this;
        }

        public Tween<T> SetLoop(Loop loopType)
        {
            this.loopType = loopType;
            loopCycles = -1;
            return this;
        }

        bool ITween.Run()
        {
            if (NullTarget()) return false;

            time += Time.deltaTime;

            float lerp = time / duration;
            float inverseLerp = 1 - lerp;

            lerpValue = Easing.EaseVal(easeType, loopType == Loop.Rewind ? (forward ? lerp : inverseLerp) : lerp);

            bool running = time < duration;
            if (!running)
            {
                CompleteTween();

                if (loopType == Loop.Yoyo)
                {
                    Reverse();
                    running = true;
                }
                else if (loopType == Loop.Rewind)
                {
                    forward = !forward;
                    running = true;
                    time = 0;
                    lerpValue = forward ? 0 : 1;
                }
            }

            UpdateValue();
            return running;
        }

        private void UpdateValue()
        {
            if (typeof(T) == typeof(float))
            {
                float newVal = Mathf.LerpUnclamped((float)(object)from, (float)(object)to, lerpValue);
                val = (T)(object)newVal;
            }
            else if (typeof(T) == typeof(Vector3))
            {
                Vector3 newVal = Vector3.LerpUnclamped((Vector3)(object)from, (Vector3)(object)to, lerpValue);
                val = (T)(object)newVal;
            }
            else if (typeof(T) == typeof(Color))
            {
                Color newVal = Color.LerpUnclamped((Color)(object)from, (Color)(object)to, lerpValue);
                val = (T)(object)newVal;
            }

            update?.Invoke(val);
        }

        void CompleteTween()
        {
            lerpValue = 1;
            onComplete?.Invoke();
        }

        void Reverse()
        {
            (from, to) = (to, from);

            forward = !forward;

            time = 0;
            lerpValue = 0;
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

