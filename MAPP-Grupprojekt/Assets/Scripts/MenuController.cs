using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private int levelToLoad;
    public void PlayGame()
    {
         SceneManager.LoadSceneAsync(levelToLoad); // H�r kan vi skriva in vad leveln heter, "Level 1", ELLER siffran p� SceneBuildIndex. N�r man klickar p� Play, ska leveln som st�r k�ras.
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
