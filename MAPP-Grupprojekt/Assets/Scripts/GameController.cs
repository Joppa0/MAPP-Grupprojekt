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
        Player1Move, Player1ChooseWeapon, Player1Throw, Player2Move, Player2ChooseWeapon, Player2Throw, Player1Win, Player2Win, MainMenu // Olika stadier som spelet kan befinna sig i. Tanken är att i varje stadie ska det endast ske det som är menat att ske. Exempel: Player1Move state = spelare 1 ger input för movement.
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

                    
                    yield return new WaitUntil(() => player1Controller.IsMovementComplete || timer.timeRemaining <= 0);  //Ser till att inget händer tills spelaren har rört sig, eller tills timern är slut.
                    player1Controller.IsMovementComplete = true;
                    
                    timer.timerIsRunning = false;

                    weaponMenu.HasChosenWeapon = false;
                    yield return new WaitForSeconds(1f);
                    currentState = BattleState.Player1ChooseWeapon; //Gå till nästa state
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

                    //här lägger vi in logik för kast från spelare 1, och lyssnar på när player 1 har kastat.


                    Debug.Log("Player 1's turn to throw.");
                    timer.timeRemaining = 11f;
                    timer.timerIsRunning = true;
                    StartCoroutine(player1Shooting.StartShoot());


                    yield return new WaitUntil(() => player1Shooting.IsShootingComplete || timer.timeRemaining <= 0);
                    player1Shooting.IsShootingComplete = true;
                    timer.timerIsRunning= false;


                    yield return new WaitForSeconds(1f);

                    player1Controller.GetComponent<SpriteRenderer>().color = inactiveColor;
                    currentState = BattleState.Player2Move; //Går till nästa state
                    break;

                case BattleState.Player2Move:

                    // Här lägger vi in logik för att röra på player 2 och lyssnar på när player 2 har rört på sig.

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

                    //Här lägger vi in logik för kast från spelare 2, och lyssnar på när spelare 2 har kastat.

                    Debug.Log("Player 2's turn to throw.");
                    timer.timeRemaining = 11f;
                    timer.timerIsRunning = true;
                    StartCoroutine(player2Shooting.StartShoot());
                    yield return new WaitUntil(() => player2Shooting.IsShootingComplete || timer.timeRemaining <= 0);
                    player2Shooting.IsShootingComplete = true;
                    timer.timerIsRunning = false;

                    yield return new WaitForSeconds(1f);

                    player2Controller.GetComponent<SpriteRenderer>().color = inactiveColor;
                    currentState = BattleState.Player1Move; // Går tillbaka till steg 1.
                    break;

                case BattleState.Player1Win:

                    // logik för när spelare 1 vinner.
                    player1Controller.GetComponent<SpriteRenderer>().color = activeColor;
                    player2Controller.GetComponent<SpriteRenderer>().color = inactiveColor;

                    Debug.Log("Player 1 wins!");
                    yield return new WaitForSeconds(2f);
                    winningScreen1.Player1wins();
                    yield break; //avslutar courotine

                case BattleState.Player2Win:

                    // logik för när spelare 2 vinner.
                    player2Controller.GetComponent<SpriteRenderer>().color = activeColor;
                    player1Controller.GetComponent<SpriteRenderer>().color = inactiveColor;
                    Debug.Log("Player 2 wins!");

                    yield return new WaitForSeconds(2f);
                    winningScreen2.Player2wins();
                    yield break; //avslutar couotine
                
                case BattleState.MainMenu:
                    //gå till huvudmenyn - används inte för tillfället, kan tas bort ifall ingen användning finns.
                    yield break;

            }

            //titta på score efter varje tur
            CheckScore();
        }
    }

    void CheckScore()
    {
        if (player1Controller.GetComponent<ScoreManager>().GetScore() > 1) 
        {
            currentState = BattleState.Player2Win; //Eftersom komponenten som räknar spelare 1's score ligger på spelare 2 och vice versa, så är den enklaste lösningen att titta på motståndarens score-komponent för att bestämma win-case.
        }
        else if (player2Controller.GetComponent<ScoreManager>().GetScore() > 1)
        {
            currentState = BattleState.Player1Win; //Samma här, spelare 1 winner när score-komponenten på spelare 2 är högre än x. (spelare 2 räknar hur många gånger den blivit träffad, och ger poäng till spelare 1).
        }
    }
    
       
}
