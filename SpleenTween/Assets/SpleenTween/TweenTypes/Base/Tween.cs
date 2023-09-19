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
        protected Action _onStart;

        public Action _nullCheck;
        public bool _targetIsNull;

        protected float _currentTime;

        protected float _delayDuration;
        protected bool _delayEnabled;
        protected bool _startTriggered;

        protected LoopType _loopType;
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
        public Tween OnStart(Action onStart)
        {
            _onStart += onStart;
            return this;
        }

        public Tween Delay(float delayDuration)
        {
            _delayDuration = delayDuration;
            _delayEnabled = true;
            _currentTime -= delayDuration;
            return this;
        }

        public virtual Tween Loop(LoopType loopType)
        {
            _loopForever = true;
            _loopType = loopType;
            _loop = true;
            return this;
        }

        public virtual Tween Loop(LoopType loopType, int loopCount)
        {
            _loopType = loopType;
            _loopCount = loopCount - 1;
            _loop = true;
            return this;
        }
        public virtual Tween Loop(LoopType loopType, int loopCount, Action onAllLoopsComplete)
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
                return false;

            _currentTime += Time.deltaTime;

            if (_currentTime < 0)
                return true;
            
            if (_currentTime >= 0 && !_startTriggered)
            {
                _onStart?.Invoke();
                _startTriggered = true;
            }

            _lerpValue = GetLerpValue(_currentTime, _duration);
            if (_loop && _targetLerp == 0)
                _lerpValue = Looping.LoopValue(_loopType, _lerpValue);
            _easeValue = Easing.EasingValue(_easing, _lerpValue);

            if(_currentTime < _duration)
            {
                UpdateValue();
                return true;
            }

            _easeValue = _targetLerp;
            UpdateValue();

            _onComplete?.Invoke();

            if (_loopCount > 0 || _loopForever)
            {
                if (_loopType == LoopType.Rewind)
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

            if (_loopCount <= 0 && !_loopForever)
            {
                _loop = false;
                _onAllLoopsComplete?.Invoke();

                Restart();

                if (_loopType == LoopType.Yoyo)
                    _easeValue = 0;

                UpdateValue();

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

        public void Restart()
        {
            _startTriggered = false;

            _lerpValue = _targetLerp;
            _currentTime = 0;
            _easeValue = Easing.EasingValue(_easing, _lerpValue);

            //set target ease for final value on yoyo loop
            if (_loopType == LoopType.Yoyo && (_loopCount > 0 || _loopForever))
                _easeValue = 0;
            else if (_loopType == LoopType.Yoyo && _loopForever)
                _easeValue = 1;

            if(_loopType == LoopType.Incremental)
                _easeValue = 0;

            if (_loopType != LoopType.Rewind)
                UpdateValue();


            if (_delayEnabled)
                _currentTime -= _delayDuration;
        }

        public void StopLoop()
        {
            _loop = false;
        }
    }
}

