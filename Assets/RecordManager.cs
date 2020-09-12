﻿using UnityEngine;

public class RecordManager : MonoBehaviour
{
    public static RecordManager Instance { get; private set; }

    public Age recordAge = new Age(0, 0);

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

        recordAge = recordAge.AddYear(PlayerPrefs.GetInt("recYear", 0));
        recordAge = recordAge.AddMonth(PlayerPrefs.GetInt("recMonth", 0));
    }

    public void CheckRecord()
    {
        Age currentAge = GameTime.Instance.AgeTime;
        if (currentAge > recordAge)
        {
            recordAge = currentAge;
        }
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("recYear", recordAge.Year());
        PlayerPrefs.SetInt("recMonth", recordAge.Month());
        PlayerPrefs.Save();
    }
}