using System.Collections.Generic;
using UnityEngine;

public class BloodGeneratorBase : MonoBehaviour
{
    public List<Transform> keyPositions = new List<Transform>();

    [SerializeField] protected int bloodDropsCount;
    [SerializeField] protected float bloodDropsSpawnRate;
    [SerializeField] protected GameObject bloodPrefab;

    protected System.Random r = new System.Random();

    protected void SpawnBlood()
    {
        for (int i = 0; i < bloodDropsCount; i++)
        {
            var o = Instantiate(bloodPrefab, transform, false);
            o.transform.position += new Vector3(r.Next(-100, 100) * 0.002f, r.Next(-100, 100) * 0.002f, 0);
        }
    }
}
