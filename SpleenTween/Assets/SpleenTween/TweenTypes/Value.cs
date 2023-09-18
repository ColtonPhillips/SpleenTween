using System;
using UnityEngine;

namespace SpleenTween
{
    public class Value : Tween
    {
        public Action<float> _onUpdate;
        float _from;
        float _to;

        float _value;

        public Value(float from, float to, float duration, Ease easing, Action<float> onUpdate) : base(duration, easing)
        {
            _from = from;
            _to = to;
            _duration = duration;
            _easing = easing;
            _onUpdate += onUpdate;
        }

        public override void UpdateValue()
        {
            _value = Mathf.LerpUnclamped(_from, _to, _easeValue);
            _onUpdate?.Invoke(_value);
        }

        void CheckIfLoopYoyo(Loop loopType)
        {
            if (loopType == SpleenTween.Loop.Yoyo)
            {
                _onComplete += () =>
                {
                    float from = _from;
                    float to = _to;

                    _from = to;
                    _to = from;
                };
            }
        }
        public override Tween Loop(Loop loopType)
        {
            CheckIfLoopYoyo(loopType);
            base.Loop(loopType);
            return this;
        }
        public override Tween Loop(Loop loopType, int loopCount)
        {
            CheckIfLoopYoyo(loopType);
            base.Loop(loopType, loopCount);
            return this;
        }
        public override Tween Loop(Loop loopType, int loopCount, Action onAllLoopsComplete)
        {
            CheckIfLoopYoyo(loopType);
            base.Loop(loopType, loopCount, onAllLoopsComplete);
            return this;
        }
    }
}

