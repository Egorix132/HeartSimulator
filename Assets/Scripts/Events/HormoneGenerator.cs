using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class HormoneGenerator : MonoBehaviour
{
    public static HormoneGenerator Instance { get; private set; }

    private List<HormoneData> hormoneList;

    public bool HasHormone { get; private set; } = false;
    private Action onReleaseHormone;

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
        }
    }

    private void Start()
    {
        onReleaseHormone = () => HasHormone = false;
        random = new System.Random();
        hormoneList = Resources.LoadAll<HormoneData>("Hormones").ToList();
        StartCoroutine(GenerateHormone());
        HormoneSpawner.OnRelease += onReleaseHormone;
    }

    private IEnumerator GenerateHormone()
    {
        yield return new WaitForSeconds(GameManager.Instance.handicapTime);
        while (true)
        {    
            DateTime time = GameTime.Instance.DateTime;
            if (!HasHormone)
            {
                HormoneData hormone = hormoneList?.Where(h => h.timeInterval.Contains(time))?.
                                FirstOrDefault(h => {
                                    return h.chance > random.Next(0, 100);
                                });

                if (hormone != null)
                {
                    HasHormone = true;
                    HormoneSpawner.Instance.SpawnHormone(hormone);
                }
            }
            yield return new WaitForSeconds(1);
        }
    }

    private void OnDisable()
    {
        HormoneSpawner.OnRelease -= onReleaseHormone;
    }
}
