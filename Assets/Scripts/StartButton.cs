using UnityEngine;

public class StartButton : MonoBehaviour
{
    private Vector3 startPos;
    private Vector3 startScale;
    private float dragTime = 0;
    private bool pressed = false;

    private void Start()
    {
        startPos = transform.position;
        startScale = transform.localScale;
    }

    private void Update()
    {
        transform.position = startPos + new Vector3(
            5 / (dragTime + 2) - 2.5f,
            -2 / (dragTime + 2) + 1,
            10 / (dragTime + 2f) - 5);
        float scaleOffset = -30 / (dragTime + 1.5f) + 20;
        transform.localScale 
            = startScale + new Vector3(scaleOffset, scaleOffset, 0);
        if (!pressed && dragTime > 0)
        {
            dragTime -= Mathf.Min(dragTime, Time.deltaTime * 5);
        }
        pressed = false;
    }

    private void OnMouseDrag()
    {
        pressed = true;
        dragTime += Time.deltaTime;
    }
}
