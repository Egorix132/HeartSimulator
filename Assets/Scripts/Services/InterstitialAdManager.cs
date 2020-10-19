using System;
using UnityEngine;
using static MaxSdkBase;

class InterstitialAdManager : MonoBehaviour
{
    public static InterstitialAdManager Instance { get; private set; }

    public readonly string AfterGameId = "40f565c9d5de51a8";
    private int retryAttempt;

    [SerializeField] private int gameCountBeforeAd;

    private int gameCounter = -1;

    public void ShowInterstitialAd(string interstitialAdUnitId)
    {
        if (MaxSdk.IsInterstitialReady(interstitialAdUnitId))
        {
            MaxSdk.ShowInterstitial(interstitialAdUnitId);
        }
    }

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

        MaxSdkCallbacks.OnSdkInitializedEvent += (SdkConfiguration sdkConfiguration) => {
            // AppLovin SDK is initialized, configure and start loading ads.
            InitializeInterstitialAds();
        };

        GameManager.OnFailGame += ShowAdAfterGame;
    }

    private void ShowAdAfterGame()
    {
        gameCounter = (gameCounter + 1) % gameCountBeforeAd;

        if(gameCounter != 0)
        {
            return;
        }

        ShowInterstitialAd(AfterGameId);
    }

    public void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnInterstitialLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.OnInterstitialLoadFailedEvent += OnInterstitialFailedEvent;
        MaxSdkCallbacks.OnInterstitialAdFailedToDisplayEvent += InterstitialFailedToDisplayEvent;
        MaxSdkCallbacks.OnInterstitialHiddenEvent += OnInterstitialDismissedEvent;

        // Load the first interstitial
        LoadInterstitial(AfterGameId);
    }

    private void LoadInterstitial(string interstitialAdUnitId)
    {
        MaxSdk.LoadInterstitial(interstitialAdUnitId);
    }

    private void OnInterstitialLoadedEvent(string adUnitId)
    {
        // Interstitial ad is ready to be shown. MaxSdk.IsInterstitialReady(interstitialAdUnitId) will now return 'true'

        // Reset retry attempt
        retryAttempt = 0;
    }

    private void OnInterstitialFailedEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to load 
        // We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds)

        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));

        Invoke("LoadInterstitial", (float)retryDelay);
    }

    private void InterstitialFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        // Interstitial ad failed to display. We recommend loading the next ad
        LoadInterstitial(adUnitId);
    }

    private void OnInterstitialDismissedEvent(string adUnitId)
    {
        // Interstitial ad is hidden. Pre-load the next ad
        LoadInterstitial(adUnitId);
    }
}

