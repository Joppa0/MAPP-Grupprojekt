using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class PauseMenu : MonoBehaviour
{ 
        
    
    
    public GameObject pauseMenu;

    public Button menuButton; // Knappen för att visa menyn
    public Button resumeButton; // Knappen för att fortsätta spelet
    public Button homeButton; // Knappen för att gå till hemskärmen
    public Button reloadButton; // Knappen för att gå ladda om scenen
    private Animator Pauseanimator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Pause()
    {
        // Växlar aktivitetsstatus för vapenmenyn
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = 0.0f;
    }

    public void Resume()
    {
        // Växlar aktivitetsstatus för vapenmenyn
        pauseMenu.SetActive(!pauseMenu.activeSelf);
        Time.timeScale = 1.0f;
    }

    public void ToHomeScene()
    {
        SceneManager.LoadSceneAsync(0);
    }

    public void ReloadCurrentScene()
    {
        // Get the current scene name using the scene manager
        Scene currentScene = SceneManager.GetActiveScene();

        // Load the current scene using the scene name
        SceneManager.LoadScene(currentScene.name);
        Time.timeScale = 1.0f;
    }



}
