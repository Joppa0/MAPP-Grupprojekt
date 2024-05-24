using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;




public class GameController : MonoBehaviour
{

    public static BattleState currentState;

    public WinningScreen winningScreen1;
    public WinningScreen winningScreen2;
    public PlayerController player1Controller;
    public PlayerController player2Controller;
    public Shooting player1Shooting;
    public Shooting player2Shooting;
    public ScoreManager scoreManager;
    public Timer timer;
    public WeaponMenu weaponMenu;
    public ShowActionController action;

    public enum BattleState
    {
        Player1Move, Player1ChooseWeapon, Player1Throw, Player2Move, Player2ChooseWeapon, Player2Throw, Player1Win, Player2Win, // Olika stadier som spelet kan befinna sig i. Tanken är att i varje stadie ska det endast ske det som är menat att ske. Exempel: Player1Move state = spelare 1 ger input för movement.
    }

    // Start is called before the first frame update
    void Start()
    {
        // Set the start state
        currentState = BattleState.Player1Move;
        StartCoroutine(ProcessState());
    }

    IEnumerator ProcessState()
    {
        while (true)
        {
            //titta på score innan varje tur
            CheckScore();

            switch (currentState)
            {
                case BattleState.Player1Move:

                    action.P1Move();

                    Debug.Log("Player 1's turn to move.");
                    timer.timeRemaining = 11f;
                    timer.timerIsRunning = true;
                    StartCoroutine(player1Controller.StartMove());


                    yield return new WaitUntil(() => player1Controller.IsMovementComplete || timer.timeRemaining <= 0);  //Ser till att inget händer tills spelaren har rört sig, eller tills timern är slut.
                    player1Controller.IsMovementComplete = true;
                    action.P1Move();
                    timer.timerIsRunning = false;
                    weaponMenu.HasChosenWeapon = false;
                    currentState = BattleState.Player1ChooseWeapon; //Gå till nästa state
                    break;


                case BattleState.Player1ChooseWeapon:


                    Debug.Log("Player 1's turn to choose weapon.");
                    timer.timeRemaining = 11f;
                    timer.timerIsRunning = true;
                    weaponMenu.ToggleWeaponMenu();
                    action.P1ChooseWeapon();
                    yield return new WaitUntil(() => weaponMenu.HasChosenWeapon || timer.timeRemaining <= 0);
                    timer.timerIsRunning = false;
                    weaponMenu.ToggleWeaponMenu();
                    action.P1ChooseWeapon();
                    currentState = BattleState.Player1Throw;
                    break;




                case BattleState.Player1Throw:

                    //här lägger vi in logik för kast från spelare 1, och lyssnar på när player 1 har kastat.

                    action.P1Shoot();
                    Debug.Log("Player 1's turn to throw.");
                    timer.timeRemaining = 11f;
                    timer.timerIsRunning = true;
                    StartCoroutine(player1Shooting.StartShoot());
                    yield return new WaitUntil(() => player1Shooting.IsShootingComplete || timer.timeRemaining <= 0);
                    player1Shooting.IsShootingComplete = true;
                    timer.timerIsRunning = false;
                    action.P1Shoot();

                    currentState = BattleState.Player2Move; //Går till nästa state
                    break;

                case BattleState.Player2Move:

                    action.P2Move();

                    Debug.Log("Player 2's turn to move.");
                    timer.timeRemaining = 11f;
                    timer.timerIsRunning = true;
                    StartCoroutine(player2Controller.StartMove());
                    yield return new WaitUntil(() => player2Controller.IsMovementComplete || timer.timeRemaining <= 0);
                    player2Controller.IsMovementComplete = true;
                    action.P2Move();

                    timer.timerIsRunning = false;
                    weaponMenu.HasChosenWeapon = false;
                    currentState = BattleState.Player2ChooseWeapon;
                    break;


                case BattleState.Player2ChooseWeapon:


                    Debug.Log("Player 2's turn to choose weapon.");
                    timer.timeRemaining = 11f;
                    timer.timerIsRunning = true;
                    weaponMenu.ToggleWeaponMenu();
                    action.P2ChooseWeapon();

                    yield return new WaitUntil(() => weaponMenu.HasChosenWeapon || timer.timeRemaining <= 0);
                    timer.timerIsRunning = false;
                    weaponMenu.ToggleWeaponMenu();
                    action.P2ChooseWeapon();

                    currentState = BattleState.Player2Throw;
                    break;

                case BattleState.Player2Throw:

                    //Här lägger vi in logik för kast från spelare 2, och lyssnar på när spelare 2 har kastat.
                    action.P2Shoot();

                    Debug.Log("Player 2's turn to throw.");
                    timer.timeRemaining = 11f;
                    timer.timerIsRunning = true;
                    StartCoroutine(player2Shooting.StartShoot());
                    yield return new WaitUntil(() => player2Shooting.IsShootingComplete || timer.timeRemaining <= 0);
                    player2Shooting.IsShootingComplete = true;
                    timer.timerIsRunning = false;

                    action.P2Shoot();

                    currentState = BattleState.Player1Move; // Går tillbaka till steg 1.
                    break;

                case BattleState.Player1Win:

                    // logik för när spelare 1 vinner.


                    Debug.Log("Player 1 wins!");
                    yield return new WaitForSeconds(2f);
                    winningScreen1.Player1wins();
                    yield break; //avslutar courotine

                case BattleState.Player2Win:

                    // logik för när spelare 2 vinner.


                    Debug.Log("Player 2 wins!");
                    yield return new WaitForSeconds(2f);
                    winningScreen2.Player2wins();
                    yield break; //avslutar couotine



            }


        }
    }

    void CheckScore()
    {
        if (player1Controller.GetComponent<ScoreManager>().GetScore() >= 5)
        {
            currentState = BattleState.Player2Win; //Eftersom komponenten som räknar spelare 1's score ligger på spelare 2 och vice versa, så är den enklaste lösningen att titta på motståndarens score-komponent för att bestämma win-case.
        }
        else if (player2Controller.GetComponent<ScoreManager>().GetScore() >= 5)
        {
            currentState = BattleState.Player1Win; //Samma här, spelare 1 winner när score-komponenten på spelare 2 är högre än x. (spelare 2 räknar hur många gånger den blivit träffad, och ger poäng till spelare 1).
        }
    }


}
