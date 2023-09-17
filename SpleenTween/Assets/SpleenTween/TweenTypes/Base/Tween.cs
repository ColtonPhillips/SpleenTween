using System;
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
            _onComplete = onComplete;
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
            _onDelay = onDelay;
            _delay = delay;
            _shouldDelay = true;
            _currentTime -= delay;
            _triggeredDelay = true;
            return this;
        }

        public Tween Loop(Loop loopType)
        {
            _loopForever = true;
            _loopType = loopType;
            _loop = true;
            return this;
        }
        public Tween Loop(Loop loopType, int loopCount)
        {
            _loopType = loopType;
            _loopCount = loopCount;
            _loop = true;
            return this;
        }
        public Tween Loop(Loop loopType, int loopCount, Action onAllLoopsComplete)
        {
            _loopType = loopType;
            _loopCount = loopCount;
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
                _lerpValue = LoopTypes.LoopValue(_loopType, _lerpValue);

            _easeValue = Easing.EasingValue(_easing, _lerpValue);

            if (_currentTime >= _duration)
            {
                _triggeredDelay = false;
                if (!_loopForever && _loopCount <= 0)
                {
                    _loop = false;
                    _onComplete?.Invoke();
                    _onAllLoopsComplete?.Invoke();

                    _easeValue = _targetLerp;
                    UpdateValue();

                    return false;
                }

                _easeValue = _targetLerp;
                UpdateValue();
                _onComplete?.Invoke();

                if (!_loop)
                    return false;
                else
                {
                    Restart();

                    if(_loopType == SpleenTween.Loop.Reverse || _loopType == SpleenTween.Loop.Yoyo)
                    {
                        if (_targetLerp == 1)
                            _targetLerp = 0;
                        else if (_targetLerp == 0)
                            _targetLerp = 1;
                    }
                    
                    _loopCount--;
                    return true;
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
            if (_loopType == SpleenTween.Loop.Reverse || _loopType == SpleenTween.Loop.Yoyo)
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
            UpdateValue();
        }

        public void StopLoop()
        {
            _loop = false;
        }
    }
}

