using System;
using UnityEngine;

namespace SpleenTween 
{
    public class Tween<T> : ITween
    {
        public T from;
        public T to;
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

                if(LoopType == Loop.Rewind)
                {
                    float backwardsLerp = 1 - LerpProgress;
                    float lerpBasedOnDirection = Direction == 0 ? backwardsLerp : LerpProgress;
                    return Easing.EaseVal(easeType, lerpBasedOnDirection); 
                }
                else if(LoopType == Loop.Yoyo)  // magic yoyo code instead of swapping from and to
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
        public Loop LoopType { get; private set; }

        Action<T> onUpdate;
        readonly Func<bool> nullCheck;

        Action onComplete;
        Action onStart;
        bool started;

        int cycles = 0;
        int Cycles
        {
            get => Mathf.Clamp(cycles - loopCounter, -1, int.MaxValue);
            set => Mathf.Clamp(value, -1, int.MaxValue);
        }

        int loopCounter;

        int Direction { get => (!Looping.IsLoopWeird(LoopType)) ? 1 : ((loopCounter % 2) == 0 ? 1 : 0); } // default to forward if not rewind. otherwise, check direction based on loop count

        public bool Paused { get; private set; }

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
            if (Paused) return true;

            if (NullTarget()) return false;

            time += Time.deltaTime;
            if (time < 0) return true; // wait for delay
            if (time >= 0 && !started) OnStartTween(); 

            if (!Active)
            {
                OnCompleteTween();
                RestartLoop();
                EaseProgress = Direction; // set final value for precision
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
            if (cycles == 0)
                SpleenTweenManager.StopTween(this);

            this.LoopType = loopType;
            this.cycles = cycles - 1;

            return this;
        }

        public Tween<T> SetDelay(float delay, bool startDelay)
        {
            this.delay = delay;
            if(startDelay) DelayCycle(delay);
            return this;
        }

        public Tween<T> Pause()
        {
            Paused = true;
            return this;
        }
        public Tween<T> Play()
        {
            Paused = false;
            return this;
        }


        private void UpdateValue()
        {
            val = SpleenExt.LerpUnclampedGeneric(from, to, EaseProgress);
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
                Looping.RestartLoopTypes(LoopType, ref from, ref to);
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

