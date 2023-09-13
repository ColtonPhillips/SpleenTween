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
        Spleen.Float(gameObject, from, to, duration, (val) => currentValue = val, Ease.Linear).OnComplete(() => print("COMPLETED"));
        Spleen.Position(gameObject, transform.position, new Vector3(2, 2, 0), duration, Ease.OutSine);
    }
}
