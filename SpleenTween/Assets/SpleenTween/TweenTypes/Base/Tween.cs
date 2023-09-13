using System;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

namespace SpleenTween
{
    public class Tween
    {
        public Action _onComplete;

        protected float _currentTime;
        protected float _duration;

        protected Ease _easing;
        protected float _easeValue;

        public Tween(float duration, Ease easing)
        {
            this._duration = duration;
            this._easing = easing;
        }

        public Tween OnComplete(Action action)
        {
            _onComplete = action;
            return this;
        }

        float LerpValue(float time, float duration)
        {
            float lerpValue = time / duration;
            return lerpValue;
        }

        public bool Tweening()
        {
            _currentTime += Time.deltaTime;
            _easeValue = Easing.EasingValue(_easing, LerpValue(_currentTime, _duration));

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

        public virtual void UpdateValue()
        {
            
        }
    }
}

