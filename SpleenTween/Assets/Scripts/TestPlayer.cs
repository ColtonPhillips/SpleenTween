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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) SceneManager.LoadScene("SwitchingTestScene");
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.T))
        {
            Spleen
                .PositionX(gameObject, transform.position.x, 2f, 1f, Ease.OutElastic).Loop()
                .OnComplete(() => Spleen.PositionY(gameObject, transform.position.y, 2f, 1f, Ease.OutElastic)
                .OnComplete(() => Spleen.PositionX(gameObject, transform.position.x, -2f, 1f, Ease.OutElastic)
                .OnComplete(() => Spleen.PositionY(gameObject, transform.position.y, -2f, 1f, Ease.OutElastic))));
                

            //SpleenTween.Scale(transform, Vector3.zero, Vector3.one * 5, .5f, easing);

            //SpleenTween.Value(from, to, duration, Ease.InCubic, (val) => currentValue = val);
            //SpleenTween.Value3(Vector2.zero, Vector2.one * 5, 5f, Ease.InOutElastic, (val) => transform.position = val);

        }
    }
    float target = 5f;
    void BackForth()
    {
        target = -target;
        Tween tween = Spleen.PositionX(gameObject, transform.position.x, target, 1f, easing).Delay(1f).OnComplete(BackForth);
    }
}
