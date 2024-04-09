using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManeger : MonoBehaviour
{
    private int snowballStrikes = 0;

    [SerializeField] private TMP_Text scoreText;

    private void Start()
    {
        scoreText.text = "" + snowballStrikes;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Snowball"))
        {
            Destroy(other.gameObject);
            snowballStrikes++;
            scoreText.text = "" + snowballStrikes;
        }
    }
}
