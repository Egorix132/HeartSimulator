using System.Collections;
using UnityEngine;

public class Panic : MonoBehaviour
{
    public static Panic Instance { get; private set; }

    private System.Random rand;
    private Vector3 startPos;

    private bool isActive;
    private Coroutine coroutine;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        rand = new System.Random();
        startPos = transform.position;
        GameManager.OnFailGame += StopPanic;
    }

    public void Activate(float duration, float rate)
    {
        coroutine = StartCoroutine(RunPanic(duration, rate));
    }

    private IEnumerator RunPanic(float duration, float rate)
    {
        isActive = true;
        float timer = duration;
        while (timer > 0)
        {
            transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
            transform.position = new Vector3(
                startPos.x + (float)rand.NextDouble() - 0.5f,
                startPos.y + ((float)rand.NextDouble() - 0.5f) * 2,
                transform.position.z);
            timer -= rate;
            yield return new WaitForSeconds(rate);
        }
        isActive = false;
        transform.position = startPos;
    }

    private void StopPanic()
    {
        if (coroutine != null) {
            StopCoroutine(coroutine);
        }
        
        isActive = false;
        transform.position = startPos;
    }
}
