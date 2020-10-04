using System;
using UnityEngine;
using UnityEngine.UI;

public class HeartHealth : MonoBehaviour
{
    public static HeartHealth Instance { get; private set; }

    [SerializeField] private Image HealthBar;
    [SerializeField] private float maxHeartHealth;

    [SerializeField] private Color damageColor;
    [SerializeField] private Color healColor;

    private VisualEffect screenEffects;
    public float Health { get; private set; }

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

        maxHeartHealth += PlayerPrefs.GetInt("Heart Health", 0) * 100;
    }

    void Start()
    {
        Health = maxHeartHealth;
        screenEffects = MainCamera.Instance.GetComponent<VisualEffect>();
    }

    public void TakeDamage(float damage)
    {
        if (Health > 0 && damage != 0)
        {
            Health -= damage;
            Color color = damage > 0 ? damageColor : healColor;
            screenEffects.SetEffect(color, 1);
            HealthBar.fillAmount = Health / maxHeartHealth;

            if (Health <= 0)
            {
                GameManager.Instance.LastChance();
            }
        }
    }

    public void SetLastChance()
    {
        Health = maxHeartHealth / 5;
        HealthBar.fillAmount = Health / maxHeartHealth;
    }

    public float GetMaxHealth()
    {
        return maxHeartHealth;
    }
}
