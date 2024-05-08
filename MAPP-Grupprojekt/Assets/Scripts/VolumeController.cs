using UnityEngine;

public class VolumeController : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        UpdateVolume(); // Initial volume setting
    }

    void Update()
    {
        UpdateVolume(); // Continuously update volume
    }

    private void UpdateVolume()
    {
        if (AudioSettingsManager.instance != null)
            audioSource.volume = AudioSettingsManager.instance.volume;
    }
}
