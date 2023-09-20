using System;
using UnityEngine;

namespace Spleen
{
    public class Value3 : Tween
    {
        public Action<Vector3> _onUpdate;
        public Vector3 _from;
        public Vector3 _to;

        public Vector3 _value;

        public Value3(Vector3 from, Vector3 to, float duration, Ease easing, Action<Vector3> onUpdate) : base(duration, easing)
        {
            _from = from;
            _to = to;
            _duration = duration;
            _easing = easing;
            _onUpdate += onUpdate;
        }

        public override void UpdateValue()
        {
            _value = Vector3.LerpUnclamped(_from, _to, _easeValue);
            _onUpdate?.Invoke(_value);
        }

        void LoopSpecificActions(LoopType loopType)
        {
            if (loopType == LoopType.Yoyo)
            {
                _onComplete += () =>
                {
                    Vector3 from = _from;
                    Vector3 to = _to;
                    _from = to;
                    _to = from;
                };
            }
            else if (loopType == LoopType.Incremental)
            {
                _onComplete += () =>
                {
                    Vector3 diff = _to - _from;
                    _from += diff;
                    _to += diff;
                };
            }
        }
        public override Tween Loop(LoopType loopType)
        {
            base.Loop(loopType);
            LoopSpecificActions(_loopType);
            return this;
        }
        public override Tween Loop(LoopType loopType, int loopCount)
        {
            base.Loop(loopType, loopCount);
            LoopSpecificActions(loopType);
            return this;
        }
        public override Tween Loop(LoopType loopType, int loopCount, Action onAllLoopsComplete)
        {
            base.Loop(loopType, loopCount, onAllLoopsComplete);
            LoopSpecificActions(loopType);
            return this;
        }
    }
}

