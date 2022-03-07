using UnityEngine;

public class Example : MonoBehaviour
{
    [SerializeField] private AnimationCurve _curve;

    private float _expiredTime;
    private float _duration = 1;

    private void Update()
    {
        _expiredTime += Time.deltaTime;

        if (_expiredTime > _duration)
            _expiredTime = 0;

        float progress = _expiredTime / _duration;

        transform.position = new Vector3(transform.position.x, _curve.Evaluate(progress), transform.position.z);
    }
}