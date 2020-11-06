using SplineMesh;
using System;
using UnityEngine;

class SplineFollower : MonoBehaviour
{
    private Spline spline;
    private float distance;
    private float time;
    private Vector3 offset;
    private Action onEnd;

    public void Init(
        Spline spline,
        float time,
        Action onEnd,
        float startDistance = 0,
        Vector3? offset = null)
    {
        this.spline = spline;
        this.time = time > 0 ? time : 1;
        distance = startDistance;
        this.offset = offset ?? Vector3.zero;
        this.onEnd = onEnd;
    }

    private void Update()
    {
        distance += Time.deltaTime / time * spline.nodes.Count;
        if (distance > spline.nodes.Count - 1)
        {
            onEnd?.Invoke();
            return;
        }
        Move();
    }

    private void Move()
    {
        CurveSample sample = spline.GetSample(distance);
        transform.localPosition = sample.location + offset;
    }
}
