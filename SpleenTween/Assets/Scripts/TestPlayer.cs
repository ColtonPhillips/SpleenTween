using UnityEngine;
using SpleenTween;

public class TestPlayer : MonoBehaviour
{
    [SerializeField] float from;
    [SerializeField] float to;
    [SerializeField] float duration;

    [SerializeField] float currentValue;

    [SerializeField] Ease easing;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            //Spleen.Float(gameObject, from, to, duration, (val) => currentValue = val, easing);
            //Spleen.Position(gameObject, transform.position, Vector3.right * 5, 2f, easing);
            //Spleen.PositionX(gameObject, from, to, duration, easing);
            //Spleen.PositionY(gameObject, from, to, duration, easing);
            //Spleen.PositionZ(gameObject, from, to, duration, easing);
            //Spleen.Scale(gameObject, Vector3.one * from, Vector3.one * to, duration, easing);
            //Spleen.ScaleX(gameObject, from, to, duration, easing);
        }
    }
}
