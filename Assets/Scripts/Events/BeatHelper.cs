using System.Collections;
using UnityEngine;

public class BeatHelper : MonoBehaviour
{
    public static BeatHelper Instance { get; private set; }

    [SerializeField] private GameObject greenZoneImage;

    private GameObject greenZone;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnEnable()
    {
        greenZone = Instantiate(greenZoneImage, transform, false);
    }

    // Update is called once per frame
    void Update()
    {
        var (min, max) = GetIdealTimes();
        greenZone.transform.position = new Vector3((max + min) / 2 + 0.6f, greenZone.transform.position.y, 0);
        greenZone.transform.localScale = new Vector3((min - max) * 7f, 1, 1);
    }

    private (float min, float max) GetIdealTimes()
    {
        float minTime = BPM.Instance.CalcIdealTime(BPMBorders.Instance.MinBorder);
        float maxTime = BPM.Instance.CalcIdealTime(BPMBorders.Instance.MaxBorder);
        return (minTime, maxTime);
    }

    private void OnDisable()
    {
        Destroy(greenZone);
    }

    public void Activate(float duration)
    {
        StartCoroutine(RunHelper(duration));
    }

    private IEnumerator RunHelper(float duration)
    {
        enabled = true;
        yield return new WaitForSeconds(duration);
        enabled = false;
    }
}
