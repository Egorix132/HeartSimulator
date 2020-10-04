using System;
using UnityEngine;
using static MaxSdkBase;

class AdManager : MonoBehaviour
{
    public static AdManager Instance { get; private set; }

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
            MaxSdk.ShowMediationDebugger();
        };


        MaxSdk.SetSdkKey("W-sOyxtIUM_SnkZdrA6as0fDUylIazyI401izbxa_Mhq6E4yWtMl6-4zBiOVwACtfq-NVqcVXHiS41RxmATPp_");
        MaxSdk.InitializeSdk();
    }
}

