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
        protected Action _onDelay;

        public Action _nullCheck;
        public bool _targetIsNull;

        protected float _currentTime;

        protected float _delay;
        protected bool _delayEnabled;
        protected bool _delayTriggered;
        protected bool _delayTriggerActivated;

        protected Loops _loopType;
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
        public Tween Delay(float delay)
        {
            _delay = delay;
            _delayEnabled = true;
            _currentTime -= delay;
            _delayTriggered = true;
            return this;
        }
        public Tween Delay(float delay, Action onDelay)
        {
            _onDelay += onDelay;
            _delay = delay;
            _delayEnabled = true;
            _currentTime -= delay;
            _delayTriggered = true;
            return this;
        }

        public virtual Tween Loop(Loops loopType)
        {
            _loopForever = true;
            _loopType = loopType;
            _loop = true;
            return this;
        }

        public virtual Tween Loop(Loops loopType, int loopCount)
        {
            _loopType = loopType;
            _loopCount = loopCount - 1;
            _loop = true;
            return this;
        }
        public virtual Tween Loop(Loops loopType, int loopCount, Action onAllLoopsComplete)
        {
            _loopType = loopType;
            _loopCount = loopCount - 1;
            _onAllLoopsComplete += onAllLoopsComplete;
            _loop = true;
            return this;
        }

        public bool Tweening()
        {
            //stop tween if target is null
            _nullCheck?.Invoke();
            if (_targetIsNull)
            {
                return false;
            }

            //update delay timer if enabled and delay hasnt been triggered
            if (_delayEnabled && !_delayTriggered)
                UpdateDelayTimer();

            //+time
            _currentTime += Time.deltaTime;

            //continue only if delay timer is done
            if (_currentTime <= 0)
                return true;
            else if (_delayEnabled && _currentTime > 0 && !_delayTriggerActivated)
            {
                _delayTriggerActivated = true;
                _onDelay?.Invoke();
            }

            _lerpValue = GetLerpValue(_currentTime, _duration);
            //basically just if loop type is reverse, make lerpvalue go backwards
            if (_loop && _targetLerp == 0)
                _lerpValue = Looping.LoopValue(_loopType, _lerpValue);

            _easeValue = Easing.EasingValue(_easing, _lerpValue);

            //when timer finishes
            if (_currentTime >= _duration)
            {
                //for basic tween finishes, just set the lerp to 1
                _easeValue = _targetLerp;
                UpdateValue();

                _onComplete?.Invoke();

                //after a loop finishes
                if (_loopCount > 0 || _loopForever)
                {
                    //set the direction of the tween if its reversing
                    if (_loopType == Loops.Rewind)
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
                //if the full loop count cycle completes
                else if (_loopCount <= 0 && !_loopForever)
                {
                    _loop = false;
                    _onAllLoopsComplete?.Invoke();

                    Restart();

                    //make sure yoyo loop is at the right final value
                    if (_loopType == Loops.Yoyo)
                        _easeValue = 0;

                    UpdateValue();

                    return false;
                }
            }
            else
            {
                //always executes during update if delay finished and tween still active
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

        void UpdateDelayTimer()
        {
            _lerpValue = 0;
            if (_loopType == Loops.Rewind)
            {
                if (_targetLerp == 1)
                    _lerpValue = 0;
                else if (_targetLerp == 0)
                    _lerpValue = 1;
            }
            _easeValue = Easing.EasingValue(_easing, _lerpValue);
            _delayTriggered = true;
            UpdateValue();
            _currentTime -= _delay;
        }

        public void Restart()
        {
            _delayTriggered = false;
            _delayTriggerActivated = false;

            _lerpValue = _targetLerp;
            _currentTime = 0;
            _easeValue = Easing.EasingValue(_easing, _lerpValue);

            //set target ease for final value on yoyo loop
            if (_loopType == Loops.Yoyo && (_loopCount > 0 || _loopForever))
                _easeValue = 0;
            else if (_loopType == Loops.Yoyo && _loopForever)
                _easeValue = 1;

            if(_loopType == Loops.Incremental)
                _easeValue = 0;

            if (_loopType != Loops.Rewind)
                UpdateValue();
        }

        public void StopLoop()
        {
            _loop = false;
        }
    }
}

