using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform StartButton;

    public float handicapTime;

    private float lastEndTime = 0;

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

    public void StartGame()
    {
        if (Time.realtimeSinceStartup - lastEndTime < 1)
        {
            return;
        }

        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("GameScene"))
        {
            SceneManager.LoadScene("GameScene");
        }
    }

    public void EndGame()
    {
        lastEndTime = Time.realtimeSinceStartup;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameScene"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}
