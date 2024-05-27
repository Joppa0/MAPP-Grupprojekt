using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class VolumeSlider : MonoBehaviour
{
    private Slider slider; // sätts på varje slider komponent i sspelet. 

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.value = AudioSettingsManager.instance.volume; // Set initial slider value
        slider.onValueChanged.AddListener(SetVolume); //Vid ändrat värde kikar vi på Setvolume. 
    }

    void SetVolume(float volume)
    {
        AudioSettingsManager.instance.volume = volume; // Update the global volume
    }
}
