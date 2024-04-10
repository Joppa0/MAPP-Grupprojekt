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
        SetTarget();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void SetTarget()
    {
        if (Input.touchCount > 0)
        {
            // Makes player able to move.
            canMove = true;

            // Gets touch position.
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = touch.position;

            // Finds target position to move to.
            target = Camera.main.ScreenToWorldPoint(touchPos).x;
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
