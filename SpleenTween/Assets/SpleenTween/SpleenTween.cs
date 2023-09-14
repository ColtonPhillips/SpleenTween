using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using Unity.Collections;
using System.Collections;

namespace Spleen
{
    public class SpleenTween : MonoBehaviour
    {
        [ReadOnly] public int ActiveTweensCount;

        readonly List<Tween> Tweens = new();
        readonly List<TweenTransform> TransformTweens = new();

        public static SpleenTween Instance;

        #region Initialization
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
            Tweens.Clear();
            TransformTweens.Clear();
        }
        //create a new instance when the game starts
        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            Instance = new GameObject("SpleenTweenManager").AddComponent<SpleenTween>();
        }
        #endregion

        private void Update()
        {
            RunTweens();
            RunTransformTweens();

            ActiveTweensCount = Tweens.Count + TransformTweens.Count;
        }

        #region TweenRunners
        void RunTweens()
        {
            for (int i = Tweens.Count - 1; i >= 0; i--)
            {
                Tween tween = Tweens[i];
                if (!tween.Tweening())
                {
                    Tweens.RemoveAt(i);
                }
            }
        }
        void RunTransformTweens()
        {
            for (int i = TransformTweens.Count - 1; i >= 0; i--)
            {
                TweenTransform tween = TransformTweens[i];
                if (tween._target == null)
                {
                    TransformTweens.RemoveAt(i);
                }
                else if (!tween.Tweening())
                {
                    TransformTweens.RemoveAt(i);
                }
            }
        }
        #endregion

        #region TweenFunctions

        #region Value
        Tween AddValue(float from, float to, float duration, Ease easing, Action<float> onUpdate)
        {
            Value tween = new(from, to, duration, easing, onUpdate);
            Tweens.Add(tween);
            return tween;
        }
        #endregion

        #region Position
        Tween AddPosition(Transform target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            Position tween = new(target, from, to, duration, easing);
            TransformTweens.Add(tween);
            return tween;
        }
        Tween AddPositionX(Transform target, float from, float to, float duration, Ease easing)
        {
            PositionX tween = new(target, from, to, duration, easing);
            TransformTweens.Add(tween);
            return tween;
        }
        Tween AddPositionY(Transform target, float from, float to, float duration, Ease easing)
        {
            PositionY tween = new(target, from, to, duration, easing);
            TransformTweens.Add(tween);
            return tween;
        }
        Tween AddPositionZ(Transform target, float from, float to, float duration, Ease easing)
        {
            PositionZ tween = new(target, from, to, duration, easing);
            TransformTweens.Add(tween);
            return tween;
        }
        #endregion

        #region Scale
        Tween AddScale(Transform target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            Scale tween = new(target, from, to, duration, easing);
            TransformTweens.Add(tween);
            return tween;
        }
        Tween AddScaleX(Transform target, float from, float to, float duration, Ease easing)
        {
            ScaleX tween = new(target, from, to, duration, easing);
            TransformTweens.Add(tween);
            return tween;
        }
        Tween AddScaleY(Transform target, float from, float to, float duration, Ease easing)
        {
            ScaleY tween = new(target, from, to, duration, easing);
            TransformTweens.Add(tween);
            return tween;
        }
        Tween AddScaleZ(Transform target, float from, float to, float duration, Ease easing)
        {
            ScaleZ tween = new(target, from, to, duration, easing);
            TransformTweens.Add(tween);
            return tween;
        }

        #endregion

        #endregion

        #region Helpers

        #region Value
        public static Tween Value(float from, float to, float duration, Ease easing, Action<float> onUpdate)
        {
            return Instance.AddValue(from, to, duration, easing, onUpdate);
        }
        #endregion

        #region Position
        public static Tween Position(Transform target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            return Instance.AddPosition(target, from, to, duration, easing);
        }
        public static Tween PositionX(Transform target, float from, float to, float duration, Ease easing)
        {
            return Instance.AddPositionX(target, from, to, duration, easing);
        }
        public static Tween PositionY(Transform target, float from, float to, float duration, Ease easing)
        {
            return Instance.AddPositionY(target, from, to, duration, easing);
        }
        public static Tween PositionZ(Transform target, float from, float to, float duration, Ease easing)
        {
            return Instance.AddPositionZ(target, from, to, duration, easing);
        }
        #endregion

        #region Scale
        public static Tween Scale(Transform target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            return Instance.AddScale(target, from, to, duration, easing);
        }
        public static Tween ScaleX(Transform target, float from, float to, float duration, Ease easing)
        {
            return Instance.AddScaleX(target, from, to, duration, easing);
        }
        public static Tween ScaleY(Transform target, float from, float to, float duration, Ease easing)
        {
            return Instance.AddScaleY(target, from, to, duration, easing);
        }
        public static Tween ScaleZ(Transform target, float from, float to, float duration, Ease easing)
        {
            return Instance.AddScaleZ(target, from, to, duration, easing);
        }
        #endregion

        #endregion
    }
}
