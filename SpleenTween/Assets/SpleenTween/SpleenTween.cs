using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.Collections;

namespace Spleen
{
    public class SpleenTween : MonoBehaviour
    {
        [ReadOnly] public int ActiveTweensCount;

        readonly List<Value> ValueTweens = new();
        readonly List<Position> PositionTweens = new();
        readonly List<PositionX> PositionXTweens = new();
        readonly List<PositionY> PositionYTweens = new();
        readonly List<PositionZ> PositionZTweens = new();

        public static SpleenTween Instance;
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(Instance);
        }

        private void OnEnable() => SceneManager.activeSceneChanged += StopAllTweens;
        private void OnDisable() => SceneManager.activeSceneChanged -= StopAllTweens;

        //reset all lists when scene loads to prevent tweens continuing after scene load
        void StopAllTweens(Scene s1, Scene s2)
        {
            ValueTweens.Clear();
            PositionTweens.Clear();
            PositionXTweens.Clear();
            PositionYTweens.Clear();
            PositionZTweens.Clear();
        }

        //create a new instance when the game starts
        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            Instance = new GameObject("SpleenTweenManager").AddComponent<SpleenTween>();
        }

        private void Update()
        {
            for (int i = ValueTweens.Count - 1; i >= 0; i--)
            {
                Value tween = ValueTweens[i];
                if (!tween.Tweening())
                {
                    ValueTweens.RemoveAt(i);
                }
            }

            RunTransformTweens(PositionTweens);
            RunTransformTweens(PositionXTweens);
            RunTransformTweens(PositionYTweens);
            RunTransformTweens(PositionZTweens);

            ActiveTweensCount = 
                ValueTweens.Count + 
                PositionTweens.Count + 
                PositionXTweens.Count + 
                PositionYTweens.Count +
                PositionZTweens.Count;
        }

        void RunTransformTweens<T>(List<T> tweenList) where T : TweenTransform
        {
            for (int i = tweenList.Count - 1; i >= 0; i--)
            {
                T tween = tweenList[i];
                if (tween._target == null)
                {
                    tweenList.RemoveAt(i);
                }
                else if (!tween.Tweening())
                {
                    tweenList.RemoveAt(i);
                }
            }
        }

        #region Float
        Tween AddValue(float from, float to, float duration, Ease easing, Action<float> onUpdate)
        {
            Value tween = new(from, to, duration, easing, onUpdate);
            ValueTweens.Add(tween);
            return tween;
        }
        public static Tween Value(float from, float to, float duration, Ease easing, Action<float> onUpdate)
        {
            return Instance.AddValue(from, to, duration, easing, onUpdate);
        }
        #endregion

        #region Position
        Tween AddPosition(Transform target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            Position tween = new(target, from, to, duration, easing);
            PositionTweens.Add(tween);
            return tween;
        }
        public static Tween Position(Transform target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            return Instance.AddPosition(target, from, to, duration, easing);
        }

        Tween AddPositionX(Transform target, float from, float to, float duration, Ease easing)
        {
            PositionX tween = new(target, from, to, duration, easing);
            PositionXTweens.Add(tween);
            return tween;
        }
        public static Tween PositionX(Transform target, float from, float to, float duration, Ease easing)
        {
            return Instance.AddPositionX(target, from, to, duration, easing);
        }
        
        Tween AddPositionY(Transform target, float from, float to, float duration, Ease easing)
        {
            PositionY tween = new(target, from, to, duration, easing);
            PositionYTweens.Add(tween);
            return tween;
        }
        public static Tween PositionY(Transform target, float from, float to, float duration, Ease easing)
        {
            return Instance.AddPositionY(target, from, to, duration, easing);
        }

        Tween AddPositionZ(Transform target, float from, float to, float duration, Ease easing)
        {
            PositionZ tween = new(target, from, to, duration, easing);
            PositionZTweens.Add(tween);
            return tween;
        }
        public static Tween PositionZ(Transform target, float from, float to, float duration, Ease easing)
        {
            return Instance.AddPositionZ(target, from, to, duration, easing);
        }

        #endregion
    }
}

