using System.Collections;
using UnityEngine;

public class HeartBloodGenerator : BloodGeneratorBase
{
    void Start()
    {
        Heartbeat.Instance.OnUp += Spawn;
    }

    private void Spawn()
    {
        StartCoroutine(SpawnCoroutine());
    }

    private IEnumerator SpawnCoroutine()
    {
        SpawnBlood(0.5f);
        yield return new WaitForFixedUpdate();
        SpawnBlood(0.5f);
        yield return new WaitForFixedUpdate();
        SpawnBlood(0.5f);
        yield return new WaitForFixedUpdate();
        SpawnBlood(0.5f);
    }

    private void OnDestroy()
    {
        Heartbeat.Instance.OnUp -= Spawn;
    }
}
