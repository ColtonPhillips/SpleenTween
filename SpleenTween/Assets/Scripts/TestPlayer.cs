using Spleen;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] float from;
    [SerializeField] float to;
    [SerializeField] float duration;

    [SerializeField] int iterations;

    [SerializeField] float currentValue;

    [SerializeField] Transform targetTest;

    [SerializeField] Ease easing;
    [SerializeField] LoopType loop;

    Tween tween;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) SpleenTween.StopTweens(gameObject);
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpleenTween.RelativePosX(gameObject, 3, duration, easing)
                .OnComplete(() => SpleenTween.RelativePosX(gameObject, gameObject.transform.position.x + 2f, 1f, Ease.Linear)
                .OnComplete(() => SpleenTween.RelativePosX(gameObject, gameObject.transform.position.x - 5f, 2f, Ease.InQuint)
                .OnComplete(() => SpleenTween.RelativePos(gameObject, gameObject.transform.position - Vector3.one * 2, 5f, Ease.InOutElastic))));
        }
    }
}
