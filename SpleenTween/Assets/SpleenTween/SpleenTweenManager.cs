using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SpleenTween
{
    public class SpleenTweenManager : MonoBehaviour
    {
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
        }

        void RunTweens()
        {
            for (int i = 0; i < activeTweens.Count; i++)
            {
                ITween tween = activeTweens[i];
                tween.Run(out bool done);
                if (done)
                {
                    activeTweens.RemoveAt(i);
                }
            }
        }

        public static void StartTween(ITween tween)
        {
            Instance.activeTweens.Add(tween);
        }

        //private void OnEnable() => SceneManager.activeSceneChanged += StopAllTweens;
        //private void OnDisable() => SceneManager.activeSceneChanged -= StopAllTweens;
    }

}
