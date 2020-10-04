using UnityEngine;

public class LeaderboardManager : MonoBehaviour
{
    public static LeaderboardManager Instance { get; private set; }

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

    public void UpdateLeaderboardScore(long value, string leaderboard)
    {
        Social.ReportScore(value, leaderboard, null);
    }

    public void OpenLeaderboard()
    {
        Social.ShowLeaderboardUI();
    }
}

