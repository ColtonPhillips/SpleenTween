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
        if (Input.GetKeyDown(KeyCode.S)) Spleen.StopAll();
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < iterations; i++)
            {
                Spleen.RotZ(gameObject, transform.rotation.z, transform.rotation.z + 360, 3f, Ease.OutElastic).Loop(loop)
                    .OnComplete(() => Spleen.Pos(gameObject, transform.position, Vector3.one * 3, 2f, Ease.Linear)
                    .OnComplete(() => Spleen.Pos(gameObject, transform.position, Vector3.one * -3, 4f, Ease.Linear)));
            }
        }
    }
}
