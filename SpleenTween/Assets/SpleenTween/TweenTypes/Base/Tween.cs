using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace SpleenTween
{
    public class Tween
    {
        protected float _duration;
        protected Ease _easing;
        protected Action _onComplete;
        protected Action _onDelay;

        public Action _nullCheck;
        public bool _targetIsNull;

        protected float _currentTime;

        protected float _delay;
        protected bool _shouldDelay;
        protected bool _triggeredDelay;

        protected Loop _loopType;
        protected float _loopDelay;

        protected float _easeValue;
        protected float _lerpValue;

        protected bool _loop;
        protected int _loopCount;
        protected Action _onAllLoopsComplete;
        bool _loopForever = false;

        int _targetLerp = 1;

        public Tween(float duration, Ease easing)
        {
            _duration = duration;
            _easing = easing;
        }
        public Tween OnComplete(Action onComplete)
        {
            _onComplete += onComplete;
            return this;
        }
        public Tween Delay(float delay)
        {
            _delay = delay;
            _shouldDelay = true;
            _currentTime -= delay;
            _triggeredDelay = true;
            return this;
        }
        public Tween Delay(float delay, Action onDelay)
        {
            _onDelay += onDelay;
            _delay = delay;
            _shouldDelay = true;
            _currentTime -= delay;
            _triggeredDelay = true;
            return this;
        }

        public virtual Tween Loop(Loop loopType)
        {
            _loopForever = true;
            _loopType = loopType;
            _loop = true;
            return this;
        }

        public virtual Tween Loop(Loop loopType, int loopCount)
        {
            _loopType = loopType;
            _loopCount = loopCount - 1;
            _loop = true;
            return this;
        }
        public virtual Tween Loop(Loop loopType, int loopCount, Action onAllLoopsComplete)
        {
            _loopType = loopType;
            _loopCount = loopCount - 1;
            _onAllLoopsComplete += onAllLoopsComplete;
            _loop = true;
            return this;
        }

        public bool Tweening()
        {
            _nullCheck?.Invoke();
            if (_targetIsNull)
            {
                return false;
            }

            if(_shouldDelay && !_triggeredDelay)
                TriggerDelay();

            _currentTime += Time.deltaTime;

            if (_currentTime <= 0)
                return true;
            else if(_shouldDelay && _currentTime > 0)
            {
                _onDelay?.Invoke();
            }


            _lerpValue = GetLerpValue(_currentTime, _duration);

            if (_loop && _targetLerp == 0)
                _lerpValue = Loops.LoopValue(_loopType, _lerpValue);

            _easeValue = Easing.EasingValue(_easing, _lerpValue);

            if (_currentTime >= _duration)
            {
                _triggeredDelay = false;
   
                _easeValue = _targetLerp;
                UpdateValue();
                _onComplete?.Invoke();
                
                if(_loopCount > 0 || _loopForever)
                {
                    if (_loopType == SpleenTween.Loop.Reverse)
                    {
                        if (_targetLerp == 1)
                            _targetLerp = 0;
                        else if (_targetLerp == 0)
                            _targetLerp = 1;
                    }
                    Restart();
                    _loopCount--;
                    return true;
                }
                else if (_loopCount <= 0 && !_loopForever)
                {
                    _loop = false;
                    _onAllLoopsComplete?.Invoke();

                    Restart();

                    if (_loopType == SpleenTween.Loop.Yoyo)
                        _easeValue = 0;
                    UpdateValue();

                    return false;
                }
            }
            else
            {
                UpdateValue();
            }
            return true;
        }
        public virtual void UpdateValue() { }

        float GetLerpValue(float time, float duration)
        {
            float lerpValue = time / duration;
            return lerpValue;
        }

        void TriggerDelay()
        {
            _lerpValue = 0;
            if (_loopType == SpleenTween.Loop.Reverse)
            {
                if (_targetLerp == 1)
                    _lerpValue = 0;
                else if (_targetLerp == 0)
                    _lerpValue = 1;
            }
            _easeValue = Easing.EasingValue(_easing, _lerpValue);
            _triggeredDelay = true;
            UpdateValue();
            _currentTime -= _delay;
        }

        public void Restart()
        {
            _lerpValue = _targetLerp;
            _currentTime = 0;
            _easeValue = Easing.EasingValue(_easing, _lerpValue);

            if (_loopType == SpleenTween.Loop.Yoyo && (_loopCount > 0 || _loopForever))
                _easeValue = 0;
            else if(_loopType == SpleenTween.Loop.Yoyo && _loopForever)
                _easeValue = 1;

            if(_loopType != SpleenTween.Loop.Reverse)
                UpdateValue();
        }

        public void StopLoop()
        {
            _loop = false;
        }
    }
}

