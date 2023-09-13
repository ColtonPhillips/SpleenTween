using UnityEngine;

namespace SpleenTween
{
    public class TweenPositionY : Tween
    {
        float _from;
        float _to;
        float _value;

        public override void UpdateValue(float easeValue)
        {
            _value = Mathf.LerpUnclamped(_from, _to, easeValue);
            transform.position = new Vector3(transform.position.x, _value, transform.position.z);
        }
        public void Inject(float from, float to)
        {
            this._from = from;
            this._to = to;
        }
    }
}

