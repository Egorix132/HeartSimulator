using SplineMesh;
using UnityEngine;

[RequireComponent(typeof(Spline))]
public class BloodGeneratorBase : MonoBehaviour
{
    [SerializeField] protected int bloodDropsCount;
    [SerializeField] protected float bloodDropsSpawnRate;
    [SerializeField] protected GameObject bloodPrefab;

    private System.Random r = new System.Random();

    protected void SpawnBlood(float remainTime)
    {
        for (int i = 0; i < bloodDropsCount; i++)
        {
            var o = Instantiate(bloodPrefab, transform, false);
            o.GetComponent<SplineFollower>()
             .Init(
                GetComponent<Spline>(),
                remainTime,
                () => Destroy(o),
                r.Next(0, 30) / 30f,
                new Vector3(
                    r.Next(-50, 50) / 3000f,
                    r.Next(-50, 50) / 3000f,
                    0));
        }
    }
}
