using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpleenTween
{
    public class Spleen : MonoBehaviour
    {
        static Tween<T> CreateTween<T>(T from, T to, float duration, Action<T> update)
        {
            Tween<T> tween = new(from, to, duration, update);
            SpleenTweenManager.StartTween(tween);
            return tween;
        }
        static Tween<T> CreateTargetTween<T,J>(J target, T from, T to, float duration, Action<T> update)
        {
            Tween<T> tween = new(from, to, duration, update, () => target == null || EqualityComparer<J>.Default.Equals(target, default));
            SpleenTweenManager.StartTween(tween);
            return tween;
        }

        public static Tween<float> Value(float from, float to, float duration, Action<float> update) => 
            CreateTween(from, to, duration, update);
        public static Tween<Vector3> Value3(Vector3 from, Vector3 to, float duration, Action<Vector3> update) => 
            CreateTween(from, to, duration, update);

        public static Tween<Vector3> Pos(Transform target, Vector3 from, Vector3 to, float duration) => 
            CreateTargetTween(target, from, to, duration, val => target.position = val);
        public static Tween<float> PosX(Transform target, float from, float to, float duration) =>
            CreateTargetTween(target, from, to, duration, val => SpleenExt.SetPosAxis(target, val, SpleenExt.Axes.X));
        public static Tween<float> PosY(Transform target, float from, float to, float duration) =>
            CreateTargetTween(target, from, to, duration, val => SpleenExt.SetPosAxis(target, val, SpleenExt.Axes.Y));
        public static Tween<float> PosZ(Transform target, float from, float to, float duration) =>
            CreateTargetTween(target, from, to, duration, val => SpleenExt.SetPosAxis(target, val, SpleenExt.Axes.Z));

        public static Tween<Vector3> Scale(Transform target, Vector3 from, Vector3 to, float duration) =>
            CreateTargetTween(target, from, to, duration, val => target.position = val);
        public static Tween<float> ScaleX(Transform target, float from, float to, float duration) =>
            CreateTargetTween(target, from, to, duration, val => SpleenExt.SetScaleAxis(target, val, SpleenExt.Axes.X));
        public static Tween<float> ScaleY(Transform target, float from, float to, float duration) =>
            CreateTargetTween(target, from, to, duration, val => SpleenExt.SetScaleAxis(target, val, SpleenExt.Axes.Y));
        public static Tween<float> ScaleZ(Transform target, float from, float to, float duration) =>
            CreateTargetTween(target, from, to, duration, val => SpleenExt.SetScaleAxis(target, val, SpleenExt.Axes.Z));

        public static Tween<Vector3> Rot(Transform target, Vector3 from, Vector3 to, float duration) =>
            CreateTargetTween(target, from, to, duration, val => target.position = val);
        public static Tween<float> RotX(Transform target, float from, float to, float duration) =>
            CreateTargetTween(target, from, to, duration, val => SpleenExt.SetRotAxis(target, val, SpleenExt.Axes.X));
        public static Tween<float> RotY(Transform target, float from, float to, float duration) =>
            CreateTargetTween(target, from, to, duration, val => SpleenExt.SetRotAxis(target, val, SpleenExt.Axes.Y));
        public static Tween<float> RotZ(Transform target, float from, float to, float duration) =>
            CreateTargetTween(target, from, to, duration, val => SpleenExt.SetRotAxis(target, val, SpleenExt.Axes.Z));
    }
}
