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
         SceneManager.LoadSceneAsync(levelToLoad); // Här kan vi skriva in vad leveln heter, "Level 1", ELLER siffran på SceneBuildIndex. När man klickar på Play, ska leveln som står köras.
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
