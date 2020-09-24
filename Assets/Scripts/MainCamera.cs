using UnityEngine;

public class MainCamera : MonoBehaviour
{
    public static GameObject Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = gameObject;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
