using System;
using UnityEngine;
using static MaxSdkBase;

class RewardAdManager : MonoBehaviour
{
    public static RewardAdManager Instance { get; private set; }

    public readonly string LastChanceId = "5397906098e954dc";
    private int retryAttempt;

    private event Action<bool> AdCallback;
    private bool getReward = false;

    public void ShowRewardAd(string rewardedAdUnitId, Action<bool> Callback)
    {
        getReward = false;
        AdCallback = Callback;
        if (MaxSdk.IsRewardedAdReady(rewardedAdUnitId))
        {
            MaxSdk.ShowRewardedAd(rewardedAdUnitId);
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
            InitializeRewardedAds();
        };
    }

    private void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.OnRewardedAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.OnRewardedAdLoadFailedEvent += OnRewardedAdFailedEvent;
        MaxSdkCallbacks.OnRewardedAdFailedToDisplayEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.OnRewardedAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.OnRewardedAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.OnRewardedAdHiddenEvent += OnRewardedAdDismissedEvent;
        MaxSdkCallbacks.OnRewardedAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;

        // Load the first RewardedAd
        LoadRewardedAd(LastChanceId);
    }

    private void LoadRewardedAd(string rewardedAdUnitId)
    {
        MaxSdk.LoadRewardedAd(rewardedAdUnitId);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId)
    {
        // Rewarded ad is ready to be shown. MaxSdk.IsRewardedAdReady(rewardedAdUnitId) will now return 'true'

        // Reset retry attempt
        retryAttempt = 0;
    }

    private void OnRewardedAdFailedEvent(string adUnitId, int errorCode)
    {
        // Rewarded ad failed to load 
        // We recommend retrying with exponentially higher delays up to a maximum delay (in this case 64 seconds)

        retryAttempt++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt));

        Invoke("LoadRewardedAd", (float)retryDelay);
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, int errorCode)
    {
        // Rewarded ad failed to display. We recommend loading the next ad
        LoadRewardedAd(adUnitId);
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId) { }

    private void OnRewardedAdClickedEvent(string adUnitId) { }

    private void OnRewardedAdDismissedEvent(string adUnitId)
    {
        // Rewarded ad is hidden. Pre-load the next ad
        LoadRewardedAd(adUnitId);
        AdCallback?.Invoke(getReward);
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, Reward reward)
    {
        // Rewarded ad was displayed and user should receive the reward
        getReward = true;
    }
}

