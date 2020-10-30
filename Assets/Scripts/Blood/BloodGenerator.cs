using System.Collections;
using UnityEngine;

public class BloodGenerator : BloodGeneratorBase
{
    void Start()
    {
        StartCoroutine(SpawnBloodCoroutine());
    }

    private IEnumerator SpawnBloodCoroutine()
    {
        while (true)
        {
            float minTime = BPM.Instance.CalcIdealTime(BPMBorders.Instance.MinBorder);

            if (minTime < 0.5f)
            {
                SpawnBlood();
            }

            yield return new WaitForSeconds(1 / bloodDropsSpawnRate);
        }
    }
}
