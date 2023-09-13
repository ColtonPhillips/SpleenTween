using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpleenTween
{
    public class TweenFloat : Tween
    {
        Action<float> _onUpdate;
        float _from;
        float _to;
        float _value;

        public override void UpdateValue(float easeValue)
        {
            _value = Mathf.LerpUnclamped(_from, _to, easeValue);
            _onUpdate?.Invoke(_value);
        }
        public void Inject(float from, float to, Action<float> onUpdate)
        {
            this._from = from;
            this._to = to;
            this._onUpdate = onUpdate;
        }
    }
}

