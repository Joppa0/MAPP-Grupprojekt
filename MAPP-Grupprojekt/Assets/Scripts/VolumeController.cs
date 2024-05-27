using UnityEngine;
using UnityEngine.SceneManagement;  

public class VolumeController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip[] clips;  //Array att h�lla b�da l�tarna

    void Start()
    {
        DontDestroyOnLoad(gameObject);
        audioSource = GetComponent<AudioSource>();
        SceneManager.sceneLoaded += OnSceneLoaded;  //K�rs n�r ny scene laddas
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
            if (clips.Length > 1)  // Kontrollera att det finns minst tv� element i arrayen f�r att undvika IndexOutOfBound
            {
                audioSource.clip = clips[1];  // Byt till andra l�ten i arrayen
                audioSource.Play();  // B�rja spela
            }
        }
        if (scene.buildIndex == 0)  // Vid laddning av mainmenu (scene 0)
        {
            if (clips.Length > 0)  // Kontrollera att det finns minst ett element i arrayen
            {
                audioSource.clip = clips[0];  // Byt till f�rsta i arrayen
                audioSource.Play();
            }
    }

}

void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;  // Unsubscribe to avoid memory leaks
    }
}
