using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using Unity.VisualScripting;
using UnityEngine;

namespace Spleen
{
    public class Tween
    {
        protected float _duration;
        protected Ease _easing;
        protected Action _onComplete;
        Action _onStart;

        public GameObject _target;
        public Action _nullCheck;
        public bool _targetIsNull;

        float _currentTime;

        float _delayDuration;
        bool _delayEnabled;
        bool _startTriggered;

        protected LoopType _loopType;
        float _loopDelay;

        protected float _easeValue;
        float _lerpValue;

        bool _loop;
        int _loopCount;
        Action _onAllLoopsComplete;
        bool _loopForever = false;

        int _targetLerp = 1;

        List<Tween> chainedTweens = new();

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

        public Tween Chain(Tween tween)
        {
            chainedTweens.Add(tween);
            float waitTime = this._duration + this._delayDuration;
            foreach(Tween t in chainedTweens)
            {
                if(t != tween)
                {
                    waitTime += t._duration + t._delayDuration;
                }
            }
            tween._currentTime -= waitTime;

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
                SpleenTween.StopTween(this);

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

                SpleenTween.StopTween(this);
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

