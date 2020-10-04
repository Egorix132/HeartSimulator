using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class BPMBorders : MonoBehaviour
{
    public static BPMBorders Instance { get; private set; }

    [SerializeField] private HormoneData day;
    [SerializeField] private HormoneData night;

    private HormoneData dayTime;

    private float additionalRange = 0;
    public float MaxBorder { get; private set; } = 0;
    public float MinBorder { get; private set; } = 0;

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
        additionalRange = PlayerPrefs.GetInt("Save Range", 0) * 0.5f;
    }

    private void Start()
    {
        UpdateDayTime();

        HormoneSpawner.OnAppear += SetHormone;
        HormoneSpawner.OnDisappear += SetInitialHormone;
    }

    void Update()
    {
        UpdateDayTime();
    }

    public void SetHormone(Hormone hormone, float moveTime)
    {
        StartCoroutine(ToHormone(hormone.data, moveTime));
    }

    public void SetInitialHormone(float disappearTime)
    {
        StartCoroutine(ToHormone(dayTime, disappearTime));
    }

    public IEnumerator ToHormone(HormoneData hormone, float transitionTime)
    {
        MaxBorder = MaxBorder > hormone.maxBorder ? MaxBorder : hormone.maxBorder;
        MinBorder = MinBorder < hormone.minBorder ? MinBorder : hormone.minBorder;
        yield return new WaitForSeconds(transitionTime);
        MaxBorder = hormone.maxBorder;
        MinBorder = hormone.minBorder;

        MaxBorder += additionalRange;
        MinBorder -= additionalRange;
    }

    private void UpdateDayTime()
    {
        DateTime time = GameTime.Instance.DateTime;
        if (time.Hour > 6 && time.Hour < 22)
        {
            dayTime = day;
        }
        else
        {
            dayTime = night;
        }
        if (!HormoneGenerator.Instance.HasHormone)
        {
            MinBorder = dayTime.minBorder;
            MaxBorder = dayTime.maxBorder;
        }
    }

    private void OnDisable()
    {
        HormoneSpawner.OnAppear -= SetHormone;
        HormoneSpawner.OnDisappear -= SetInitialHormone;
    }
}

