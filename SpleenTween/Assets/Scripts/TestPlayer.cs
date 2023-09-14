using UnityEngine;
using SpleenTween;
using UnityEngine.SceneManagement;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] float from;
    [SerializeField] float to;
    [SerializeField] float duration;

    [SerializeField] float currentValue;

    [SerializeField] Transform targetTest;

    [SerializeField] Ease easing;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) SceneManager.LoadScene("SwitchingTestScene");
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.T))
        {
            for(int i = 0; i < 1; i++)
                Spleen.TweenPosition(transform, Vector3.one * from, Vector3.one * to, duration, Ease.InBounce).OnComplete(() =>
                {
                    Spleen.TweenPosition(targetTest, Vector3.one * from, -Vector3.one * to, duration, Ease.InElastic);
                });
        }
    }
}
