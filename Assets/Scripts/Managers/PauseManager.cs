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
            return;
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

    public void ShowModal(Vector3 pos, string text, bool closable = true)
    {
        Time.timeScale = 0;
        IsPause = true;
        modal.SetModal(pos, text, closable);
        modal.gameObject.SetActive(true);
    }

    public void CloseModal()
    {
        Time.timeScale = 1;
        IsPause = false;
        modal.gameObject.SetActive(false);
        modal.GetComponent<BoxCollider2D>().enabled = false;
    }

    public void QuitGame()
    {
        Time.timeScale = 1;
        GameManager.Instance.QuitGame();
    }
}
