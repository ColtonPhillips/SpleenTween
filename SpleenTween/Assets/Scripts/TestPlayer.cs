using SpleenTween;
using System;
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
        if (Input.GetKeyDown(KeyCode.S)) Spleen.StopTweens(gameObject);
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.T))
        {
            Spleen.PosY(gameObject, UnityEngine.Random.Range(0, 10f), UnityEngine.Random.Range(0f, 10f), (Ease)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Ease)).Length)).Loop(LoopType.Yoyo);
            Spleen.PosX(gameObject, UnityEngine.Random.Range(0, 10f), UnityEngine.Random.Range(0f, 10f), (Ease)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Ease)).Length)).Loop(LoopType.Yoyo);
            Spleen.RotZ(gameObject, UnityEngine.Random.Range(0, 10f), UnityEngine.Random.Range(0f, 10f), (Ease)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Ease)).Length)).Loop(LoopType.Yoyo);
            Spleen.ScaleY(gameObject, UnityEngine.Random.Range(0, 10f), UnityEngine.Random.Range(0f, 10f), (Ease)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Ease)).Length)).Loop(LoopType.Yoyo);
            Spleen.ScaleX(gameObject, UnityEngine.Random.Range(0, 10f), UnityEngine.Random.Range(0f, 10f), (Ease)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Ease)).Length)).Loop(LoopType.Yoyo);
        }
    }
}
