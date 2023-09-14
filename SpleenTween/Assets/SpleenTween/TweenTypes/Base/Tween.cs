using System;
using UnityEngine;

namespace Spleen
{
    public class Tween
    {
        protected float _duration;
        protected Ease _easing;
        public Action _onComplete;
        public Action _onLerpValue;

        float _lerpValueTrigger;
        bool _lerpValueTriggered;

        protected float _currentTime;
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
        public Tween OnLerpValue(float lerpValue, Action onLerpValue)
        {
            lerpValue = Mathf.Clamp01(lerpValue);
            _lerpValueTrigger = lerpValue;
            _onLerpValue = onLerpValue;
            return this;
        }

        public bool Tweening()
        {
            _currentTime += Time.deltaTime;
            float lerpValue = GetLerpValue(_currentTime, _duration);
            _easeValue = Easing.EasingValue(_easing, lerpValue);

            if (!_lerpValueTriggered && _currentTime >= _lerpValueTrigger)
            {
                _lerpValueTriggered = true;
                _onLerpValue?.Invoke();
            }
                
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

