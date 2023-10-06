using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpleenTween
{
    public class SpleenExt : MonoBehaviour
    {
        public enum Axes {X,Y,Z};

        static void SetTransformAxis(Axes axis, Transform target, float targetVal, Action<Vector3> setAxis)
        {
            Vector3 newTransform = target.position;
            switch (axis)
            {
                case Axes.X: newTransform.x = targetVal; break;
                case Axes.Y: newTransform.y = targetVal; break;
                case Axes.Z: newTransform.z = targetVal; break;
            }
            setAxis(newTransform);
        }

        public static void SetPosAxis(Axes axis, Transform target, float targetVal) => SetTransformAxis(axis, target, targetVal, (val) => target.transform.position = val);
        public static void SetScaleAxis(Axes axis, Transform target, float targetVal) => SetTransformAxis(axis, target, targetVal, (val) => target.transform.localScale = val);
        public static void SetRotAxis(Axes axis, Transform target, float targetVal) => SetTransformAxis(axis, target, targetVal, (val) => target.transform.eulerAngles = val);
    }
}
