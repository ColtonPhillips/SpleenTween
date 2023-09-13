using UnityEngine;

namespace SpleenTween
{
    public class TweenScaleX : Tween
    {
        float _from;
        float _to;
        float _value;

        public override void UpdateValue(float easeValue)
        {
            _value = Mathf.LerpUnclamped(_from, _to, easeValue);
            transform.localScale = new Vector3(_value, transform.localScale.y, transform.localScale.z);
        }
        public void Inject(float from, float to)
        {
            this._from = from;
            this._to = to;
        }
    }
}

