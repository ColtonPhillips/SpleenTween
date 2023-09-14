using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpleenTween
{
    public class FloatTween : Tween
    {
        readonly Action<float> _onUpdate;
        readonly float _from;
        readonly float _to;

        float _value;

        public FloatTween(float from, float to, float duration, Ease easing, Action<float> onUpdate) : base(duration, easing)
        {
            this._from = from;
            this._to = to;
            this._duration = duration;
            this._easing = easing;
            this._onUpdate = onUpdate;
        }

        public override void UpdateValue()
        {
            _value = Mathf.LerpUnclamped(_from, _to, _easeValue);
            _onUpdate?.Invoke(_value);
        }
    }
}

