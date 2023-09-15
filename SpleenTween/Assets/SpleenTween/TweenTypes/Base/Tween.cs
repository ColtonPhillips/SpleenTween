using System;
using System.Reflection;
using Unity.VisualScripting;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using UnityEngine.Diagnostics;

namespace SpleenTween
{
    public class Tween
    {
        protected float _duration;
        protected Ease _easing;
        public Action _onComplete;
        protected Action _onDelay;

        protected float _currentTime;

        protected float _delay;
        protected float _currentDelay;

        protected float _loopDelay;

        protected float _easeValue;
        public float _lerpValue;

        protected bool _loop;

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
        public Tween Loop()
        {
            _loop = true;
            _onComplete += Restart;
            return this;
        }

        public bool Tweening()
        {
            _currentTime += Time.deltaTime;

            if (_currentDelay != 0)
            {
                TriggerDelay();
                return true;
            }

            _lerpValue = GetLerpValue(_currentTime, _duration);
            _easeValue = Easing.EasingValue(_easing, _lerpValue);
                
            if (_currentTime >= _duration)
            {
                _easeValue = 1;
                UpdateValue();
                _currentDelay = _delay;
                _onComplete?.Invoke();

                if (_loop)
                {
                    Restart();
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

        void TriggerDelay()
        {
            if (_currentTime >= _delay)
            {
                _onDelay?.Invoke();
                _currentDelay = 0;
                Restart();
                UpdateValue();
            }
        }

        public void Restart()
        {
            _easeValue = 0;
            _lerpValue = 0f;
            _currentTime = 0;
            UpdateValue();
        }

        public void StopLoop()
        {
            _loop = false;
        }
    }
}

