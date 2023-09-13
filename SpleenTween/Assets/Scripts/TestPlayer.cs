using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SpleenTween;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] float from;
    [SerializeField] float to;
    [SerializeField] float duration;

    [SerializeField] float currentValue;

    private void Start()
    {
        Spleen.Tween(gameObject, from, to, duration, (val) => currentValue = val).OnComplete(() => print("COMPLETED"));
    }
}
