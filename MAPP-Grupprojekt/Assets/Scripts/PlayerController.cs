using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{   
    public bool IsMovementComplete { get; private set; }
    public bool IsShootingComplete { get; private set; }

    private Vector2 target;
    [SerializeField] private GameObject bullet;
    [SerializeField] private float speed = 1.0f;
    [SerializeField] private float distanceToStop = 0.1f;

    private bool hasTarget;

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        if (!IsMovementComplete && hasTarget)
        {
            // Moves toward target.
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, transform.position.y), Time.deltaTime * speed);

            // Stops moving if the target has been reached or is close enough.
            if (Mathf.Abs(transform.position.x - target.x) <= distanceToStop)
            {
                // Sets hasTarget to false, since it would otherwise make the player move indefinitely.
                hasTarget = false;

                // Tells GameController that movement is complete, meaning the state machine can change states.
                IsMovementComplete = true;
            }
        }
    }

    private IEnumerator SetTarget()
    {
        bool done = false;
        while (!done)
        {
            // Checks if player has touched the screen.
            if (Input.touchCount > 0)
            {
                // Gets touch position.
                Touch touch = Input.GetTouch(0);
                
                if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    Vector2 touchPos = touch.position;

                    // Finds target position to move to.
                    target = Camera.main.ScreenToWorldPoint(touchPos);

                    hasTarget = true;

                    done = true;
                }
            }

            // Works the same way as touch, but with the mouse. Used for debugging.
            else if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                Vector2 pos = Input.mousePosition;

                target = Camera.main.ScreenToWorldPoint(pos);

                hasTarget = true;

                done = true;
            }
            yield return null;
        }
    }

    // Initiates movement.
    public IEnumerator StartMove()
    {
        IsMovementComplete = false;

        StartCoroutine(SetTarget());

        yield return new WaitUntil(() => hasTarget);
    }

    // Initiates shooting.
    public IEnumerator StartShoot()
    {
        IsShootingComplete = false;

        StartCoroutine(SetTarget());

        yield return new WaitUntil(() => hasTarget);

        Shoot();
    }


    // Fires a bullet in toward the target.
    private void Shoot()
    {
        // Gets the direction to aim in.
        Vector2 aimPos = target - new Vector2(transform.position.x, transform.position.y);

        // Calculates the rotation in degrees.
        float rotation = Mathf.Atan2(-aimPos.x, aimPos.y) * Mathf.Rad2Deg;

        // Spawns a new bullet with the desired rotation.
        Instantiate(bullet, transform.position + Vector3.Normalize(aimPos) * 1.5f, Quaternion.Euler(0, 0, rotation));

        hasTarget = false;
        IsShootingComplete = true;
    }
}
