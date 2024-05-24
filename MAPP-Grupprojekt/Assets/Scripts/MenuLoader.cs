using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuLoader : MonoBehaviour
{
   [SerializeField] private GameObject tutorial;
    
    public Animator transition;

    public float transitionTime = 1f;

    public void StartGame()
    {
         SceneManager.LoadSceneAsync(1); // H�r kan vi skriva in vad leveln heter, "Level 1", ELLER siffran p� SceneBuildIndex. N�r man klickar p� Play, ska leveln som st�r k�ras.
    }

    public void PlayGame()
    {
        
        StartCoroutine(LoadLevel(SceneManager.GetActiveScene().buildIndex + 1)); //byter scen till nästa scen i buildsettings index
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
        
        transition.SetTrigger("HideScene");
        
        yield return new WaitForSeconds(transitionTime); //sätter igång ett delay

        SceneManager.LoadScene(levelIndex);//laddar in nästa scen
    }
    }

