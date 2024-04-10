using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 

public class GameController : MonoBehaviour
{

    public static BattleState currentState;


    public PlayerController player1;
    public PlayerController player2;







    public enum BattleState
    {
        Player1Move, Player1Throw, Player2Move, Player2Throw,Player1Win, Player2Win // Olika stadier som spelet kan befinna sig i. Tanken är att i varje stadie ska det endast ske det som är menat att ske. Exempel: Player1Move state = spelare 1 ger input för movement.
    }

    // Start is called before the first frame update
    void Start()
    {
        // sätter start state
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

                    
                    StartCoroutine(player1.SetMoveTarget());

                    Debug.Log("Player 1's turn to move.");
                    yield return new WaitUntil(() => player1.IsMovementComplete);
                    currentState = BattleState.Player1Throw; //Gå till nästa state
                    break;
                
                case BattleState.Player1Throw:

                    //här lägger vi in logik för kast från spelare 1, och lyssnar på när player 1 har kastat.

                    Debug.Log("Player 1's turn to throw.");
                    yield return new WaitForSeconds(2f);
                    currentState = BattleState.Player2Move; //Går till nästa state
                    break;

                case BattleState.Player2Move:

                    // Här lägger vi in logik för att röra på player 2 och lyssnar på när player 2 har rört på sig.

                    StartCoroutine(player2.SetMoveTarget());

                    Debug.Log("Player 2's turn to move.");
                    yield return new WaitUntil(() => player2.IsMovementComplete);
                    currentState = BattleState.Player2Throw; 
                    break;

                case BattleState.Player2Throw:

                    //Här lägger vi in logik för kast från spelare 2, och lyssnar på när spelare 2 har kastat.

                    Debug.Log("Player 2's turn to throw.");
                    yield return new WaitForSeconds(2f);
                    currentState = BattleState.Player1Move; // Går tillbaka till steg 1.
                    break;

                case BattleState.Player1Win:

                    // logik för när spelare 1 vinner.

                    Debug.Log("Player 1 wins!");
                    yield break; //avslutar courotine

                case BattleState.Player2Win:

                    // logik för när spelare 2 vinner.

                    Debug.Log("Player 2 wins!");
                    yield break; //avslutar couotine



            }
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
