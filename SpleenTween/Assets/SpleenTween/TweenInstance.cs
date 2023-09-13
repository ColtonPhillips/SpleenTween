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

        public void InjectConstructor(float from, float to, float duration, Action<float> onUpdate)
        {
            this.from = from;
            this.to = to;
            this.duration = duration;
            this.onUpdate = onUpdate;
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

        
        public TweenInstance OnComplete(Action action)
        {
            this.onComplete = action;
            return this;
        }
    }
}

