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
        protected float _currentDelay;

        protected Loop _loopType;
        protected float _loopDelay;

        protected float _easeValue;
        protected float _lerpValue;

        protected bool _loop;

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
            _currentDelay = delay;
            return this;
        }
        public Tween Delay(float delay, Action onDelay)
        {
            _onDelay = onDelay;
            _delay = delay;
            _currentDelay = delay;
            return this;
        }
        public Tween Loop(Loop loopType)
        {
            _loopType = loopType;
            _loop = true;
            _onComplete += Restart;
            return this;
        }

        public bool Tweening()
        {
            _nullCheck?.Invoke();
            if (_targetIsNull)
            {
                return false;
            }

            _currentTime += Time.deltaTime;

            if (_currentDelay != 0)
            {
                if (_currentTime >= _delay)
                {
                    TriggerDelay();
                }
                return true;
            }

            _lerpValue = GetLerpValue(_currentTime, _duration);

            if (_loop && _targetLerp == 0)
                _lerpValue = LoopTypes.LoopValue(_loopType, _lerpValue);

            _easeValue = Easing.EasingValue(_easing, _lerpValue);

            if (_currentTime >= _duration)
            {
                _easeValue = 1;
                UpdateValue();
                _currentDelay = _delay;
                _onComplete?.Invoke();

                if (!_loop)
                    return false;
                else
                {
                    Restart();

                    if (_targetLerp == 1)
                        _targetLerp = 0;
                    else if(_targetLerp == 0)
                        _targetLerp = 1;

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
            _onDelay?.Invoke();
            _currentDelay = 0;
            _currentTime = 0;
            UpdateValue();
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

