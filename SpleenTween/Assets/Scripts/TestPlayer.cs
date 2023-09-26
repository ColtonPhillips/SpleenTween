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

    [SerializeField] AudioSource source;

    Tween tween;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)) Spleen.StopTweens(gameObject);
        if (Input.GetKeyDown(KeyCode.R)) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        if (Input.GetKeyDown(KeyCode.T))
        {
            //Spleen.PosY(transform.gameObject, UnityEngine.Random.Range(0, 10f), UnityEngine.Random.Range(0f, 10f), (Ease)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Ease)).Length)).Loop(LoopType.Yoyo);
            //Spleen.PosX(transform.gameObject, UnityEngine.Random.Range(0, 10f), UnityEngine.Random.Range(0f, 10f), (Ease)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Ease)).Length)).Loop(LoopType.Yoyo);
            //Spleen.RotZ(transform.gameObject, UnityEngine.Random.Range(0, 10f), UnityEngine.Random.Range(0f, 10f), (Ease)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Ease)).Length)).Loop(LoopType.Yoyo);

            //Spleen.AddLocRotZ(gameObject, UnityEngine.Random.Range(0f, 360f), UnityEngine.Random.Range(.25f, 1.25f), (Ease)UnityEngine.Random.Range(0, Enum.GetNames(typeof(Ease)).Length)).Loop(LoopType.Yoyo);

            //Spleen.AddVol(source, .1f, 10, Ease.OutCubic);

            Spleen.AddPosX(gameObject, 2, 2f, Ease.OutCubic).Loop(LoopType.Incremental);//.Chain(Spleen.AddPosY(gameObject, 2, 2, 0)).Chain(Spleen.AddPosY(gameObject, -2, 3f, 0));
        }
    }
}
