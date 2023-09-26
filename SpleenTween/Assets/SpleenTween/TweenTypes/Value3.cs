using System;
using UnityEngine;

namespace SpleenTween
{
    public class Value3 : Tween
    {
        public Action<Vector3> onUpdate;
        public Vector3 from;
        public Vector3 to;

        public Vector3 value;

        public Value3(Vector3 from, Vector3 to, float duration, Ease easing, Action<Vector3> onUpdate) : base(duration, easing)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            this.easing = easing;
            this.onUpdate += onUpdate;
        }

        public override void UpdateValue()
        {
            value = Vector3.LerpUnclamped(from, to, easeValue);
            onUpdate?.Invoke(value);
        }

        void LoopSpecificActions(LoopType loopType)
        {
            if (loopType == LoopType.Yoyo)
            {
                onComplete += () =>
                {
                    Vector3 from = this.from;
                    Vector3 to = this.to;
                    this.from = to;
                    this.to = from;
                };
            }
            else if (loopType == LoopType.Incremental)
            {
                onComplete += () =>
                {
                    Vector3 diff = to - from;
                    this.from += diff;
                    this.to += diff;
                };
            }
        }
        public override Tween Loop(LoopType loopType)
        {
            base.Loop(loopType);
            LoopSpecificActions(loopType);
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

        public override Tween Loop(LoopType loopType, int loopCount, float startDelay, Action onAllLoopsComplete)
        {
            LoopSpecificActions(loopType);
            base.Loop(loopType, startDelay);
            return this;
        }
    }
}

