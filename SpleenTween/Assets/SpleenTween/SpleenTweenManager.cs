using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpleenTween
{
    public class SpleenTweenManager : MonoBehaviour
    {
        public int activeTweensCount;
        readonly List<ITween> activeTweens = new();

        public static SpleenTweenManager Instance;

        [RuntimeInitializeOnLoadMethod]
        static void Initialize()
        {
            new GameObject("SpleenTweenManager").AddComponent<SpleenTweenManager>();
        }

        private void Awake()
        {
            if(Instance != null && Instance != this)
                Destroy(gameObject);
            else
                Instance = this;

            DontDestroyOnLoad(this.gameObject);
        }

        private void Update()
        {
            RunTweens();
            activeTweensCount = activeTweens.Count;
        }

        void RunTweens()
        {
            for (int i = 0; i < activeTweens.Count; i++)
            {
                ITween tween = activeTweens[i];
                bool running = tween.Run();
                if (!running)
                {
                    activeTweens.RemoveAt(i);
                }
            }
        }

        public static void StartTween(ITween tween)
        {
            Instance.activeTweens.Add(tween);
        }

        public static void StopTween(ITween tween)
        {
            Instance.activeTweens.Remove(tween);
        }

        public static void StopAll(Scene a, Scene b)
        {
            Instance.activeTweens.Clear();
        }

        private void OnEnable() => SceneManager.activeSceneChanged += StopAll;
        private void OnDisable() => SceneManager.activeSceneChanged -= StopAll;
    }

}
