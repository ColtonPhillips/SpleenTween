using System;
using UnityEngine;

namespace SpleenTween
{
    public class Tween
    {
        protected float _duration;
        protected Ease _easing;
        public Action _onComplete;

        protected float _currentTime;
        protected float _easeValue;

        public Tween(float duration, Ease easing)
        {
            this._duration = duration;
            this._easing = easing;
        }
        public Tween OnComplete(Action onComplete)
        {
            _onComplete = onComplete;
            return this;
        }

        public bool Tweening()
        {
            _currentTime += Time.deltaTime;
            _easeValue = Easing.EasingValue(_easing, GetLerpValue(_currentTime, _duration));
            UpdateValue();

            if (_currentTime >= _duration)
            {
                _easeValue = 1;
                UpdateValue();
                _onComplete?.Invoke();

                return false;
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

