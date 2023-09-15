using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpleenTween
{
    public class ScaleZ : TweenGameObject
    {
        readonly float _from;
        readonly float _to;

        float _value;

        public ScaleZ(GameObject target, float from, float to, float duration, Ease easing) : base(target, duration, easing)
        {
            _target = target;
            _from = from;
            _to = to;
            _duration = duration;
            _easing = easing;
        }

        public override void UpdateValue()
        {
            _value = Mathf.LerpUnclamped(_from, _to, _easeValue);
            _target.transform.localScale = new Vector3(_target.transform.localScale.x, _target.transform.localScale.y, _value);
        }
    }
}

