using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameController;
using UnityEngine.SceneManagement;
using UnityEngine.UI;



public class WinningScreen : MonoBehaviour
{
    public GameObject winningScreen;
    public GameObject winningScreen2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Player1wins()
    {
        winningScreen.SetActive(!winningScreen.activeSelf);
    }

    public void Player2wins()
    {
        winningScreen2.SetActive(!winningScreen2.activeSelf);
    }




}
