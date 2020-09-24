using System;
using System.Collections;
using UnityEngine;

public class GameEvent : MonoBehaviour
{
    public GameEventData data;

    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public IEnumerator Run() 
    {
        yield return new WaitForFixedUpdate();

        float remains = data.disappearTime;
        while (remains > 0)
        {
            if(spriteRenderer != null)
            {
                spriteRenderer.color = new Color(
                spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b,
                ((float)Math.Sin(remains * Math.PI * 5 / data.disappearTime - Math.PI / 2) + 1.2f) / 2);
            }
             
            yield return new WaitForFixedUpdate();
            remains -= Time.fixedDeltaTime;
        }

        Destroy(gameObject);
    }

    private void OnMouseUp()
    {
        data.RunEvent();
        gameObject.SetActive(false);
    }
}
