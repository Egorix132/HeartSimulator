using System;
using System.Collections.Generic;
using UnityEngine;

class HormoneSpawner : MonoBehaviour
{

    public static HormoneSpawner Instance { get; private set; }

    [SerializeField] private List<Vector3> spawnPoints = new List<Vector3>();
    [SerializeField] private GameObject hormonePrefab;

    public static event Action<Hormone, float> OnAppear;
    public static event Action<Hormone> OnSet;
    public static event Action<float> OnDisappear;
    public static event Action OnRelease;

    System.Random random;

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
        random = new System.Random();
    }

    public void SpawnHormone(HormoneData hormone)
    {
        Vector3 spawnPoint = spawnPoints[random.Next(0, spawnPoints.Count - 1)];
        Hormone newHormone = Instantiate(hormonePrefab, spawnPoint, new Quaternion(), null).GetComponent<Hormone>();
        newHormone.data = hormone;
        StartCoroutine(newHormone.Run(OnAppear, OnSet, OnDisappear, OnRelease));
    }
}


