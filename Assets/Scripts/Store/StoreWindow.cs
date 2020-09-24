using UnityEngine;

public class StoreWindow : MonoBehaviour
{
    public static StoreWindow Instance { get; private set; }

    [SerializeField] private GameObject store;

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

    public void Open()
    {
        store.SetActive(true);
    }

    public void Close()
    {
        store.SetActive(false);
    }
}
