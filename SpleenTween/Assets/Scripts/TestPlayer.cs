using SpleenTween;
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
        if (Input.GetKeyDown(KeyCode.S)) SceneManager.LoadScene("SwitchingTestScene");
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < iterations; i++)
            {
                Spleen.StopTween(tween);
                tween = Spleen.ScaleX(gameObject, 2, 4, 3f, Ease.OutElastic).Loop(loop).Delay(.5f).OnStart(() => print("asd"));
            }
        }
    }
}
