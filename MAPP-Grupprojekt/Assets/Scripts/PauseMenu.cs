using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{ 
    public GameObject pauseMenu;
    public Button menuButton; // Button to show the menu
    public Button resumeButton; // Button to continue the game
    public Button homeButton; // Button to go to the home screen
    public Button reloadButton; // Button to reload the scene
    private Animator pauseAnimator;

    void Start()
    {
        pauseAnimator = pauseMenu.GetComponent<Animator>();
        if (pauseAnimator != null)
        {
            // Set the animator to update even when Time.timeScale is set to 0
            pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
    }

    public void Pause()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = 1.0f;
    }

    public void ToHomeScene()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void ReloadCurrentScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1.0f;
    }
}
