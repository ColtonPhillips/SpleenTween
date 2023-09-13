using UnityEngine;

namespace SpleenTween
{
    public class TweenScaleZ : Tween
    {
        float _from;
        float _to;
        float _value;

        public override void UpdateValue(float easeValue)
        {
            _value = Mathf.LerpUnclamped(_from, _to, easeValue);
            transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, _value);
        }
        public void Inject(float from, float to)
        {
            this._from = from;
            this._to = to;
        }
    }
}

