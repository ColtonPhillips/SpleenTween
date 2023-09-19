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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) SceneManager.LoadScene("SwitchingTestScene");
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.T))
        {
            for (int i = 0; i < iterations; i++)
            {
                Spleen.PositionX(gameObject, transform.position.x, transform.position.x + 1, 2f, Ease.Linear).Loop(LoopType.Incremental)
                    .OnComplete(() => Spleen.PositionY(gameObject, transform.position.y, transform.position.y + 1.5f, 1f, Ease.Linear));
                
            }
        }
    }
}
