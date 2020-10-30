using UnityEngine;

public class HeartBlood : MonoBehaviour
{
    public static HeartBlood Instance { get; private set; }

    private bool isUpping;

    private float speed = 0;

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
    }

    public void Fill(bool isUpping, float speed)
    {
        this.isUpping = isUpping;
        this.speed = speed;
    }

    private void Update()
    {
        float newHeight = isUpping
            ? transform.localScale.y + speed
            : transform.localScale.y - speed;

        newHeight = newHeight > 1 ? newHeight : 1;
        newHeight = newHeight < 4 ? newHeight : 4;

        transform.localScale = new Vector3(1, newHeight, 1);
    }
}
