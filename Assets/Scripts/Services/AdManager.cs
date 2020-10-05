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
            if (sdkConfiguration.ConsentDialogState == MaxSdkBase.ConsentDialogState.Applies)
            {
                MaxSdk.SetHasUserConsent(false);
            }
            else if (sdkConfiguration.ConsentDialogState == MaxSdkBase.ConsentDialogState.DoesNotApply)
            {
                // No need to show consent dialog, proceed with initialization
            }
            else
            {
                // Consent dialog state is unknown. Proceed with initialization, but check if the consent
                // dialog should be shown on the next application initialization
            }

            // AppLovin SDK is initialized, configure and start loading ads.
            MaxSdk.ShowMediationDebugger();
        };


        MaxSdk.SetSdkKey("W-sOyxtIUM_SnkZdrA6as0fDUylIazyI401izbxa_Mhq6E4yWtMl6-4zBiOVwACtfq-NVqcVXHiS41RxmATPp_");
        MaxSdk.InitializeSdk();
    }
}

