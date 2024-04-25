using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
// using static UnityEditor.FilePathAttribute;

public class PlayerController : MonoBehaviour
{
    public bool IsMovementComplete { get; set; }

    private Vector2 target;
    private Vector3 lastPosition;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float distanceToStop = 0.1f;
    [SerializeField] private float rayDistance = 0.25f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float calculatedDistance = 10;

    private bool hasTarget;

    private void FixedUpdate()
    {
        Move();
    }

    private bool CanMove()
    {
        if (target.x < transform.position.x)
        {
            RaycastHit2D leftHit = Physics2D.Raycast(transform.position, Vector2.right, rayDistance, groundLayer);
            return leftHit.collider == null;
        }
        else
        {
            RaycastHit2D rightHit = Physics2D.Raycast(transform.position, Vector2.left, rayDistance, groundLayer);
            return rightHit.collider == null;
        }
    }

    private void Move()
    {
        if (!IsMovementComplete && hasTarget)
        {
            lastPosition = transform.position;
            // Moves toward target.
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, transform.position.y), Time.deltaTime * speed);

            // Stops moving if the target has been reached or is close enough.
            if (Mathf.Abs(transform.position.x - target.x) <= distanceToStop || !CanMove())
            {
                // Sets hasTarget to false, since it would otherwise make the player move indefinitely.
                hasTarget = false;

                // Tells GameController that movement is complete, meaning the state machine can change states.
                IsMovementComplete = true;
            }
        }
    }

    private IEnumerator SetMoveTarget()
    {
        // Continues until player has chosen where to move.
        bool done = false;
        while (!done && !IsMovementComplete)
        {
            // Checks if player has touched the screen.
            if (Input.touchCount > 0)
            {
                // Gets touch position.
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    // Gets the touch position in world coordinates and sets it as the target to move toward.
                    target = Camera.main.ScreenToWorldPoint(touch.position);

                    hasTarget = true;

                    // Stops loop.
                    done = true;
                }
            }

            // Works the same way as touch, but with the mouse. Used for debugging.
            else if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);

                hasTarget = true;

                done = true;
            }
            yield return null;
        }

        //Limited movement, right
        if (target.x > transform.position.x)
        {
            if (Mathf.Abs(transform.position.x - target.x) > calculatedDistance)
            {
                target.x = transform.position.x + calculatedDistance;
            }
        }

        //Limited movement, left
        if (target.x < transform.position.x)
        {
            if (Mathf.Abs(transform.position.x - target.x) > calculatedDistance)
            {
                target.x = transform.position.x - calculatedDistance;
            }
        }

    }

    // Initiates movement.
    public IEnumerator StartMove()
    {
        IsMovementComplete = false;

        StartCoroutine(SetMoveTarget());

        // Waits until the target position has been found.
        yield return new WaitUntil(() => hasTarget);
    }
}
