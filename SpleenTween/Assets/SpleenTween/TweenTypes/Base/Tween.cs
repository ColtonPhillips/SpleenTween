using System;
using UnityEngine;

namespace SpleenTween
{
    public class Tween : MonoBehaviour
    {
        public Action _onComplete;

        float _currentTime;
        float _duration;

        Ease _easing;

        void Update()
        {
            _currentTime += Time.deltaTime;

            float easeValue = Easing.EasingValue(_easing, LerpValue(_currentTime, _duration));
            UpdateValue(easeValue);

            if (_currentTime >= _duration)
            {
                UpdateValue(1);
                _onComplete?.Invoke();

                Destroy(this);
            }
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

        public void InjectBase(float duration, Ease easing)
        {
            this._duration = duration;
            this._easing = easing;
        }

        public virtual void UpdateValue(float easeValue) { }
    }
}

