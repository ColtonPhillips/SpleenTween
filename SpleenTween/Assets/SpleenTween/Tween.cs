using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Video;

namespace SpleenTween 
{
    public class Tween<T> : ITween
    {
        T from;
        T to;
        T val;

        float time;
        readonly float duration;
        float delay;

        float LerpProgress { get => Mathf.Clamp01(time / duration); }
        float EaseProgress
        {
            get
            {
                float backwardsLerp = 1 - LerpProgress;
                float lerpBasedOnDirection = loopType == Loop.Rewind ? (Direction == 0 ? backwardsLerp : LerpProgress) : LerpProgress;
                float easeVal = Easing.EaseVal(easeType, lerpBasedOnDirection);
                return easeVal;
            }
            set { }
        }

        bool Active { get => time < duration; }

        readonly Ease easeType = Ease.Linear;

        readonly Action<T> update;
        readonly Func<bool> nullCheck;

        Action onComplete;

        Loop loopType;

        int _cycles = -1;
        int Cycles
        {
            get
            {
                int clampedCycles = Mathf.Clamp(_cycles - loopCounter, -1, int.MaxValue);
                return clampedCycles;
            }
            set 
            {
                Mathf.Clamp(value, -1, int.MaxValue);
            }
        }
        int loopCounter; 

        int Direction { get => (loopCounter % 2) == 0 ? 1 : 0; }

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

        public Tween<T> SetLoop(Loop loopType, int cycles)
        {
            this.loopType = loopType;
            _cycles = cycles;
            return this;
        }

        public Tween<T> SetDelay(float delay, bool startDelay)
        {
            this.delay = delay;
            if(startDelay) DelayCycle(delay);
            return this;
        }

        bool ITween.Run()
        {
            if (Cycles == 0) return false;

            if (NullTarget()) return false;

            time += Time.deltaTime;
            if (time < 0) return true;

            if (!Active)
            {
                CompleteTween();
                RestartLoop();
                EaseProgress = Direction;
            }

            UpdateValue();
            return Active;
        }

        private void UpdateValue()
        {
            if (typeof(T) == typeof(float))
            {
                float newVal = Mathf.LerpUnclamped((float)(object)from, (float)(object)to, EaseProgress);
                val = (T)(object)newVal;
            }
            else if (typeof(T) == typeof(Vector3))
            {
                Vector3 newVal = Vector3.LerpUnclamped((Vector3)(object)from, (Vector3)(object)to, EaseProgress);
                val = (T)(object)newVal;
            }
            else if (typeof(T) == typeof(Color))
            {
                Color newVal = Color.LerpUnclamped((Color)(object)from, (Color)(object)to, EaseProgress);
                val = (T)(object)newVal;
            }
            update?.Invoke(val);
        }

        void CompleteTween()
        {
            onComplete?.Invoke();
        } 

        void RestartLoop()
        {
            if (Cycles == -1 || Cycles > 0)
            {
                Looping.RestartLoopTypes(loopType, ref from, ref to);
                time = 0;
                DelayCycle(delay);
                loopCounter++;
            }
        }

        void DelayCycle(float delay)
        {
            time -= delay;
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

