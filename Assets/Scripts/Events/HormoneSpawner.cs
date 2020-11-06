using SplineMesh;
using System;
using System.Collections.Generic;
using UnityEngine;

class HormoneSpawner : MonoBehaviour
{
    public static HormoneSpawner Instance { get; private set; }

    [SerializeField] private GameObject hormonePrefab;

    public static event Action<Hormone, float> OnAppear;
    public static event Action<Hormone> OnSet;
    public static event Action<float> OnDisappear;
    public static event Action OnRelease;

    private Spline spline;

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
        spline = GetComponent<Spline>();
    }

    public void SpawnHormone(HormoneData hormoneData)
    {
        Hormone newHormone
            = Instantiate(hormonePrefab, transform)
                .GetComponent<Hormone>();
        newHormone.data = hormoneData;
        StartCoroutine(newHormone.Run(spline, OnAppear, OnSet, OnDisappear, OnRelease));
    }
}


