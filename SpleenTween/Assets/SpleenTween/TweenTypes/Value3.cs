using System;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

namespace SpleenTween
{
    public class Value3 : Tween
    {
        public Action<Vector3> _onUpdate;
        Vector3 _from;
        Vector3 _to;

        Vector3 _value;

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

        void CheckIfLoopYoyo(Loop loopType)
        {
            if (loopType == SpleenTween.Loop.Yoyo)
            {
                _onComplete += () =>
                {
                    Vector3 from = _from;
                    Vector3 to = _to;
                    _from = to;
                    _to = from;
                };
            }
        }
        public override Tween Loop(Loop loopType)
        {
            base.Loop(loopType);
            CheckIfLoopYoyo(_loopType);
            return this;
        }
        public override Tween Loop(Loop loopType, int loopCount)
        {
            base.Loop(loopType, loopCount);
            CheckIfLoopYoyo(loopType);
            return this;
        }
        public override Tween Loop(Loop loopType, int loopCount, Action onAllLoopsComplete)
        {
            base.Loop(loopType, loopCount, onAllLoopsComplete);
            CheckIfLoopYoyo(loopType);
            return this;
        }
    }
}

