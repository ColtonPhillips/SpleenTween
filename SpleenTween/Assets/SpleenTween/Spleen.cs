using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

namespace SpleenTween
{
    public class Spleen : MonoBehaviour
    {
        //active tweens
        readonly List<FloatTween> _floatTweens = new();
        readonly List<PositionTween> _positionTweens = new();

        public static Spleen Instance;
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
            _floatTweens.Clear();
            _positionTweens.Clear();
        }

        //create a new instance when the game starts
        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            Instance = new GameObject("SpleenTweenManager").AddComponent<Spleen>();
        }

        private void Update()
        {
            foreach(FloatTween tween in _floatTweens.Reverse<FloatTween>())
            {
                if (!tween.Tweening())
                    _floatTweens.Remove(tween);
            }

            foreach (PositionTween tween in _positionTweens.Reverse<PositionTween>())
            {
                if (tween._target == null)
                {
                    _positionTweens.Remove(tween);
                    continue;
                }
                if (!tween.Tweening())
                    _positionTweens.Remove(tween);
            }
        }

        Tween AddFloatTween(float from, float to, float duration, Ease easing, Action<float> onUpdate)
        {
            FloatTween tween = new(from, to, duration, easing, onUpdate);
            _floatTweens.Add(tween);
            return tween;
        }
        public static Tween TweenFloat(float from, float to, float duration, Ease easing, Action<float> onUpdate)
        {
            return Instance.AddFloatTween(from, to, duration, easing, onUpdate);
        }

        Tween AddPositionTween(Transform target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            PositionTween tween = new(target, from, to, duration, easing);
            _positionTweens.Add(tween);
            return tween;
        }
        public static Tween TweenPosition(Transform target, Vector3 from, Vector3 to, float duration, Ease easing)
        {
            return Instance.AddPositionTween(target, from, to, duration, easing);
        }
    }
}

