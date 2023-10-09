using System;
using UnityEngine;

namespace SpleenTween
{
    public class SpleenExt : MonoBehaviour
    {
        public enum Axes {x,y,z};

        static void SetAxis(Axes axis, Vector3 inVal, float targetVal, Action<Vector3> setAxis)
        {
            Vector3 newVal = inVal;
            switch (axis)
            {
                case Axes.x: newVal.x = targetVal; break;
                case Axes.y: newVal.y = targetVal; break;
                case Axes.z: newVal.z = targetVal; break;
            }
            setAxis(newVal);
        }
        static void AddAxis(Axes axis, float increment, Action<Vector3> setAxis)
        {
            Vector3 newVal = Vector3.zero;
            switch (axis)
            {
                case Axes.x: newVal.x = increment; break;
                case Axes.y: newVal.y = increment; break;
                case Axes.z: newVal.z = increment; break;
            }
            setAxis(newVal);
        }

        public static void SetPosAxis(Axes axis, Transform target, float targetVal) => SetAxis(axis, target.position, targetVal, (val) => target.transform.position = val);
        public static void SetScaleAxis(Axes axis, Transform target, float targetVal) => SetAxis(axis, target.localScale, targetVal, (val) => target.transform.localScale = val);
        public static void SetRotAxis(Axes axis, Transform target, float targetVal) => SetAxis(axis, target.eulerAngles, targetVal, (val) => target.transform.eulerAngles = val);

        public static void AddPosAxis(Axes axis, Transform target, float increment) => AddAxis(axis, increment, (val) => target.transform.position += val);
        public static void AddScaleAxis(Axes axis, Transform target, float increment) => AddAxis(axis, increment, (val) => target.transform.localScale += val);
        public static void AddRotAxis(Axes axis, Transform target, float increment) => AddAxis(axis, increment, (val) => target.transform.eulerAngles += val);


        public static T AddGeneric<T>(T a, T b)
        {
            if (typeof(T) == typeof(float))
            {
                return (T)(object)((float)(object)a + (float)(object)b);
            }
            else if (typeof(T) == typeof(Vector3))
            {
                return (T)(object)((Vector3)(object)a + (Vector3)(object)b);
            }
            else if (typeof(T) == typeof(Color))
            {
                return (T)(object)((Color)(object)a + (Color)(object)b);
            }
            else
            {
                throw new System.NotSupportedException("Type not supported for addition");
            }
        }

        public static T SubtractGeneric<T>(T a, T b)
        {
            if (typeof(T) == typeof(float))
            {
                return (T)(object)((float)(object)a - (float)(object)b);
            }
            else if (typeof(T) == typeof(Vector3))
            {
                return (T)(object)((Vector3)(object)a - (Vector3)(object)b);
            }
            else if (typeof(T) == typeof(Color))
            {
                return (T)(object)((Color)(object)a - (Color)(object)b);
            }
            else
            {
                throw new System.NotSupportedException("Type not supported for subtraction");
            }
        }

        public static T LerpUnclampedGeneric<T>(T from, T to, float lerpProgress)
        {
            if (typeof(T) == typeof(float))
            {
                return (T)(object)Mathf.LerpUnclamped((float)(object)from, (float)(object)to, lerpProgress);
            }
            else if (typeof(T) == typeof(Vector3))
            {
                return (T)(object)Vector3.LerpUnclamped((Vector3)(object)from, (Vector3)(object)to, lerpProgress);
            }
            else if (typeof(T) == typeof(Color))
            {
                return (T)(object)Color.LerpUnclamped((Color)(object)from, (Color)(object)to, lerpProgress);
            }
            else
            {
                throw new System.NotSupportedException("Type not supported for lerping");
            }
        }
    }
}
