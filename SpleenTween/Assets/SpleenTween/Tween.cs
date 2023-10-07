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
                float easeVal = Easing.EaseVal(easeType, LerpProgress);

                if(loopType == Loop.Rewind)
                {
                    float backwardsLerp = 1 - LerpProgress;
                    float lerpBasedOnDirection = Direction == 0 ? backwardsLerp : LerpProgress;
                    return Easing.EaseVal(easeType, lerpBasedOnDirection); 
                }
                else if(loopType == Loop.Yoyo)  // magic yoyo code instead of swapping from and to
                {
                    float backwardsEase = 1 - easeVal;
                    return Direction == 0 ? backwardsEase : easeVal;
                }

                return easeVal;
            }
            set { }
        }

        bool Active { get => time < duration; }

        readonly Ease easeType;
        Loop loopType;

        Action<T> onUpdate;
        readonly Func<bool> nullCheck;

        Action onComplete;
        Action onStart;
        bool started;

        int cycles = -1;
        int Cycles
        {
            get => Mathf.Clamp(cycles - loopCounter, -1, int.MaxValue);
            set => Mathf.Clamp(value, -1, int.MaxValue);
        }

        int loopCounter;

        int Direction { get => (loopType != Loop.Rewind && loopType != Loop.Yoyo) ? 1 : ((loopCounter % 2) == 0 ? 1 : 0); } // default to forward if not rewind. otherwise, check direction based on loop count


        public Tween(T from, T to, float duration, Ease easeType, Action<T> onUpdate)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            this.easeType = easeType;
            this.onUpdate = onUpdate;
        }
        public Tween(T from, T to, float duration, Ease easeType, Action<T> onUpdate, Func<bool> nullCheck)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            this.onUpdate = onUpdate;
            this.easeType = easeType;
            this.nullCheck = nullCheck;
        }

        bool ITween.Run()
        {
            if (Cycles == 0) return false; // hack: because if Cycles is 0, it does 1 cycle

            if (NullTarget()) return false;

            time += Time.deltaTime;
            if (time < 0) return true; // wait for delay
            if (time >= 0 && !started) OnStartTween(); 

            if (!Active)
            {
                OnCompleteTween();
                RestartLoop();
                EaseProgress = Direction; // set final value for precision
                Debug.Log(Direction);
            }

            UpdateValue();
            return Active;
        }

        public Tween<T> OnComplete(Action onComplete)
        {
            this.onComplete += onComplete;
            return this;
        }
        public Tween<T> OnStart(Action onStart)
        {
            this.onStart += onStart;
            return this;
        }
        public Tween<T> OnUpdate(Action<T> onUpdate)
        {
            this.onUpdate += onUpdate;
            return this;
        }

        public Tween<T> SetLoop(Loop loopType, int cycles)
        {
            this.loopType = loopType;
            this.cycles = cycles;
            return this;
        }

        public Tween<T> SetDelay(float delay, bool startDelay)
        {
            this.delay = delay;
            if(startDelay) DelayCycle(delay);
            return this;
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
            onUpdate?.Invoke(val);
        }

        void OnCompleteTween()
        {
            started = false;
            onComplete?.Invoke();
        } 
        void OnStartTween()
        {
            started = true;
            onStart?.Invoke();
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

