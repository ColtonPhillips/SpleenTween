using System;
using UnityEngine;

namespace SpleenTween
{
    public class TweenInstance : MonoBehaviour
    {
        Action<float> onUpdate;
        public Action onComplete;

        float from;
        float to;

        float currentTime;
        float duration;

        float currentValue;

        public void InjectConstructor(float from, float to, float duration, Action<float> onUpdate, Action onComplete)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            this.onUpdate = onUpdate;
            this.onComplete = onComplete;
        }

        void Update()
        {
            currentTime += Time.deltaTime;

            float lerpValue = currentTime / duration;
            lerpValue = Mathf.Clamp01(lerpValue);

            currentValue = Mathf.Lerp(from, to, lerpValue);

            onUpdate?.Invoke(currentValue);

            if (currentTime >= duration)
            {
                onComplete?.Invoke();
                Destroy(this);
            }
        }
    }
}

