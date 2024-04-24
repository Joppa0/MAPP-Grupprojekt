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

    public Color activeColor = new Color(1f, 1f, 1f, 1f); // Red: 255, Green: 255, Blue: 255, Alpha: 255
    public Color inactiveColor = new Color(26f / 255f, 26f / 255f, 26f / 255f, 170f / 255f); // Red: 26, Green: 26, Blue: 26, Alpha: 170
    private SpriteRenderer rend;




    public enum BattleState
    {
        Player1Move, Player1ChooseWeapon, Player1Throw, Player2Move, Player2ChooseWeapon, Player2Throw, Player1Win, Player2Win, MainMenu // Olika stadier som spelet kan befinna sig i. Tanken �r att i varje stadie ska det endast ske det som �r menat att ske. Exempel: Player1Move state = spelare 1 ger input f�r movement.
    }

    // Start is called before the first frame update
    void Start()
    {
        rend = player1Controller.GetComponent<SpriteRenderer>();
        rend = player2Controller.GetComponent<SpriteRenderer>();

        player1Controller.GetComponent<SpriteRenderer>().color = activeColor;
        player2Controller.GetComponent<SpriteRenderer>().color = inactiveColor;

        // Debug logs for active and inactive colors
        Debug.Log("Active Color: " + activeColor);
        Debug.Log("Inactive Color: " + inactiveColor);

        // Set the start state
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

                    player1Controller.GetComponent<SpriteRenderer>().color = activeColor;


                    Debug.Log("Player 1's turn to move.");
                    timer.timeRemaining = 11f;
                    timer.timerIsRunning = true;
                    StartCoroutine(player1Controller.StartMove());

                    
                    yield return new WaitUntil(() => player1Controller.IsMovementComplete || timer.timeRemaining <= 0);  //Ser till att inget h�nder tills spelaren har r�rt sig, eller tills timern �r slut.
                    player1Controller.IsMovementComplete = true;
                    
                    timer.timerIsRunning = false;

                    weaponMenu.HasChosenWeapon = false;
                    yield return new WaitForSeconds(1f);
                    currentState = BattleState.Player1ChooseWeapon; //G� till n�sta state
                    break;


                case BattleState.Player1ChooseWeapon:


                    Debug.Log("Player 1's turn to choose weapon.");
                    timer.timeRemaining = 11f;
                    timer.timerIsRunning = true;
                    weaponMenu.ToggleWeaponMenu();
                    yield return new WaitUntil(() => weaponMenu.HasChosenWeapon || timer.timeRemaining <= 0);
                    timer.timerIsRunning= false;
                    weaponMenu.ToggleWeaponMenu();
                    yield return new WaitForSeconds(1f);
                    currentState = BattleState.Player1Throw;
                    break;

                


                case BattleState.Player1Throw:

                    //h�r l�gger vi in logik f�r kast fr�n spelare 1, och lyssnar p� n�r player 1 har kastat.


                    Debug.Log("Player 1's turn to throw.");
                    timer.timeRemaining = 11f;
                    timer.timerIsRunning = true;
                    StartCoroutine(player1Shooting.StartShoot());


                    yield return new WaitUntil(() => player1Shooting.IsShootingComplete || timer.timeRemaining <= 0);
                    player1Shooting.IsShootingComplete = true;
                    timer.timerIsRunning= false;


                    yield return new WaitForSeconds(1f);

                    player1Controller.GetComponent<SpriteRenderer>().color = inactiveColor;
                    currentState = BattleState.Player2Move; //G�r till n�sta state
                    break;

                case BattleState.Player2Move:

                    // H�r l�gger vi in logik f�r att r�ra p� player 2 och lyssnar p� n�r player 2 har r�rt p� sig.

                    player2Controller.GetComponent<SpriteRenderer>().color = activeColor;
                    

                    Debug.Log("Player 2's turn to move.");
                    timer.timeRemaining = 11f;
                    timer.timerIsRunning = true;
                    StartCoroutine(player2Controller.StartMove());

                    
                    yield return new WaitUntil(() => player2Controller.IsMovementComplete || timer.timeRemaining <= 0);  
                    player2Controller.IsMovementComplete = true;
                    timer.timerIsRunning = false;
                    weaponMenu.HasChosenWeapon = false;


                    yield return new WaitForSeconds(1f);
                    currentState = BattleState.Player2ChooseWeapon; 
                    break;


                case BattleState.Player2ChooseWeapon:


                    Debug.Log("Player 2's turn to choose weapon.");
                    timer.timeRemaining = 11f;
                    timer.timerIsRunning = true;
                    weaponMenu.ToggleWeaponMenu();
                    yield return new WaitUntil(() => weaponMenu.HasChosenWeapon || timer.timeRemaining <= 0);
                    timer.timerIsRunning = false;
                    weaponMenu.ToggleWeaponMenu();
                    yield return new WaitForSeconds(1f);
                    currentState = BattleState.Player2Throw;
                    break;

                case BattleState.Player2Throw:

                    //H�r l�gger vi in logik f�r kast fr�n spelare 2, och lyssnar p� n�r spelare 2 har kastat.

                    Debug.Log("Player 2's turn to throw.");
                    timer.timeRemaining = 11f;
                    timer.timerIsRunning = true;
                    StartCoroutine(player2Shooting.StartShoot());
                    yield return new WaitUntil(() => player2Shooting.IsShootingComplete || timer.timeRemaining <= 0);
                    player2Shooting.IsShootingComplete = true;
                    timer.timerIsRunning = false;

                    yield return new WaitForSeconds(1f);

                    player2Controller.GetComponent<SpriteRenderer>().color = inactiveColor;
                    currentState = BattleState.Player1Move; // G�r tillbaka till steg 1.
                    break;

                case BattleState.Player1Win:

                    // logik f�r n�r spelare 1 vinner.
                    player1Controller.GetComponent<SpriteRenderer>().color = activeColor;
                    player2Controller.GetComponent<SpriteRenderer>().color = inactiveColor;

                    Debug.Log("Player 1 wins!");
                    yield return new WaitForSeconds(2f);
                    winningScreen1.Player1wins();
                    yield break; //avslutar courotine

                case BattleState.Player2Win:

                    // logik f�r n�r spelare 2 vinner.
                    player2Controller.GetComponent<SpriteRenderer>().color = activeColor;
                    player1Controller.GetComponent<SpriteRenderer>().color = inactiveColor;
                    Debug.Log("Player 2 wins!");

                    yield return new WaitForSeconds(2f);
                    winningScreen2.Player2wins();
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
        if (player1Controller.GetComponent<ScoreManager>().GetScore() > 1) 
        {
            currentState = BattleState.Player2Win; //Eftersom komponenten som r�knar spelare 1's score ligger p� spelare 2 och vice versa, s� �r den enklaste l�sningen att titta p� motst�ndarens score-komponent f�r att best�mma win-case.
        }
        else if (player2Controller.GetComponent<ScoreManager>().GetScore() > 1)
        {
            currentState = BattleState.Player1Win; //Samma h�r, spelare 1 winner n�r score-komponenten p� spelare 2 �r h�gre �n x. (spelare 2 r�knar hur m�nga g�nger den blivit tr�ffad, och ger po�ng till spelare 1).
        }
    }
    
       
}
