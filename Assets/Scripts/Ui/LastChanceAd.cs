using UnityEngine;

class LastChanceAd : MonoBehaviour
{
    public static LastChanceAd Instance { get; private set; }

    [SerializeField] private GameObject Buttons;

    private bool lastChanceIsUsed = false;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance == this)
        {
            Destroy(gameObject);
            return;
        }
    }

    public void OfferLastChance()
    {
        if(lastChanceIsUsed){
            Refuse();
            return;
        }

        Age age = GameTime.Instance.AgeTime;
        string text = $"You have only played for {age.Year()} years and {age.Month()} month. Watch this ad and get another chance!";
        PauseManager.Instance.ShowModal(new Vector3(0,0,0), text, false);
        Buttons.SetActive(true);
    }

    public void ShowAd()
    {
        RewardAdManager.Instance.ShowRewardAd(RewardAdManager.Instance.LastChanceId, HandleAdResult);
        Buttons.SetActive(false);
        lastChanceIsUsed = true;
    }

    private void HandleAdResult(bool success)
    {
        PauseManager.Instance.CloseModal();
        if (success)
        {
            HeartHealth.Instance.SetLastChance();
        }
        else
        {
            GameManager.Instance.EndGame();
        }
    }

    public void Refuse()
    {
        PauseManager.Instance.CloseModal();
        GameManager.Instance.EndGame();
    }
}

