using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public static event Action OnPause;
    public static event Action OnQuit;
    public static event Action OnStartGame;
    public static event Action OnEndGame;

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
            OnStartGame?.Invoke();
        }
    }

    public void LastChance()
    {
        LastChanceAd.Instance.OfferLastChance();
    }

    public void EndGame()
    {
        OnEndGame?.Invoke();
        lastEndTime = Time.realtimeSinceStartup;
        if (SceneManager.GetActiveScene() == SceneManager.GetSceneByName("GameScene"))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

    private void OnApplicationQuit()
    {
        OnPause?.Invoke();
        OnQuit?.Invoke();
        PlayerPrefs.Save();
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            OnPause?.Invoke();
            PlayerPrefs.Save();
        }
    }
}
