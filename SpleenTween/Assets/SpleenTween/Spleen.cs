using System;
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
            Tween<T> tween = new(from, to, duration, ease, onUpdate, () => target == null || target.Equals(null));
            SpleenTweenManager.StartTween(tween);
            return tween;
        }

        static Tween<T> CreateRelativeTargetTween<T, K>(K target, T increment, float duration, Ease ease, Func<T> currentVal, Action<T, T> onUpdate)
        {
            T current = currentVal.Invoke();
            T from = current;
            T to = SpleenExt.AddGeneric(current, increment);

            Tween<T> tween = new(from, to, duration, ease, (val) =>
            {
                onUpdate.Invoke(val, current);
                current = val;
            }, () => target == null || target.Equals(null));

            tween.OnStart(() =>
            {
                if (Looping.IsLoopWeird(tween.LoopType)) return;

                current = currentVal.Invoke();
                from = current;
                to = SpleenExt.AddGeneric(current, increment);
                tween.from = from;
                tween.to = to;
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
            CreateTargetTween(target, from, to, duration, ease, val => SpleenExt.SetPosAxis(SpleenExt.Axes.x, target, val));
        public static Tween<float> PosY(Transform target, float from, float to, float duration, Ease ease) =>
            CreateTargetTween(target, from, to, duration, ease, val => SpleenExt.SetPosAxis(SpleenExt.Axes.y, target, val));
        public static Tween<float> PosZ(Transform target, float from, float to, float duration, Ease ease) =>
            CreateTargetTween(target, from, to, duration, ease, val => SpleenExt.SetPosAxis(SpleenExt.Axes.z, target, val));

        public static Tween<Vector3> AddPos(Transform target, Vector3 increment, float duration, Ease easing) =>
            CreateRelativeTargetTween(target, increment, duration, easing, () => target.position, (val, from) => target.position += val - from);
        public static Tween<float> AddPosX(Transform target, float increment, float duration, Ease easing) =>
            CreateRelativeTargetTween(target, increment, duration, easing, () => target.position.x, (val, from) => 
            SpleenExt.AddPosAxis(SpleenExt.Axes.x, target, val - from));
        public static Tween<float> AddPosY(Transform target, float increment, float duration, Ease easing) =>
            CreateRelativeTargetTween(target, increment, duration, easing, () => target.position.y, (val, from) =>
            SpleenExt.AddPosAxis(SpleenExt.Axes.y, target, val - from));

        public static Tween<float> AddPosZ(Transform target, float increment, float duration, Ease easing) =>
            CreateRelativeTargetTween(target, increment, duration, easing, () => target.position.z, (val, from) =>
            SpleenExt.AddPosAxis(SpleenExt.Axes.z, target, val - from));
    }
}
