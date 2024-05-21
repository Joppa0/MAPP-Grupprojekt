using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    //[SerializeField] private int levelToLoad;
    [SerializeField] private GameObject credits;
    [SerializeField] private GameObject tutorial;

    public Animator transition;

    public float transitionTime = 0.5f;


    public void StartGame()
    {
         SceneManager.LoadSceneAsync(1); // H�r kan vi skriva in vad leveln heter, "Level 1", ELLER siffran p� SceneBuildIndex. N�r man klickar p� Play, ska leveln som st�r k�ras.
    }

    public void PlayGame()
    {
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex +1));
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ShowTutorial()
    {
        tutorial.SetActive(true);
    }

    public void CloseTutorial()
    {
        tutorial.SetActive(false);
    }


    IEnumerator LoadLevel(int levelIndex){
        
        transition.SetTrigger("Start");
        
        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(levelIndex);

    }


     public void ShowCredits()
    {
        credits.SetActive(true);
    }

    public void CloseCredits()
    {
        credits.SetActive(false);

    }
   
}
