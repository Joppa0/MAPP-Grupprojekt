using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        // SceneManager.LoadSceneAsync("Level 1");  --- H�r kan vi skriva in vad leveln heter. N�r man klickar p� Play, ska leveln som st�r k�ras.
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
