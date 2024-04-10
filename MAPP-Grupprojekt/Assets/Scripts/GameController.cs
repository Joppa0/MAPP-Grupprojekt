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
        Player1Move, Player1Throw, Player2Move, Player2Throw,Player1Win, Player2Win // Olika stadier som spelet kan befinna sig i. Tanken �r att i varje stadie ska det endast ske det som �r menat att ske. Exempel: Player1Move state = spelare 1 ger input f�r movement.
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

                    
                    StartCoroutine(player1.SetMoveTarget());

                    Debug.Log("Player 1's turn to move.");
                    yield return new WaitUntil(() => player1.IsMovementComplete);
                    currentState = BattleState.Player1Throw; //G� till n�sta state
                    break;
                
                case BattleState.Player1Throw:

                    //h�r l�gger vi in logik f�r kast fr�n spelare 1, och lyssnar p� n�r player 1 har kastat.

                    Debug.Log("Player 1's turn to throw.");
                    yield return new WaitForSeconds(2f);
                    currentState = BattleState.Player2Move; //G�r till n�sta state
                    break;

                case BattleState.Player2Move:

                    // H�r l�gger vi in logik f�r att r�ra p� player 2 och lyssnar p� n�r player 2 har r�rt p� sig.

                    StartCoroutine(player2.SetMoveTarget());

                    Debug.Log("Player 2's turn to move.");
                    yield return new WaitUntil(() => player2.IsMovementComplete);
                    currentState = BattleState.Player2Throw; 
                    break;

                case BattleState.Player2Throw:

                    //H�r l�gger vi in logik f�r kast fr�n spelare 2, och lyssnar p� n�r spelare 2 har kastat.

                    Debug.Log("Player 2's turn to throw.");
                    yield return new WaitForSeconds(2f);
                    currentState = BattleState.Player1Move; // G�r tillbaka till steg 1.
                    break;

                case BattleState.Player1Win:

                    // logik f�r n�r spelare 1 vinner.

                    Debug.Log("Player 1 wins!");
                    yield break; //avslutar courotine

                case BattleState.Player2Win:

                    // logik f�r n�r spelare 2 vinner.

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
