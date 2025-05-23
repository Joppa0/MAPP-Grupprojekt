using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float timeRemaining = 10;
    public bool timerIsRunning = false;
    public Text timerText;

    private void Start(){
        timerIsRunning = true;
    }

   
    void Update()
    {
        if (timerIsRunning){
            if (timeRemaining > 0){
                timeRemaining -= Time.deltaTime; //kollar hur mycket tid som finns
            }
            else{
                Debug.Log("Time has run out!");
                timeRemaining = 0;
                timerIsRunning = false; //visar att timern har tagit slut
            }
        }
        DisplayTime(timeRemaining);
    }

    void DisplayTime(float timeToDisplay){
            

        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds); //visar timern

    }
}
