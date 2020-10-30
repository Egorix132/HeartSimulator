using System.Linq;
using UnityEngine;

public class AchievmentsManager : MonoBehaviour
{
    public static AchievmentsManager Instance { get; private set; }

    public int beatCount;
    public int upgradesCount;

    public bool pubertyAchieved;


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
        Social.LoadAchievements((achievments) =>
            {
                pubertyAchieved 
                    = achievments.ToList()
                                 .Find((a) => a.id == GPGSIds.achievement_may_be_enougth)
                                 .completed;
            });
        beatCount = PlayerPrefs.GetInt("Achievments.BeatCount", 0);
        upgradesCount = PlayerPrefs.GetInt("Achievments.UpgradesCount", 0);


        Heartbeat.OnBeat += CountBeat;
        GameManager.OnPause += SaveProgress;
        GameManager.OnEndGame += ReportBeats;
    }

    public void UpdateLeaderboardScore(long value, string leaderboard)
    {
        Social.ReportScore(value, leaderboard, null);
    }

    public void OpenAchivments()
    {
        Social.ShowAchievementsUI();
    }

    private void CountBeat()
    {
        beatCount++;
        if (beatCount == 1)
        {
            Social.ReportProgress(GPGSIds.achievement_first_beat, 100, (bool _) => { });
        }

        if (beatCount == 1000)
        {
            Social.ReportProgress(GPGSIds.achievement_this_is_just_the_beginning, 100, (bool _) => { });
        }

        if (!pubertyAchieved && GameTime.Instance?.AgeTime.Year() > 13)
        {
            Social.ReportProgress(GPGSIds.achievement_may_be_enougth, 100, (bool _) => { });
        }
    }

    public void CountUpgrade()
    {
        upgradesCount++;
        Social.ReportProgress(GPGSIds.achievement_full_health, upgradesCount / 25, (bool _) => { });      
    }

    private void ReportBeats()
    {
        Social.ReportProgress(GPGSIds.achievement_this_is_just_the_beginning, beatCount / 1000, (bool _) => { });
    }

    private void SaveProgress()
    {
        PlayerPrefs.SetInt("Achievments.BeatCount", beatCount);
        PlayerPrefs.SetInt("Achievments.UpgradesCount", upgradesCount);
    }
}

