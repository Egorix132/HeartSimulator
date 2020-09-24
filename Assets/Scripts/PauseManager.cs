using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance { get; private set; }

    [SerializeField] private GameObject menu;
    [SerializeField] private ModalManager modal;

    public bool IsPause { get; private set; }


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

    public void ContinueGame()
    {
        Time.timeScale = 1;
        IsPause = false;
        menu.SetActive(false);
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        IsPause = true;
        menu.SetActive(true);
    }

    public void ShowModal(Vector3 pos, string text)
    {
        Time.timeScale = 0;
        IsPause = false;
        modal.SetModal(pos, text);
        modal.gameObject.SetActive(false);
    }

    public void CloseModal()
    {
        Time.timeScale = 1;
        IsPause = true;
        modal.gameObject.SetActive(false);
    }
}
