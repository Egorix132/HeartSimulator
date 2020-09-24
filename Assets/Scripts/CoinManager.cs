using UnityEngine;

public class CoinManager : MonoBehaviour
{
    public static CoinManager Instance { get; private set; }

    public int Coins { get; private set; }

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

        Coins += PlayerPrefs.GetInt("coins", 0);

        GameManager.OnEndGame += SaveCoins;
        GameManager.OnQuit += SaveCoins;
    }

    public void AddCoins(int coins)
    {
        Coins += coins;
    }

    public bool SpendCoins(int coins)
    {
        if(Coins >= coins)
        {
            Coins -= coins;
            return true;
        }

        return false;
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt("coins", Coins);
        PlayerPrefs.Save();
    }
}
