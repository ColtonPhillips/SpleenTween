using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Spleen
{
    public class Value : Tween
    {
        readonly Action<float> _onUpdate;
        readonly float _from;
        readonly float _to;

        float _value;

        public Value(float from, float to, float duration, Ease easing, Action<float> onUpdate) : base(duration, easing)
        {
            _from = from;
            _to = to;
            _duration = duration;
            _easing = easing;
            _onUpdate = onUpdate;
        }

        public override void UpdateValue()
        {
            _value = Mathf.LerpUnclamped(_from, _to, _easeValue);
            _onUpdate?.Invoke(_value);
        }
    }
}

