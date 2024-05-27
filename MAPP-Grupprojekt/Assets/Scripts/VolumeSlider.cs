using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    private Slider slider; // s�tts p� varje slider komponent i sspelet. 

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = AudioSettingsManager.instance.volume; // Set initial slider value
        slider.onValueChanged.AddListener(SetVolume); //Vid �ndrat v�rde kikar vi p� Setvolume. 
    }

    void SetVolume(float volume)
    {
        AudioSettingsManager.instance.volume = volume; // Update the global volume
    }
}
