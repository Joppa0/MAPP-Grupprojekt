using UnityEngine;
using UnityEngine.SceneManagement;  

public class VolumeController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clips;  //Array att hålla båda låtarna

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;  //Körs när ny scene laddas
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

   
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)  // Laddar den tredje scenen
        {
            if (clips.Length > 1)  // Kontrollera att det finns minst två element i arrayen för att undvika IndexOutOfBound
            {
                audioSource.clip = clips[1];  // Byt till andra låten i arrayen
                audioSource.Play();  // Börja spela
            }
        }
        if (scene.buildIndex == 0)  // Vid laddning av mainmenu (scene 0)
        {
            if (clips.Length > 0)  // Kontrollera att det finns minst ett element i arrayen
            {
                audioSource.clip = clips[0];  // Byt till första i arrayen
                audioSource.Play();
            }
    }

}

void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Unsubscribe to avoid memory leaks
    }
}
