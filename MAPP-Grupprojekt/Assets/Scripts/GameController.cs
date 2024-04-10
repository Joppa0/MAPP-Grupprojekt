using System.Collections;
using System.Collections.Generic;
using UnityEngine;


 

public class GameController : MonoBehaviour
{

    public static BattleState currentState;


    public enum BattleState
    {
        Player1Move, Player1Throw, Player2Move, Player2Throw,Player1Win, Player2Win // Olika stadier som spelet kan befinna sig i. Tanken �r att i varje stadie ska det endast ske det som �r menat att ske. Exempel: Player1Move state = spelare 1 ger input f�r movement.
    }

    // Start is called before the first frame update
    void Start()
    {
        // s�tter start state
        currentState = BattleState.Player1Move;

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
