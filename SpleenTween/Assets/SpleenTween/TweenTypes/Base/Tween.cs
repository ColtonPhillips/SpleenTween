using System;
using UnityEngine;

namespace Spleen
{
    public class Tween
    {
        protected float _duration;
        protected Ease _easing;
        public Action _onComplete;

        protected float _currentTime;
        protected float _delay;
        protected float _easeValue;

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
            return this;
        }

        public bool Tweening()
        {
            _currentTime += Time.deltaTime;

            if(_delay != 0)
            {
                if(_currentTime >= _delay)
                {
                    _delay = 0;
                    _currentTime = Time.deltaTime;
                }
                return true;
            }

            float lerpValue = GetLerpValue(_currentTime, _duration);
            _easeValue = Easing.EasingValue(_easing, lerpValue);
                
            if (_currentTime >= _duration)
            {
                _easeValue = 1;
                UpdateValue();
                _onComplete?.Invoke();

                return false;
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
    }
}

