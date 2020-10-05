using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TuitionManager : MonoBehaviour
{
    private bool enableTuition;
    private List<int> displayedHormones = new List<int>();
    private TuitionText texts;

    void Start()
    {
        texts = JsonResources.tuitionTexts;
        enableTuition = Convert.ToBoolean(PlayerPrefs.GetInt("Tuition", 1));

        if (!enableTuition)
        {
            return;
        }

        if (PlayerPrefs.GetInt("Tuition_FirstBeat", 1) == 1)
        {
            Heartbeat.OnBeat += OnFirstBeat;
        }

        string stringHormones = PlayerPrefs.GetString("Tuition_Hormones", "{}");
        displayedHormones = JsonHelper.FromJson<int>(stringHormones).ToList();

        if (displayedHormones.Count < texts.Hormones.Count())
        {
            GameManager.OnEndGame += SaveTuition;
            GameManager.OnPause += SaveTuition;
            HormoneSpawner.OnSet += OnHormoneSetted;
        }
    }

    private void OnFirstBeat()
    {
        Heartbeat.OnBeat -= OnFirstBeat;
        PlayerPrefs.SetInt("Tuition_FirstBeat", 0);
        PauseManager.Instance.ShowModal(new Vector3(0, 0, 0), texts.FirstBeat);
    }

    private void OnHormoneSetted(Hormone hormone)
    {
        if (!displayedHormones.Contains(hormone.data.id))
        {
            displayedHormones.Add(hormone.data.id);
            PauseManager.Instance.ShowModal(new Vector3(0, 0, 0), texts.Hormones[hormone.data.id]);
        }

        if (displayedHormones.Count >= texts.Hormones.Count())
        {
            PlayerPrefs.SetString(
                "Tuition_Hormones",
                JsonUtility.ToJson(displayedHormones));

            HormoneSpawner.OnSet -= OnHormoneSetted;
        }
    }

    private void SaveTuition()
    {
        var displayedHormonesJson = JsonHelper.ToJson(displayedHormones.ToArray());
        PlayerPrefs.SetString(
                "Tuition_Hormones",
                displayedHormonesJson);
    }
}
