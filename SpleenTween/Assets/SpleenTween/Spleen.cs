using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpleenTween
{
    public class Spleen : MonoBehaviour
    {
        static Tween<T> CreateTween<T>(T from, T to, float duration, Ease ease, Action<T> update)
        {
            Tween<T> tween = new(from, to, duration, ease, update);
            SpleenTweenManager.StartTween(tween);
            return tween;
        }
        static Tween<T> CreateTargetTween<T,K>(K target, T from, T to, float duration, Ease ease, Action<T> update)
        {
            Tween<T> tween = new(from, to, duration, ease, update, () =>
            {
                return target == null || target.Equals(null);
            });
            SpleenTweenManager.StartTween(tween);
            return tween;
        }

        public static Tween<float> Value(float from, float to, float duration, Ease ease, Action<float> update) => 
            CreateTween(from, to, duration, ease, update);
        public static Tween<Vector3> Value3(Vector3 from, Vector3 to, float duration, Ease ease, Action<Vector3> update) => 
            CreateTween(from, to, duration, ease, update);

        public static Tween<Vector3> Pos(Transform target, Vector3 from, Vector3 to, float duration, Ease ease) => 
            CreateTargetTween(target, from, to, duration, ease, val => target.position = val);
        public static Tween<float> PosX(Transform target, float from, float to, float duration, Ease ease) =>
            CreateTargetTween(target, from, to, duration, ease, val => SpleenExt.SetPosAxis(SpleenExt.Axes.X, target, val));
        public static Tween<float> PosY(Transform target, float from, float to, float duration, Ease ease) =>
            CreateTargetTween(target, from, to, duration, ease, val => SpleenExt.SetPosAxis(SpleenExt.Axes.Y, target, val));
        public static Tween<float> PosZ(Transform target, float from, float to, float duration, Ease ease) =>
            CreateTargetTween(target, from, to, duration, ease, val => SpleenExt.SetPosAxis(SpleenExt.Axes.Z, target, val));
    }
}
