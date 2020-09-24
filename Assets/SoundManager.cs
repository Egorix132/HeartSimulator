using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PlaySound(AudioClip sound)
    {
        GameObject soundObject = new GameObject("sound");
        AudioSource audioSource = soundObject.AddComponent<AudioSource>();
        audioSource.PlayOneShot(sound);
    }
}
