using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public bool IsMovementComplete { get; private set; } = true;

    private float target;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float distanceToStop = 0.1f;

    private bool hasTarget;

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
                // Gets touch position.
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = touch.position;

                // Finds target position to move to.
                target = Camera.main.ScreenToWorldPoint(touchPos).x;

                hasTarget = true;

                done = true;
            }

            // Works the same way as touch, but with the mouse. Used for debugging.
            else if (Input.GetMouseButton(0))
            {
                Vector2 pos = Input.mousePosition;

                target = Camera.main.ScreenToWorldPoint(pos).x;

                hasTarget = true;

                done = true;
            }
            yield return null;
        }
    }

    private void Move()
    {
        if (hasTarget)
        {
            // Moves toward target.
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target, transform.position.y), Time.deltaTime * speed);

            // Stops moving if the target has been reached or is close enough.
            if (Mathf.Abs(transform.position.x - target) <= distanceToStop)
            {
                // Sets hasTarget to false, since it would otherwise make the player move indefinitely.
                hasTarget = false;

                // Tells GameController that movement is complete, meaning the state machine can change states.
                IsMovementComplete = true;
            }
        }
    }
}
