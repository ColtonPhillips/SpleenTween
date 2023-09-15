using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpleenTween
{
    public class Scale : TweenGameObject
    {
        readonly Vector3 _from;
        readonly Vector3 _to;

        public Scale(GameObject target, Vector3 from, Vector3 to, float duration, Ease easing) : base(target, duration, easing)
        {
            _target = target;
            _from = from;
            _to = to;
            _duration = duration;
            _easing = easing;
        }

        public override void UpdateValue()
        {
            _target.transform.localScale = Vector3.LerpUnclamped(_from, _to, _easeValue);
        }
    }
}

