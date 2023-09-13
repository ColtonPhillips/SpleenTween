using System;
using UnityEngine;

namespace SpleenTween
{
    public class TweenInstance : MonoBehaviour
    {
        //LATER -- MAKE BASE CLASS TWEEN WITH VIRTUAL VOID ONUPDATE, THEN MAKE DIFFERENT CHILD CLASSES WITH OVERRIDE UPDATES FOR EASIER AND FASTER CODE

        Action<float> _onUpdate;

        public Action _onComplete;

        float _from;
        float _to;
        float _value;

        Vector3 _from3;
        Vector3 _to3;

        float _currentTime;
        float _duration;

        Ease _easing;
        TweenType _tweenType;

        public TweenInstance OnComplete(Action action)
        {
            _onComplete = action;
            return this;
        }

        void Update()
        {
            _currentTime += Time.deltaTime;

            float lerpValue = _currentTime / _duration;
            lerpValue = Mathf.Clamp01(lerpValue);

            float easeValue = Easing.EasingValue(_easing, lerpValue);

            switch (_tweenType)
            {
                case TweenType.Float: UpdateFloat(easeValue); break;
                case TweenType.Position: UpdatePosition(easeValue); break;
            }

            if (_currentTime >= _duration)
            {
                _onComplete?.Invoke();
                Destroy(this);
            }
        }


        void UpdateFloat(float easeValue)
        {
            _value = Mathf.Lerp(_from, _to, easeValue);
            _onUpdate?.Invoke(_value);
        }
        void UpdatePosition(float easeValue)
        {
            transform.position = Vector3.Lerp(_from3, _to3, easeValue);
        }

        public void InjectFloat(float from, float to, float duration, Action<float> onUpdate, Ease easing)
        {
            _from = from;
            _to = to;
            _duration = duration;
            _onUpdate = onUpdate;
            _easing = easing;

            _tweenType = TweenType.Float;
        }
        public void InjectPosition(Vector3 from, Vector3 to, float duration, Ease easing)
        {
            _from3 = from;
            _to3 = to;
            _duration = duration;
            _easing = easing;

            _tweenType = TweenType.Position;
        }
    }
}

