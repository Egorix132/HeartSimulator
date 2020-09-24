using System;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    public static UpgradeManager Instance { get; private set; }

    [SerializeField] private GameObject upgradeUI;

    [SerializeField] private Text title;
    [SerializeField] private Text description;
    [SerializeField] private Text cost;
    [SerializeField] private Image button;

    [SerializeField] private Sprite buyImage;
    [SerializeField] private Sprite boughtImage;

    [SerializeField] private Color unselectedNotBoughtColor;
    [SerializeField] private Color unselectedBoughtColor;
    [SerializeField] private Color selectedColor;

    private UpgradeBase currentUpgrade;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Select(UpgradeBase upgrade)
    {
        Unselect();

        upgrade.Image.color = selectedColor;

        title.text = $"{upgrade.upgradeTitle} {upgrade.level}";
        description.text = upgrade.upgradeDescription;

        if (upgrade.IsBought)
        {
            button.sprite = boughtImage;
            cost.text = "Bought";
        }
        else
        {
            button.sprite = buyImage;
            cost.text = upgrade.cost.ToString();
        }

        currentUpgrade = upgrade;

        upgradeUI.SetActive(true);
    }

    private void OnDisable()
    {
        Unselect();
        upgradeUI.SetActive(false);
    }

    public void Unselect()
    {
        if (currentUpgrade != null)
        {
            currentUpgrade.Image.color = currentUpgrade.IsBought
                ? unselectedBoughtColor
                : unselectedNotBoughtColor;
        }
    }

    public void Buy()
    {
        if(currentUpgrade != null && !currentUpgrade.IsBought)
        {
            currentUpgrade.Buy();
        }
    }
}
