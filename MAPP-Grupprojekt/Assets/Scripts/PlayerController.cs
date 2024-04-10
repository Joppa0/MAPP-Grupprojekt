using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private float target;
    [SerializeField] private float speed = 1.0f;

    private bool canMove;

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(SetTarget());
    }

    private void FixedUpdate()
    {
        Move();
    }

    public IEnumerator SetTarget()
    {
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
                canMove = true;

                Vector2 pos = Input.mousePosition;

                target = Camera.main.ScreenToWorldPoint(pos).x;

                done = true;
            }
            yield return null;
        }
    }

    private void Move()
    {
        if (canMove)
        {
            // Moves toward target.
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target, transform.position.y), Time.deltaTime * speed);

            // Stops moving if the target has been reached.
            if (transform.position.x == target)
            {
                canMove = false;
            }
        }
    }
}
