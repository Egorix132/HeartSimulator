using System.Collections;
using UnityEngine;

public class LineCardiogram : MonoBehaviour, HeartbeatListener
{
    private LineRenderer line;

    void OnEnable()
    {
        Heartbeat.OnBeat += AddBeat;
        line = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        for (int i = 0; i < line.positionCount; i++)
        {
            line.SetPosition(i, new Vector3(-i * 0.03f, 0, 0));
        }
    }

    void FixedUpdate()
    {   
        for (int i = line.positionCount - 1; i > 0; i--)
        {
            float oldX = line.GetPosition(i).x;
            float newY = line.GetPosition(i - 1).y;
            line.SetPosition(i, new Vector3(oldX, newY, 0));
        }
    }

    public IEnumerator Beat()
    {
        
        line.SetPosition(0, new Vector3(0, 0.05f, 0));
        yield return new WaitForFixedUpdate();
        line.SetPosition(0, new Vector3(0, 0, 0));
        yield return new WaitForSeconds(0.03f);

        line.SetPosition(0, new Vector3(0, -0.03f, 0));
        yield return new WaitForFixedUpdate();

        line.SetPosition(0, new Vector3(0, 0.1f, 0));
        yield return new WaitForFixedUpdate();

        line.SetPosition(0, new Vector3(0, -0.08f, 0));
        yield return new WaitForSeconds(0.02f);

        line.SetPosition(0, new Vector3(0, 0, 0));
        yield return new WaitForSeconds(0.03f);

        line.SetPosition(0, new Vector3(0, 0.03f, 0));
        yield return new WaitForFixedUpdate();

        line.SetPosition(0, new Vector3(0, 0, 0));
    }

    public void AddBeat()
    {
        StartCoroutine(Beat());
    }

    void OnDisable()
    {
        Heartbeat.OnBeat -= AddBeat;
    }
}
