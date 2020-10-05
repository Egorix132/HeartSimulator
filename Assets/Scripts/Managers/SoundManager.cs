using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource mainSource;
    [SerializeField] private int mainMelodyTemp;

    private readonly List<AudioSource> audioSources = new List<AudioSource>();

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
        mainSource = GetComponent<AudioSource>();
        GameManager.OnStartGame += StartMainMelody;
        GameManager.OnEndGame += StopMainMelody;
    }

    private void StartMainMelody()
    {
        mainSource.Play();
        StartCoroutine(ChangeTemp());
    }

    private IEnumerator ChangeTemp()
    {
        yield return new WaitForFixedUpdate();
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            int bpm = BPM.Instance.Bpm;
            float pitch = (float)Math.Sqrt((float)bpm / mainMelodyTemp);
            if(Math.Abs(mainSource.pitch - pitch) > 0.1f)
            {
                mainSource.pitch = pitch;
            }
        }
    }

    private void StopMainMelody()
    {
        StopCoroutine(ChangeTemp());
        mainSource.Stop();
    }

    public void PlaySound(AudioClip sound)
    {
        AudioSource audioSource = audioSources.Find(s => !s.isPlaying);
        if (audioSource == null)
        {
            GameObject soundObject = new GameObject("audioSource");
            audioSource = soundObject.AddComponent<AudioSource>();
            audioSources.Add(audioSource);
        }

        audioSource.PlayOneShot(sound);
    }
}
