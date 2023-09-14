using UnityEngine;
using Spleen;
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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) SceneManager.LoadScene("SwitchingTestScene");
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.T))
        {
            transform.localScale = Vector3.zero;
            SpleenTween.ScaleX(transform, 0, 3, 2, Ease.OutBounce).OnLerpValue(.25f, () => SpleenTween.ScaleY(transform, transform.localScale.y, 2f, 2f, Ease.OutSine));

            SpleenTween.Value(from, to, duration, Ease.InCubic, (val) => currentValue = val);

            //SpleenTween.PositionX(transform, 0, 5, 2, easing);
        }
    }
}
