using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpleenTween
{
    public class Spleen : MonoBehaviour
    {
        static Tween<T> CreateTween<T>(T from, T to, float duration, Ease ease, Action<T> onUpdate)
        {
            Tween<T> tween = new(from, to, duration, ease, onUpdate);
            SpleenTweenManager.StartTween(tween);
            return tween;
        }
        static Tween<T> CreateTargetTween<T,K>(K target, T from, T to, float duration, Ease ease, Action<T> onUpdate)
        {
            Tween<T> tween = new(from, to, duration, ease, onUpdate, () =>
            {
                return target == null || target.Equals(null);
            });
            SpleenTweenManager.StartTween(tween);
            return tween;
        }
        static Tween<T> CreateRelativeTween<T>()
        {
            return null;
        }
        static Tween<T> CreateRelativeTargetTween<T, K>(K target, T increment, float duration, Ease ease, Func<T> initialVal, Action<T,T> onUpdate)
        {
            T initial = initialVal.Invoke();
            Tween<T> tween = new(initial, increment, duration, ease, (val) =>
            {
                onUpdate.Invoke(val, initial);
            });
            tween.OnUpdate((val) => initial = val);

            tween.OnStart(() =>
            {
                if (tween.LoopType != Loop.Rewind && tween.LoopType != Loop.Yoyo)
                {
                    initial = initialVal.Invoke();
                    tween.from = initial;
                    tween.to = SpleenExt.AddGeneric<T>(initial, increment);
                }
            });
            SpleenTweenManager.StartTween(tween);
            return tween;
        }


        public static Tween<float> Value(float from, float to, float duration, Ease ease, Action<float> onUpdate) => 
            CreateTween(from, to, duration, ease, onUpdate);
        public static Tween<Vector3> Value3(Vector3 from, Vector3 to, float duration, Ease ease, Action<Vector3> onUpdate) => 
            CreateTween(from, to, duration, ease, onUpdate);

        public static Tween<Vector3> Pos(Transform target, Vector3 from, Vector3 to, float duration, Ease ease) => 
            CreateTargetTween(target, from, to, duration, ease, val => target.position = val);
        public static Tween<float> PosX(Transform target, float from, float to, float duration, Ease ease) =>
            CreateTargetTween(target, from, to, duration, ease, val => SpleenExt.SetPosAxis(SpleenExt.Axes.X, target, val));
        public static Tween<float> PosY(Transform target, float from, float to, float duration, Ease ease) =>
            CreateTargetTween(target, from, to, duration, ease, val => SpleenExt.SetPosAxis(SpleenExt.Axes.Y, target, val));
        public static Tween<float> PosZ(Transform target, float from, float to, float duration, Ease ease) =>
            CreateTargetTween(target, from, to, duration, ease, val => SpleenExt.SetPosAxis(SpleenExt.Axes.Z, target, val));

        public static Tween<Vector3> AddPos(Transform target, Vector3 increment, float duration, Ease easing)
        {
            return CreateRelativeTargetTween(target, increment, duration, easing, () => target.position, (val, from) => target.position += val - from);
        }

        public static Tween<Vector3> AddPosTesting(Transform target, Vector3 increment, float duration, Ease easing)
        {
            Vector3 from = target.position;
            Tween<Vector3> tween = new(from, increment, duration, easing, (val) =>
            {
                target.position += val - from;
                from = val;
            });
            tween.OnStart(() =>
            {
                if (tween.LoopType != Loop.Rewind && tween.LoopType != Loop.Yoyo)
                {
                    from = target.position;
                    tween.from = from;
                    tween.to = from + increment;
                }
            });

            SpleenTweenManager.StartTween(tween);
            return tween;
        }
    }
}
