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
                Spleen.RotZ(gameObject, transform.rotation.z, transform.rotation.z + 360, 3f, Ease.OutElastic).Loop(LoopType.Incremental);
                Spleen.ScaleX(gameObject, transform.localScale.x, transform.localScale.x + 2, 1.5f, Ease.InExpo).Loop(LoopType.Yoyo);
                Spleen.ScaleY(gameObject, transform.localScale.x, transform.localScale.y - 0.5f, 1.5f, Ease.OutBack).Loop(LoopType.Rewind);
                Spleen.PosY(gameObject, transform.position.y, transform.position.y - 3, 1.5f, Ease.OutBounce).Loop(LoopType.Rewind);
                SpriteRenderer sr = GetComponent<SpriteRenderer>();
                Spleen.Value(0, 1, 3f, Ease.InSine, (val) => sr.color = new Color(0, val, val, 1)).Loop(LoopType.Rewind);
            }
        }
    }
}
