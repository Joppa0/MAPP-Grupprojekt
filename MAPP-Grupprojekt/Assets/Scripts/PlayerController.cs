using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public bool IsMovementComplete { get; private set; }

    private float target;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float distanceToStop = 0.1f;

    private bool canMove;
    

    // Update is called once per frame
    void Update()
    {
        //StartCoroutine(SetMoveTarget());
    }

    private void FixedUpdate()
    {
        Move();
    }

    public IEnumerator SetMoveTarget()
    {
        IsMovementComplete = false;

        bool done = false;
        while (!done)
        {
            // Checks if player has touched the screen.
            if (Input.touchCount > 0)
            {
                // Makes player able to move.
                canMove = true;

                // Gets touch position.
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = touch.position;

                // Finds target position to move to.
                target = Camera.main.ScreenToWorldPoint(touchPos).x;

                done = true;
            }

            // Works the same way as touch, but with the mouse. Used for debugging.
            else if (Input.GetMouseButton(0))
            {
                //canMove = true;

                Vector2 pos = Input.mousePosition;

                target = Camera.main.ScreenToWorldPoint(pos).x;

                done = true;
            }
            yield return null;
        }
    }

    private void Move()
    {
        if (!IsMovementComplete)
        {
            // Moves toward target.
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target, transform.position.y), Time.deltaTime * speed);

            // Stops moving if the target has been reached.
            if (Mathf.Abs(transform.position.x - target) <= distanceToStop)
            {
                // canMove = false;

                IsMovementComplete = true;

                //if (GameController.currentState == GameController.BattleState.Player1Move)
                //    GameController.currentState = GameController.BattleState.Player1Throw;

                //else if (GameController.currentState == GameController.BattleState.Player2Move)
                //    GameController.currentState = GameController.BattleState.Player2Throw;
            }
        }
    }
}
