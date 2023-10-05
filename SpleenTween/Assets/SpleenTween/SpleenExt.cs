using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpleenTween
{
    public class SpleenExt : MonoBehaviour
    {
        public enum Axes {X,Y,Z};

        static void SetTransformAxis(Transform target, float axisVal, Axes axis, Action<Vector3> setAxis)
        {
            Vector3 newTransform = target.position;
            switch (axis)
            {
                case Axes.X: newTransform.x = axisVal; break;
                case Axes.Y: newTransform.y = axisVal; break;
                case Axes.Z: newTransform.z = axisVal; break;
            }
            setAxis(newTransform);
        }

        public static void SetPosAxis(Transform target, float axisVal, Axes axis) => SetTransformAxis(target, axisVal, axis, (val) => target.transform.position = val);
        public static void SetScaleAxis(Transform target, float axisVal, Axes axis) => SetTransformAxis(target, axisVal, axis, (val) => target.transform.localScale = val);
        public static void SetRotAxis(Transform target, float axisVal, Axes axis) => SetTransformAxis(target, axisVal, axis, (val) => target.transform.eulerAngles = val);
    }
}
