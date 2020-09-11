using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [SerializeField] private Transform StartButton;

    public float handicapTime;

    private void Awake()
    {
        if (Instance == null)
        { 
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
        }
    }

    private void OnMouseUpAsButton()
    {
        if(SceneManager.GetActiveScene() != SceneManager.GetSceneByName("GameScene"))
        {         
            SceneManager.LoadScene("GameScene");
        }           
    }
}
