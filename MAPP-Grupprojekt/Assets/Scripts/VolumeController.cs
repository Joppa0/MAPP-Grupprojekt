using UnityEngine;
using UnityEngine.SceneManagement;  // Required for detecting scene changes

public class VolumeController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clips;  // Array to hold multiple clips

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;  // Subscribe to the sceneLoaded event
        UpdateVolume();
    }

    void Update()
    {
        UpdateVolume();
    }

    private void UpdateVolume()
    {
        if (AudioSettingsManager.instance != null)
            audioSource.volume = AudioSettingsManager.instance.volume;
    }

    // This method is called every time a scene is loaded
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)  //laddar den tredje scenen
        {
            audioSource.clip = clips[1];  //Byt till andra l�ten i arrayen
            audioSource.Play();  //B�rja spela
        }
        if (scene.buildIndex == 0)  //Vid laddning av mainmenu (scene 0)
        {
            audioSource.clip = clips[0];  //Byt till f�rsta i arrayen
            audioSource.Play();  
        }

    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Unsubscribe to avoid memory leaks
    }
}
