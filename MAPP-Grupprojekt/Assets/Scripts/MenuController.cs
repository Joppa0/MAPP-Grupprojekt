using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // SceneManager.LoadSceneAsync("Level 1");  --- Här kan vi skriva in vad leveln heter. När man klickar på Play, ska leveln som står köras.
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
