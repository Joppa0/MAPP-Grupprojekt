using UnityEngine;
using UnityEngine.UI; // Required for accessing UI components like Slider

public class VolumeController : MonoBehaviour
{
    public Slider volumeSlider; // Drag your slider here in the inspector
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        audioSource.volume = volumeSlider.value; // Set volume based on slider value
    }
}
