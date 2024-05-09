using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    private int snowballStrikes = 0;

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private VibrationController vibrationController;
    private Animator anim;
    

    private void Start()
    {
        scoreText.text = "" + snowballStrikes;
        anim = GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Snowball"))
        {
            snowballStrikes++;
            scoreText.text = "" + snowballStrikes;
            #if UNITY_ANDROID || UNITY_IOS
            vibrationController.HeavyVibration();
#endif
            anim.SetTrigger("Hit");
        }
    }

    public int GetScore()
    {
        return snowballStrikes;
    }


}
