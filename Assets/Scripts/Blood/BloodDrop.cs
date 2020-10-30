using System.Collections;
using UnityEngine;

class BloodDrop : MonoBehaviour
{
    [SerializeField] private float speed;

    private Rigidbody2D body;
    private IEnumerator enumerator;

    [SerializeField] private Vector3 currentTarget;

    private void Start()
    {
        body = GetComponent<Rigidbody2D>();
        enumerator
            = transform.parent.GetComponent<BloodGeneratorBase>().keyPositions.GetEnumerator();
        ToNextTarget();
    }

    private void Update()
    {
        if(Vector2.Distance(transform.position, currentTarget) < 0.1f)
        {
            ToNextTarget();
        }
        body.velocity = (currentTarget - transform.position).normalized * speed;
    }

    private void ToNextTarget()
    {
        if (!enumerator.MoveNext())
        {
            Destroy(gameObject);
            return;
        }

        var transform = (Transform)enumerator.Current;

        currentTarget = transform.position;
    }
}
