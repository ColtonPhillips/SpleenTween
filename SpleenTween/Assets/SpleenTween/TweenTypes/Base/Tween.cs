using System;
using System.Reflection;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace Spleen
{
    public class Tween
    {
        protected float _duration;
        protected Ease _easing;
        public Action _onComplete;

        protected float _currentTime;

        protected float _delay;
        protected float _currentDelay;

        protected float _loopDelay;

        protected float _easeValue;
        public float _lerpValue;

        bool _loop = false;

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
        public Tween Loop(bool loop, float delay)
        {
            _loop = true;
            _loopDelay = delay;
            return this;
        }

        public bool Tweening()
        {
            _currentTime += Time.deltaTime;
            if (_currentDelay != 0)
            {
                if(_currentTime >= _delay)
                {
                    _currentDelay = 0;
                    _currentTime = 0;
                    _easeValue = 0;
                    UpdateValue();
                }   
                return true;
            }

            _lerpValue = GetLerpValue(_currentTime, _duration);
            _easeValue = Easing.EasingValue(_easing, _lerpValue);
                
            if (_currentTime >= _duration)
            {
                _easeValue = 1;
                UpdateValue();
                _onComplete?.Invoke();
                _currentDelay = _delay;

                if (_loop)
                {
                    _currentTime = 0;
                    _easeValue = 0;
                    UpdateValue();
                    return true;
                }

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

