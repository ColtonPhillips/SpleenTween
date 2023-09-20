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
            SpleenTween.RelativePosX(gameObject, 5, 2f, Ease.Linear);
            SpleenTween.RelativePosY(gameObject, -2f, 3f, Ease.Linear);
            SpleenTween.RelativePos(gameObject, new Vector3(-5, 2f), 5f, Ease.InElastic);



            SpleenTween.RelativePos(gameObject, transform.position + (Vector3.right * 3), 3f, Ease.Linear);
            SpleenTween.RelativePos(gameObject, transform.position + (Vector3.down * 2), 5f, Ease.Linear);
            SpleenTween.RelativePosX(gameObject, -10, 2f, Ease.Linear);
            SpleenTween.RelativeRotZ(gameObject, 360, 10f, Ease.InExpo);
            SpleenTween.RelativeRotY(gameObject, 360, 5f, Ease.OutExpo).Loop(LoopType.Yoyo);
            SpleenTween.ScaleY(gameObject, 2f, 2f, Ease.InElastic);
            SpleenTween.ScaleX(gameObject, 0.5f, 4f, Ease.InBounce);

            SpleenTween.RelativePos(gameObject, transform.position + (Vector3.right * 3), 3f, Ease.Linear);
            SpleenTween.RelativePos(gameObject, transform.position + (Vector3.down * 2), 5f, Ease.Linear);
            SpleenTween.RelativePosX(gameObject, -10, 3f, Ease.Linear);
            SpleenTween.RelativeRotZ(gameObject, -459, 7f, Ease.OutExpo);
            SpleenTween.RelativeRotY(gameObject, -30, 2f, Ease.InOutElastic).Loop(LoopType.Yoyo);
            SpleenTween.ScaleY(gameObject, -20f, 1f, Ease.InElastic);
            SpleenTween.ScaleX(gameObject, 10f, 5f, Ease.InBounce);
            SpleenTween.RelativePosX(gameObject, 5, 2f, Ease.Linear);
            SpleenTween.RelativePosY(gameObject, -2f, 3f, Ease.Linear);
            SpleenTween.RelativePos(gameObject, new Vector3(-5, 2f), 5f, Ease.InElastic);



            SpleenTween.RelativePos(gameObject, transform.position + (Vector3.right * 3), 3f, Ease.Linear);
            SpleenTween.RelativePos(gameObject, transform.position + (Vector3.down * 2), 5f, Ease.Linear);
            SpleenTween.RelativePosX(gameObject, -10, 2f, Ease.Linear);
            SpleenTween.RelativeRotZ(gameObject, 360, 10f, Ease.InExpo);
            SpleenTween.RelativeRotY(gameObject, 360, 5f, Ease.OutExpo).Loop(LoopType.Yoyo);
            SpleenTween.ScaleY(gameObject, 2f, 2f, Ease.InElastic);
            SpleenTween.ScaleX(gameObject, 0.5f, 4f, Ease.InBounce);

            SpleenTween.RelativePos(gameObject, transform.position + (Vector3.right * 3), 3f, Ease.Linear);
            SpleenTween.RelativePos(gameObject, transform.position + (Vector3.down * 2), 5f, Ease.Linear);
            SpleenTween.RelativePosX(gameObject, -10, 3f, Ease.Linear);
            SpleenTween.RelativeRotZ(gameObject, -459, 7f, Ease.OutExpo);
            SpleenTween.RelativeRotY(gameObject, -30, 2f, Ease.InOutElastic).Loop(LoopType.Yoyo);
            SpleenTween.ScaleY(gameObject, -20f, 1f, Ease.InElastic);
            SpleenTween.ScaleX(gameObject, 10f, 5f, Ease.InBounce);
            SpleenTween.RelativePosX(gameObject, 5, 2f, Ease.Linear);
            SpleenTween.RelativePosY(gameObject, -2f, 3f, Ease.Linear);
            SpleenTween.RelativePos(gameObject, new Vector3(-5, 2f), 5f, Ease.InElastic);



            SpleenTween.RelativePos(gameObject, transform.position + (Vector3.right * 3), 3f, Ease.Linear);
            SpleenTween.RelativePos(gameObject, transform.position + (Vector3.down * 2), 5f, Ease.Linear);
            SpleenTween.RelativePosX(gameObject, -10, 2f, Ease.Linear);
            SpleenTween.RelativeRotZ(gameObject, 360, 10f, Ease.InExpo);
            SpleenTween.RelativeRotY(gameObject, 360, 5f, Ease.OutExpo).Loop(LoopType.Yoyo);
            SpleenTween.ScaleY(gameObject, 2f, 2f, Ease.InElastic);
            SpleenTween.ScaleX(gameObject, 0.5f, 4f, Ease.InBounce);

            SpleenTween.RelativePos(gameObject, transform.position + (Vector3.right * 3), 3f, Ease.Linear);
            SpleenTween.RelativePos(gameObject, transform.position + (Vector3.down * 2), 5f, Ease.Linear);
            SpleenTween.RelativePosX(gameObject, -10, 3f, Ease.Linear);
            SpleenTween.RelativeRotZ(gameObject, -459, 7f, Ease.OutExpo);
            SpleenTween.RelativeRotY(gameObject, -30, 2f, Ease.InOutElastic).Loop(LoopType.Yoyo);
            SpleenTween.ScaleY(gameObject, -20f, 1f, Ease.InElastic);
            SpleenTween.ScaleX(gameObject, 10f, 5f, Ease.InBounce);
        }
    }
}
