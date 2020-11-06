using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System;
using UnityEngine;

public class Authentification : MonoBehaviour
{
    public static Authentification Instance { get; private set; }

    public static PlayGamesPlatform platform;
    public static Action AfterAuth;

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
            if (success)
            {
                AfterAuth?.Invoke();
            }
        });
    }
}
