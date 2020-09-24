using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class BPM : MonoBehaviour, HeartbeatListener
{
    public static BPM Instance { get; private set; }

    public delegate void BPMUpdate(int newBpm, int oldBpm);
    public static event BPMUpdate OnBPMUpdate;

    [SerializeField] private Text BPMText;
    [SerializeField] private float updateRate = 1;
    [SerializeField] private int saveBeatsCount = 3;

    private readonly List<float> uniqBeats = new List<float>();
    public int Bpm { get; private set; } = 0;
    public int OldBpm { get; private set; } = 0;
    private float waitingNewTimer;
    private float updateTimer = 2;

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

    private void OnEnable()
    {
        Heartbeat.OnBeat += AddBeat;
    }

    void Update()
    {
        if(updateTimer <= 0)
        {
            CalcBPM();
        }
        updateTimer -= Time.deltaTime;
        waitingNewTimer += Time.deltaTime;
        Bpm = (int)(uniqBeats.Count / waitingNewTimer * 60 + Bpm) / 2;

        if (waitingNewTimer > 5)
        {
            DeleteFirstBeat();
        }
    }

    private void CalcBPM()
    {
        updateTimer = updateRate;
        Bpm = (int)(uniqBeats.Count / waitingNewTimer * 60 + Bpm) / 2;
        BPMText.text = Bpm.ToString();
        OnBPMUpdate?.Invoke(Bpm, OldBpm);
        OldBpm = Bpm;
    }

    public float CalcIdealTime(float idealBpm)
    {
        float add = uniqBeats.Count > saveBeatsCount ? uniqBeats[0] : 0;
        float averageBpm = ((int)idealBpm) * 2 - Bpm;
        float time 
            = (uniqBeats.Count * 60f) / (averageBpm == 0 ? 60 : averageBpm)
            + add - waitingNewTimer;
        return time;
    }

    private void DeleteFirstBeat()
    {
        if (uniqBeats.Count > 0)
        {
            waitingNewTimer -= uniqBeats[0];

            for (int i = 1; i < uniqBeats.Count; i++)
            {
                uniqBeats[i] -= uniqBeats[0];
            }
            uniqBeats.RemoveAt(0);
        }
    }

    public void AddBeat()
    {
        uniqBeats.Add(waitingNewTimer);
        if (uniqBeats.Count > saveBeatsCount)
        {
            DeleteFirstBeat();
        }
        CalcBPM();
    }

    void OnDisable()
    {
        Heartbeat.OnBeat -= AddBeat;
    }
}
