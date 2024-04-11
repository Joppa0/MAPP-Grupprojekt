using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class ScoreManeger : MonoBehaviour
{
    private int snowballStrikes = 0;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private int winningStrikes = 10;
    [SerializeField] private int levelToLoad;

    private void Start()
    {
        scoreText.text = "" + snowballStrikes;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Snowball"))
        {
            
            snowballStrikes++;
            scoreText.text = "" + snowballStrikes;
        }
    }

    private void Update()
    {
        if(snowballStrikes == winningStrikes)
        {
            Invoke("LoadNextLevel", 2f);
        }
    }

    private void LoadNextLevel()
    {
        SceneManager.LoadScene(levelToLoad);
    }
}
