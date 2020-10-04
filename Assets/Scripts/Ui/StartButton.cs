using UnityEngine;

public class StartButton : MonoBehaviour
{
    public static StartButton Instance { get; private set; }

    private Vector3 startPos;
    private Vector3 startScale;
    private float dragTime = 0;
    private bool pressed = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        startPos = transform.localPosition;
        startScale = transform.localScale;
    }

    private void Update()
    {
        transform.localPosition = startPos + new Vector3(
            (2 / (dragTime + 2) - 1f) * 1500,
            (-0.8f / (dragTime + 2) + 0.4f) * 1000,
            10 / (dragTime + 2f) - 5);
        float scaleOffset = (-21 / (dragTime + 1.5f) + 14) * 1000;
        transform.localScale 
            = startScale + new Vector3(scaleOffset, scaleOffset, 0);

        if (!pressed && dragTime > 0)
        {
            dragTime -= Mathf.Min(dragTime, Time.deltaTime * 5);
        }
        pressed = false;
    }

    public void Drag()
    {
        pressed = true;
        dragTime += Time.deltaTime;
    }
}
