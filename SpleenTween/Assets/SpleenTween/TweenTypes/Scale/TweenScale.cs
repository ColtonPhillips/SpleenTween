using System;
using UnityEngine;

namespace SpleenTween
{
    public class TweenScale : Tween
    {
        Vector3 _from;
        Vector3 _to;
        Vector3 _value;

        public override void UpdateValue(float easeValue)
        {
            _value = Vector3.LerpUnclamped(_from, _to, easeValue);
            transform.localScale = _value;
        }
        public void Inject(Vector3 from, Vector3 to)
        {
            this._from = from;
            this._to = to;
        }
    }
}

