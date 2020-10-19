using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine;

public class Authentification : MonoBehaviour
{
    public static Authentification Instance { get; private set; }

    public static PlayGamesPlatform platform;

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

        if (platform == null)
        {
            PlayGamesClientConfiguration config
                = new PlayGamesClientConfiguration.Builder().Build();
            PlayGamesPlatform.InitializeInstance(config);
            PlayGamesPlatform.DebugLogEnabled = true;

            platform = PlayGamesPlatform.Activate();
        }

        Social.Active.localUser.Authenticate(success =>
        {
            
        });
    }
}
