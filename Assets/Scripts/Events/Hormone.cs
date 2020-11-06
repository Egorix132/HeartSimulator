using SplineMesh;
using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(ParticleSystem))]
[RequireComponent(typeof(Spline))]
class Hormone : MonoBehaviour
{
    public HormoneData data;

    private SpriteRenderer spriteRenderer;
    private ParticleSystem particleSystem;
    private SplineFollower splineFollower;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        particleSystem = GetComponent<ParticleSystem>();
        splineFollower = GetComponent<SplineFollower>();
    }

    private void Start()
    {
        spriteRenderer.color = data.color;
        particleSystem.startColor = data.color;
    }

    public IEnumerator Run(
        Spline spline,
        Action<Hormone, float> onAppear,
        Action<Hormone> onSet,
        Action<float> onDisappear,
        Action onRelease)
    {
        onAppear?.Invoke(this, data.moveTime);

        bool isReached = false;
        splineFollower.Init(spline, data.moveTime, () => isReached = true);

        yield return new WaitUntil(() => isReached);

        onSet?.Invoke(this);

        yield return new WaitForSeconds(data.duration - data.disappearTime);

        onDisappear?.Invoke(data.disappearTime);

        float remains = data.disappearTime;
        while (remains > 0)
        {
            spriteRenderer.color = new Color(
                data.color.r, data.color.g, data.color.b,
                ((float)Math.Sin(remains * Math.PI * 5 / data.disappearTime - Math.PI / 2) + 1.2f) / 2);
            particleSystem.startColor = new Color(
                data.color.r, data.color.g, data.color.b,
                ((float)Math.Sin(remains * Math.PI * 5 / data.disappearTime - Math.PI / 2) + 1.2f) / 2);
            yield return new WaitForFixedUpdate();
            remains -= Time.fixedDeltaTime;
        }

        onRelease?.Invoke();

        Destroy(gameObject);
    }
}

