using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpleenTween
{
    public class Spleen : MonoBehaviour
    {
        public List<FloatTween> _activeTweensFloat = new();
        public static Spleen Instance;
        private void Awake()
        {
            if (Instance != null && Instance != this)
                Destroy(this);
            else
                Instance = this;
        }

        private void OnEnable() => SceneManager.activeSceneChanged += StopAllTweens;
        private void OnDisable() => SceneManager.activeSceneChanged -= StopAllTweens;

        //reset all lists when scene loads to prevent tweens continuing after scene load
        void StopAllTweens(Scene scene1, Scene scene2)
        {
            _activeTweensFloat.Clear();
        }

        //create a new instance when the game starts
        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            Instance = new GameObject("SpleenTweenManager").AddComponent<Spleen>();
            DontDestroyOnLoad(Instance);
        }

        private void Update()
        {
            for (int i = _activeTweensFloat.Count - 1; i >= 0; i--)
            {
                if (!_activeTweensFloat[i].Tweening())
                {
                    _activeTweensFloat.RemoveAt(i);
                }
            }
        }

        Tween Float(float from, float to, float duration, Ease easing, Action<float> onUpdate)
        {
            FloatTween tween = new(from, to, duration, easing, onUpdate);
            _activeTweensFloat.Add(tween);
            return tween;
        }
        public static void TweenFloat(float from, float to, float duration, Ease easing, Action<float> updateCallback)
        {
            Instance.Float(from, to, duration, easing, updateCallback);
        }
    }
}

