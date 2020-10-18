using UnityEngine;

public class SettingsWindow : MonoBehaviour
{
    public static SettingsWindow Instance { get; private set; }

    [SerializeField] private GameObject menu;

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

    public void Open()
    {
        menu.SetActive(true);
    }

    public void Close()
    {
        menu.SetActive(false);
    }
}
