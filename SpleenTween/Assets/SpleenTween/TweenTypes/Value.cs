using System;
using UnityEngine;

namespace Spleen
{
    public class Value : Tween
    {
        public Action<float> _onUpdate;
        float _from;
        float _to;

        public float _value;

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

        void LoopSpecificActions(LoopType loopType)
        {
            if (loopType == LoopType.Yoyo)
            {
                _onComplete += () =>
                {
                    float from = _from;
                    float to = _to;

                    _from = to;
                    _to = from;
                };
            }
            else if(loopType == LoopType.Incremental)
            {
                _onComplete += () =>
                {
                    float diff = _to - _from;
                    _from += diff;
                    _to += diff;
                };
            }
        }
        public override Tween Loop(LoopType loopType)
        {
            LoopSpecificActions(loopType);
            base.Loop(loopType);
            return this;
        }
        public override Tween Loop(LoopType loopType, int loopCount)
        {
            LoopSpecificActions(loopType);
            base.Loop(loopType, loopCount);
            return this;
        }
        public override Tween Loop(LoopType loopType, int loopCount, Action onAllLoopsComplete)
        {
            LoopSpecificActions(loopType);
            base.Loop(loopType, loopCount, onAllLoopsComplete);
            return this;
        }
    }
}

