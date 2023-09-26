using System;
using System.Collections.Generic;
using UnityEngine;

namespace SpleenTween
{
    public class Tween
    {
        protected float duration;
        protected Ease easing;
        public Action onComplete;
        public Action onStart;
        Action onUpdateGeneral;

        public GameObject target;
        public Action nullCheck;
        public bool targetIsNull;

        float currentTime;

        float delayDuration;
        bool delayEnabled;
        bool startTriggered;

        public LoopType loopType;
        float loopDelay;

        protected float easeValue;
        float lerpValue;

        bool loop;
        int loopCount;
        Action onAllLoopsComplete;
        bool loopForever = false;

        public int targetLerp = 1;

        List<Tween> chainedTweens = new();

        public Tween(float duration, Ease easing)
        {
            this.duration = duration;
            this.easing = easing;
        }
        public Tween OnUpdate(Action onUpdate)
        {
            onUpdateGeneral += onUpdate;
            return this;
        }
        public Tween OnComplete(Action onComplete)
        {
            this.onComplete += onComplete;
            return this;
        }
        public Tween OnStart(Action onStart)
        {
            this.onStart += onStart;
            return this;
        }

        public Tween Delay(float delayDuration)
        {
            this.delayDuration = delayDuration;
            delayEnabled = true;
            currentTime -= delayDuration;
            return this;
        }

        public Tween Chain(Tween tween)
        {
            Spleen.StopTween(tween);
            chainedTweens.Add(tween);
            float waitTime = this.duration + this.delayDuration;

            int index = chainedTweens.Count - 1;

            Tween prevTween = null;
            if (index <= 0)
                prevTween = this;
            else
                prevTween = chainedTweens[index - 1];

            prevTween.OnComplete(() => Spleen.AddTween(tween));
            return this;
        }

        public virtual Tween Loop(LoopType loopType)
        {
            loopForever = true;
            this.loopType = loopType;
            loop = true;
            return this;
        }
        public virtual Tween Loop(LoopType loopType, float startDelay)
        {
            currentTime -= startDelay;
            loopForever = true;
            this.loopType = loopType;
            loop = true;
            return this;
        }
        public virtual Tween Loop(LoopType loopType, int loopCount)
        {
            this.loopType = loopType;
            this.loopCount = loopCount - 1;
            loop = true;
            return this;
        }
        public virtual Tween Loop(LoopType loopType, int loopCount, float startDelay)
        {
            currentTime -= startDelay;
            this.loopType = loopType;
            this.loopCount = loopCount - 1;
            loop = true;
            return this;
        }
        public virtual Tween Loop(LoopType loopType, int loopCount, Action onAllLoopsComplete)
        {
            this.loopType = loopType;
            this.loopCount = loopCount - 1;
            this.onAllLoopsComplete += onAllLoopsComplete;
            loop = true;
            return this;
        }
        public virtual Tween Loop(LoopType loopType, int loopCount, float startDelay, Action onAllLoopsComplete)
        {
            currentTime -= startDelay;
            this.loopType = loopType;
            this.loopCount = loopCount - 1;
            this.onAllLoopsComplete += onAllLoopsComplete;
            loop = true;
            return this;
        }

        public Tween StopIfNull(GameObject target)
        {
            nullCheck += () =>
            {
                if (target == null)
                    targetIsNull = true;
            };
            this.target = target;
            return this;
        }

        public bool Tweening()
        {
            nullCheck?.Invoke();
            if (targetIsNull)
                return false;

            currentTime += Time.deltaTime;

            if (currentTime < 0)
                return true;
            
            if (currentTime >= 0 && !startTriggered)
            {
                OnTweenStart();
            }

            lerpValue = GetLerpValue(currentTime, duration);
            if (loop && targetLerp == 0)
                lerpValue = Looping.LoopValue(loopType, lerpValue);
            easeValue = Easing.EasingValue(easing, lerpValue);

            if(currentTime < duration)
            {
                UpdateValue();
                onUpdateGeneral?.Invoke();
                return true;
            }

            easeValue = targetLerp;

            if (loop)
            {
                onComplete?.Invoke();

                if (loopCount > 0 || loopForever)
                {
                    if (loopType == LoopType.Rewind)
                    {
                        if (targetLerp == 1)
                            targetLerp = 0;
                        else if (targetLerp == 0)
                            targetLerp = 1;
                    }

                    Restart();
                    loopCount--;

                    onUpdateGeneral?.Invoke();
                    return true;
                }

                if (loopCount <= 0 && !loopForever)
                {
                    loop = false;
                    onAllLoopsComplete?.Invoke();

                    Restart();

                    if (loopType == LoopType.Yoyo)
                        easeValue = 0;

                    UpdateValue();

                    onUpdateGeneral?.Invoke();
                    return false;
                }
            }
            
            UpdateValue();
            onUpdateGeneral?.Invoke();
            onComplete?.Invoke();
            return false;
        }

        public virtual void UpdateValue() { }

        float GetLerpValue(float time, float duration)
        {
            float lerpValue = time / duration;
            return lerpValue;
        }

        public void Restart()
        {
            startTriggered = false;

            lerpValue = targetLerp;
            currentTime = 0;
            easeValue = Easing.EasingValue(easing, lerpValue);

            if (loopType == LoopType.Yoyo && (loopCount > 0 || loopForever))
                easeValue = 0;
            else if (loopType == LoopType.Yoyo && loopForever)
                easeValue = 1;

            if(loopType == LoopType.Incremental)
                easeValue = 0;

            if (loopType != LoopType.Rewind)
                UpdateValue();

            if (delayEnabled)
                currentTime -= delayDuration;
        }

        public void StopLoop()
        {
            loop = false;
        }

        void OnTweenStart()
        {
            onStart?.Invoke();
            startTriggered = true;
        }
    }
}

