using UnityEngine;

public class SnowballSound : MonoBehaviour
{
    private AudioSource audioSource; // Reference to the AudioSource component

    // Start is called before the first frame update
    void Start()
    {
        // Get the AudioSource component attached to the snowball prefab
        audioSource = GetComponent<AudioSource>();

        // Play the "SnowballFlying" audio clip
        if (audioSource && audioSource.clip)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource or AudioClip not assigned to snowball prefab.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Stop playing the sound if the snowball is destroyed
        if (!gameObject)
        {
            StopSound();
        }
    }

    // Stops playing the sound
    private void StopSound()
    {
        if (audioSource && audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}