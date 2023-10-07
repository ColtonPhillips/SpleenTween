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

    [SerializeField] AudioSource source;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            Spleen.Pos(transform, Vector3.zero, Vector3.one, 1f, Ease.Linear).SetLoop(Loop.Yoyo, -1);
        }

        if (Input.GetKeyDown(KeyCode.S))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
