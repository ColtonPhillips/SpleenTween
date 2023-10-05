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
            Spleen.Pos(transform, transform.position + Vector3.one * from, transform.position + Vector3.one * to, 3f).OnComplete(() => print("complete"));
        }
    }
}
