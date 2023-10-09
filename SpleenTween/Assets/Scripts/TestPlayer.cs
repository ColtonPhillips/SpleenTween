using SpleenTween;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] float from;
    [SerializeField] float to;
    [SerializeField] float duration;
    [SerializeField] Ease ease;
    [SerializeField] Loop loop;
    [SerializeField] int loopCount = -1;

    [SerializeField] int iterations;

    [SerializeField] float currentValue;

    [SerializeField] Transform targetTest;

    [SerializeField] AudioSource source;

    ITween tween;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            SpleenTweenManager.StopTween(tween);
            tween = Spleen.AddPos(transform, Vector3.one * to, duration, ease).SetLoop(loop, loopCount);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
