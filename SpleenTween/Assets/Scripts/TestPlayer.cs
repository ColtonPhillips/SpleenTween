using UnityEngine;
using SpleenTween;
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
    [SerializeField] Loop loop;

    Tween tween;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) SceneManager.LoadScene("SwitchingTestScene");
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.T))
        {
            Spleen.StopTween(tween);
            tween = Spleen.Scale(gameObject, Vector3.one * from, Vector3.one * to, duration, easing).OnComplete(() => print("complete")).Delay(1).Loop(loop);
        }
    }
}
