using UnityEngine;

namespace Spleen
{
    public class TweenTransform : Tween
    {
        public Transform _target;

        public TweenTransform(Transform target, float duration, Ease easing) : base(duration, easing)
        {
            _target = target;
        }
    }
}

