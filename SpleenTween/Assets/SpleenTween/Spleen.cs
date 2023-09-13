using System;
using UnityEngine;

namespace SpleenTween
{
    public class Spleen : MonoBehaviour
    {
        #region Float
        public static Tween Float(GameObject targetObj, float from, float to, float duration, Action<float> onUpdate, Ease easing)
        {
            TweenFloat tween = targetObj.AddComponent<TweenFloat>();
            tween.InjectBase(duration, easing);
            tween.Inject(from, to, onUpdate);
            return tween;
        }
        #endregion

        #region Position
        public static Tween Position(GameObject targetObj, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            TweenPosition tween = targetObj.AddComponent<TweenPosition>();
            tween.InjectBase(duration, easing);
            tween.Inject(from, to);
            return tween;
        }
        public static Tween PositionX(GameObject targetObj, float from, float to, float duration, Ease easing)
        {
            TweenPositionX tween = targetObj.AddComponent<TweenPositionX>();
            tween.InjectBase(duration, easing);
            tween.Inject(from, to);
            return tween;
        }
        public static Tween PositionY(GameObject targetObj, float from, float to, float duration, Ease easing)
        {
            TweenPositionY tween = targetObj.AddComponent<TweenPositionY>();
            tween.InjectBase(duration, easing);
            tween.Inject(from, to);
            return tween;
        }
        public static Tween PositionZ(GameObject targetObj, float from, float to, float duration, Ease easing)
        {
            TweenPositionZ tween = targetObj.AddComponent<TweenPositionZ>();
            tween.InjectBase(duration, easing);
            tween.Inject(from, to);
            return tween;
        }
        #endregion

        #region Scale
        public static Tween Scale(GameObject targetObj, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            TweenScale tween = targetObj.AddComponent<TweenScale>();
            tween.InjectBase(duration, easing);
            tween.Inject(from, to);
            return tween;
        }
        public static Tween ScaleX(GameObject targetObj, float from, float to, float duration, Ease easing)
        {
            TweenScaleX tween = targetObj.AddComponent<TweenScaleX>();
            tween.InjectBase(duration, easing);
            tween.Inject(from, to);
            return tween;
        }
        public static Tween ScaleY(GameObject targetObj, float from, float to, float duration, Ease easing)
        {
            TweenScaleY tween = targetObj.AddComponent<TweenScaleY>();
            tween.InjectBase(duration, easing);
            tween.Inject(from, to);
            return tween;
        }
        public static Tween ScaleZ(GameObject targetObj, float from, float to, float duration, Ease easing)
        {
            TweenScaleZ tween = targetObj.AddComponent<TweenScaleZ>();
            tween.InjectBase(duration, easing);
            tween.Inject(from, to);
            return tween;
        }
        #endregion
    }
}

