using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartStrength : MonoBehaviour
{
    public static HeartStrength Instance { get; private set; }

    [SerializeField] private BPMBorders bpmBorders;

    [SerializeField] private int normalRangeStandart;
    [SerializeField] private int wearStrength;
    [SerializeField] private int healRangeStandart;
    [SerializeField] private int healStrength;

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

    void OnEnable()
    {
        BPM.OnBPMUpdate += CalcStrength;
    }

    private void CalcStrength(int newBpm, int oldBpm)
    {
        if(Time.timeSinceLevelLoad > GameManager.Instance.handicapTime)
        {
            float damage = Math.Abs(newBpm - oldBpm) <= healRangeStandart && newBpm != 0 ? -healStrength : 0;

            damage += Math.Abs(newBpm - oldBpm) > normalRangeStandart ? wearStrength : 0;
            damage += newBpm < bpmBorders.MinBorder ? (bpmBorders.MinBorder - newBpm) : 0;
            damage += newBpm > bpmBorders.MaxBorder ? (newBpm - bpmBorders.MaxBorder) : 0;
            HeartHealth.Instance.TakeDamage(damage);
        }
    }

    private void OnDisable()
    {
        BPM.OnBPMUpdate -= CalcStrength;
    }
}
