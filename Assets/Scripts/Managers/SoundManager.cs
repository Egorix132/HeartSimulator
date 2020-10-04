using System.Collections;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    [SerializeField] private AudioSource mainSource;
    [SerializeField] private int mainMelodyTemp;

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
            float pitch = (float)bpm / mainMelodyTemp;
            mainSource.pitch = pitch;
        }
    }

    private void StopMainMelody()
    {
        StopCoroutine(ChangeTemp());
        mainSource.Stop();
    }

    public void PlaySound(AudioClip sound)
    {
        GameObject soundObject = new GameObject("sound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(sound);
    }
}
