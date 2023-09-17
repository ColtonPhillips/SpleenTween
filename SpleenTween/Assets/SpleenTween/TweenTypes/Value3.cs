using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpleenTween
{
    public class Value3 : Tween
    {
        public Action<Vector3> _onUpdate;
        readonly Vector3 _from;
        readonly Vector3 _to;

        Vector3 _value;

        public Value3(Vector3 from, Vector3 to, float duration, Ease easing, Action<Vector3> onUpdate) : base(duration, easing)
        {
            _from = from;
            _to = to;
            _duration = duration;
            _easing = easing;
            _onUpdate = onUpdate;
        }

        public override void UpdateValue()
        {
            _value = Vector3.LerpUnclamped(_from, _to, _easeValue);
            _onUpdate?.Invoke(_value);
        }
    }
}

