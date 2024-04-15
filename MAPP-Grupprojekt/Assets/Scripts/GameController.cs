using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class GameController : MonoBehaviour
{

    public static BattleState currentState;

    public PlayerController player1;
    public PlayerController player2;

    public ScoreManager scoreManager;
    public Timer timer;





    public enum BattleState
    {
        Player1Move, Player1ChooseWeapon, Player1Throw, Player2Move, Player2Throw,Player1Win, Player2Win, MainMenu // Olika stadier som spelet kan befinna sig i. Tanken �r att i varje stadie ska det endast ske det som �r menat att ske. Exempel: Player1Move state = spelare 1 ger input f�r movement.
    }

    // Start is called before the first frame update
    void Start()
    {
        // s�tter start state
        currentState = BattleState.Player1Move;
        StartCoroutine(ProcessState());

    }

    IEnumerator ProcessState()
    {
        while (true)
        {
            switch (currentState)
            {
                case BattleState.Player1Move:



                    Debug.Log("Player 1's turn to move.");
                    timer.timeRemaining = 10f;
                    timer.timerIsRunning = true;
                    StartCoroutine(player1.StartMove());

                    
                    yield return new WaitUntil(() => player1.IsMovementComplete || timer.timeRemaining <= 0);  //Ser till att inget h�nder tills spelaren har r�rt sig, eller tills timern �r slut.
                    timer.timerIsRunning = false;

                    yield return new WaitForSeconds(1f);
                    currentState = BattleState.Player1Throw; //G� till n�sta state
                    break;


                case BattleState.Player1ChooseWeapon:


                    Debug.Log("Player 1's turn to choose weapon.");
                    timer.timeRemaining = 10f;
                    timer.timerIsRunning = true;
                    //StartCouroutine()
                
                case BattleState.Player1Throw:

                    //h�r l�gger vi in logik f�r kast fr�n spelare 1, och lyssnar p� n�r player 1 har kastat.


                    Debug.Log("Player 1's turn to throw.");
                    timer.timeRemaining = 10f;
                    timer.timerIsRunning = true;
                    StartCoroutine(player1.StartShoot());


                    yield return new WaitUntil(() => player1.IsShootingComplete || timer.timeRemaining <= 0);
                    timer.timerIsRunning= false;


                    yield return new WaitForSeconds(1f);
                    currentState = BattleState.Player2Move; //G�r till n�sta state
                    break;

                case BattleState.Player2Move:

                    // H�r l�gger vi in logik f�r att r�ra p� player 2 och lyssnar p� n�r player 2 har r�rt p� sig.


                    Debug.Log("Player 2's turn to move.");
                    timer.timeRemaining = 10f;
                    timer.timerIsRunning = true;
                    StartCoroutine(player2.StartMove());

                    
                    yield return new WaitUntil(() => player2.IsMovementComplete || timer.timeRemaining <= 0);  
                    timer.timerIsRunning = false;
                    yield return new WaitForSeconds(1f);
                    currentState = BattleState.Player2Throw; 
                    break;

                case BattleState.Player2Throw:

                    //H�r l�gger vi in logik f�r kast fr�n spelare 2, och lyssnar p� n�r spelare 2 har kastat.

                    Debug.Log("Player 2's turn to throw.");
                    timer.timeRemaining = 10f;
                    timer.timerIsRunning = true;
                    StartCoroutine(player2.StartShoot());
                    yield return new WaitUntil(() => player2.IsShootingComplete || timer.timeRemaining <= 0);
                    timer.timerIsRunning = false;

                    yield return new WaitForSeconds(1f);
                    currentState = BattleState.Player1Move; // G�r tillbaka till steg 1.
                    break;

                case BattleState.Player1Win:

                    // logik f�r n�r spelare 1 vinner.

                    Debug.Log("Player 1 wins!");
                    yield return new WaitForSeconds(2f);
                    SceneManager.LoadSceneAsync(0);
                    yield break; //avslutar courotine

                case BattleState.Player2Win:

                    // logik f�r n�r spelare 2 vinner.

                    Debug.Log("Player 2 wins!");

                    yield return new WaitForSeconds(2f);
                    SceneManager.LoadSceneAsync(0);
                    yield break; //avslutar couotine
                
                case BattleState.MainMenu:
                    //g� till huvudmenyn - anv�nds inte f�r tillf�llet, kan tas bort ifall ingen anv�ndning finns.
                    yield break;

            }

            //titta p� score efter varje tur
            CheckScore();
        }
    }

    void CheckScore()
    {
        if (player1.GetComponent<ScoreManager>().GetScore() > 1) 
        {
            currentState = BattleState.Player2Win; //Eftersom komponenten som r�knar spelare 1's score ligger p� spelare 2 och vice versa, s� �r den enklaste l�sningen att titta p� motst�ndarens score-komponent f�r att best�mma win-case.
        }
        else if (player2.GetComponent<ScoreManager>().GetScore() > 1)
        {
            currentState = BattleState.Player1Win; //Samma h�r, spelare 1 winner n�r score-komponenten p� spelare 2 �r h�gre �n x. (spelare 2 r�knar hur m�nga g�nger den blivit tr�ffad, och ger po�ng till spelare 1).
        }
    }
    
       
}
