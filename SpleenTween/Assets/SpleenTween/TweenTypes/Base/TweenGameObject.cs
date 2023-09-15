using UnityEngine;

namespace SpleenTween
{
    public class TweenGameObject : Tween
    {
        public GameObject _target;

        public TweenGameObject(GameObject target, float duration, Ease easing) : base(duration, easing)
        {
            _target = target;
        }
    }
}

