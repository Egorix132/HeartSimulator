using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartHealth : MonoBehaviour
{
    public static HeartHealth Instance { get; private set; }
    
    [SerializeField] private UnityEngine.UI.Image HealthBar;
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
        }
    }

    void Start()
    {
        Health = maxHeartHealth;
        screenEffects = MainCamera.Instance.GetComponent<VisualEffect>();
    }

    public void TakeDamage(float damage)
    {
        Health -= damage;
        if(Health > 0 && damage != 0)
        {
            Color color = damage > 0 ? damageColor : healColor;
            float transparency = damage * 10 / Health < 1 ? Math.Abs(damage) * 10 / Health : 1;
            screenEffects.SetEffect(color, 1);
            HealthBar.fillAmount = Health / maxHeartHealth;
        }       
    }
}
