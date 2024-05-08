using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    private Slider slider;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = AudioSettingsManager.instance.volume; // Set initial slider value
        slider.onValueChanged.AddListener(SetVolume);
    }

    void SetVolume(float volume)
    {
        AudioSettingsManager.instance.volume = volume; // Update the global volume
    }
}
