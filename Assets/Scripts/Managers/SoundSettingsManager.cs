using UnityEngine;
using UnityEngine.UI;

public class SoundSettingsManager : MonoBehaviour
{
    [SerializeField] private Slider volumeSlider;

    private string volumePrefName = "volume";
    private float volume;

    private void Start()
    {
        volume = PlayerPrefs.GetFloat(volumePrefName, 1);
        SoundManager.Instance.ChangeVolume(volume);
        volumeSlider.value = volume;
        GameManager.OnStartGame += SaveVolume;
        GameManager.OnPause += SaveVolume;
    }

    public void ChangeVolume(float newVolume)
    {
        volume = newVolume;
        SoundManager.Instance.ChangeVolume(newVolume);
    }

    private void SaveVolume()
    {
        PlayerPrefs.SetFloat(volumePrefName, volume);
    }
}
