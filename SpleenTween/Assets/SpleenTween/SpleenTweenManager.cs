using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpleenTween
{
    public class SpleenTweenManager : MonoBehaviour
    {
        public int ActiveTweensCount;

        public List<Tween> Tweens = new();

        public static SpleenTweenManager Instance;

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

        // reset all lists when scene loads to prevent tweens carrying over
        void StopAllTweens(Scene s1, Scene s2)
        {
            Tweens.Clear();
        }

        //create a new instance when the game starts
        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            Instance = new GameObject("SpleenTweenManager").AddComponent<SpleenTweenManager>();
        }
        #endregion

        private void Update()
        {
            RunTweens();
            ActiveTweensCount = Tweens.Count;
        }

        void RunTweens()
        {
            for (int i = 0; i < Tweens.Count; i++)
            {
                Tween tween = Tweens[i];
                if (!tween.Tweening())
                {
                    Tweens.RemoveAt(i);
                }
            }
        }

        public static Tween StartTweenAndAddNullCheck(Tween tween, GameObject target)
        {
            tween.StopIfNull(target);
            Instance.Tweens.Add(tween);
            return tween;
        }
    }
}

