using System;
using System.Collections;
using UnityEngine;

class Hormone : MonoBehaviour
{
    public HormoneData data;

    private SpriteRenderer spriteRenderer;
    private Vector3 heart;
    private Vector3 startPoint;

    private void Start()
    {
        startPoint = transform.position;
        heart = Heartbeat.Instance.transform.position - new Vector3(0, 0, 1);
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = data.color;
    }

    public IEnumerator Run(Action<Hormone, float> onAppear, Action<Hormone> onSet, Action<float> onDisappear, Action onRelease)
    {
        onAppear?.Invoke(this, data.moveTime);

        while (transform.position != heart)
        {
            transform.position = Vector3.MoveTowards(transform.position,
                heart,
                Vector3.Distance(heart, startPoint) / data.moveTime * Time.fixedDeltaTime);
            yield return new WaitForFixedUpdate();
        }

        onSet?.Invoke(this);

        yield return new WaitForSeconds(data.duration - data.disappearTime);

        onDisappear?.Invoke(data.disappearTime);

        float remains = data.disappearTime;
        while (remains > 0)
        {
            GetComponent<SpriteRenderer>().color = new Color(
                data.color.r, data.color.g, data.color.b,
                ((float)Math.Sin(remains * Math.PI * 5 / data.disappearTime - Math.PI / 2) + 1.2f) / 2);
            yield return new WaitForFixedUpdate();
            remains -= Time.fixedDeltaTime;
        }

        onRelease?.Invoke();

        Destroy(gameObject);
    }
}

