using System;
using UnityEngine;

namespace SpleenTween
{
    public class Value : Tween
    {
        public Action<float> onUpdate;
        public float from;
        public float to;

        public float value;

        public Value(float from, float to, float duration, Ease easing, Action<float> onUpdate) : base(duration, easing)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            this.easing = easing;
            this.onUpdate += onUpdate;
        }

        public override void UpdateValue()
        {
            value = Mathf.LerpUnclamped(from, to, easeValue);
            onUpdate?.Invoke(value);
        }

        void LoopSpecificActions(LoopType loopType)
        {
            if (loopType == LoopType.Yoyo)
            {
                onComplete += () =>
                {
                    float from = this.from;
                    float to = this.to;

                    from = to;
                    to = from;
                };
            }
            else if(loopType == LoopType.Incremental)
            {
                onComplete += () =>
                {
                    float diff = to - from;
                    from += diff;
                    to += diff;
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

