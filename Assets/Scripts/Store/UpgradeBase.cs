using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
[RequireComponent(typeof(BoxCollider2D))]
public class UpgradeBase : MonoBehaviour
{
    public string upgradeTitle;
    public string upgradeDescription;
    public int cost;
    public int level;

    public Image Image { get; private set; }

    public bool IsBought { get; private set; }

    private void Awake()
    {
        IsBought = PlayerPrefs.GetInt(upgradeTitle, 0) >= level;
        Image = GetComponent<Image>();
        Image.color = IsBought ? Color.white : Image.color;
    }

    public void Buy()
    {
        if (!IsBought && CoinManager.Instance.SpendCoins(cost))
        {
            IsBought = true;

            PlayerPrefs.SetInt(upgradeTitle, level);

            Image.color = Color.white;

            AchievmentsManager.Instance.CountUpgrade();
        }
    }

    public void Select()
    {
        UpgradeManager.Instance.Select(this);
    }

    private void OnMouseUp()
    {
        Select();
    }
}
