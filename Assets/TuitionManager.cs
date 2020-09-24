using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TuitionManager : MonoBehaviour
{
    private bool enableTuition;
    // Start is called before the first frame update
    void Start()
    {
        enableTuition = Convert.ToBoolean(PlayerPrefs.GetInt("Tuition", 1));
        Heartbeat.OnBeat += OnFirstBeat;
    }

    private void OnFirstBeat()
    {
        Heartbeat.OnBeat -= OnFirstBeat;
    }
}
