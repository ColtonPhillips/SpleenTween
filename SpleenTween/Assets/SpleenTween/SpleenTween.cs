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

            ActiveTweensCount = Tweens.Count;
        }

        #region TweenRunners
        void RunTweens()
        {
            for (int i = Tweens.Count - 1; i >= 0; i--)
            {
                Tween tween = Tweens[i];
                if(tween.GetType().IsSubclassOf(typeof(TweenTransform)))
                {
                    TweenTransform tweenTransform = (TweenTransform)tween;
                    if (tweenTransform._target == null)
                    {
                        Tweens.RemoveAt(i);
                        continue;
                    }
                }

                if (!tween.Tweening())
                {
                    Tweens.RemoveAt(i);
                }
            }
        }

        public static void StopTween(Tween tween)
        {
            Instance.Tweens.Remove(tween);
        }

        #endregion

        #region TweenFunctions

        #region Value
        public static Tween Value(float from, float to, float duration, Ease easing, Action<float> onUpdate)
        {
            Value tween = new(from, to, duration, easing, onUpdate);
            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween Value3(Vector3 from, Vector3 to, float duration, Ease easing, Action<Vector3> onUpdate)
        {
            Value3 tween = new(from, to, duration, easing, onUpdate);
            Instance.Tweens.Add(tween);
            return tween;
        }
        #endregion

        #region Position
        public static Tween Position(Transform target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            Position tween = new(target, from, to, duration, easing);
            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween PositionX(Transform target, float from, float to, float duration, Ease easing)
        {
            PositionX tween = new(target, from, to, duration, easing);
            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween PositionY(Transform target, float from, float to, float duration, Ease easing)
        {
            PositionY tween = new(target, from, to, duration, easing);
            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween PositionZ(Transform target, float from, float to, float duration, Ease easing)
        {
            PositionZ tween = new(target, from, to, duration, easing);
            Instance.Tweens.Add(tween);
            return tween;
        }
        #endregion

        #region Scale
        public static Tween Scale(Transform target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            Scale tween = new(target, from, to, duration, easing);
            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween ScaleX(Transform target, float from, float to, float duration, Ease easing)
        {
            ScaleX tween = new(target, from, to, duration, easing);
            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween ScaleY(Transform target, float from, float to, float duration, Ease easing)
        {
            ScaleY tween = new(target, from, to, duration, easing);
            Instance.Tweens.Add(tween);
            return tween;
        }
        public static Tween ScaleZ(Transform target, float from, float to, float duration, Ease easing)
        {
            ScaleZ tween = new(target, from, to, duration, easing);
            Instance.Tweens.Add(tween);
            return tween;
        }

        #endregion

        #endregion
    }
}

