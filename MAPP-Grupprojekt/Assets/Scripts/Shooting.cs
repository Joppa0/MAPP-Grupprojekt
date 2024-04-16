using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shooting : MonoBehaviour
{
    public enum Snowballs
    {
        Snowball, HeatSeeking, SnowShovel
    }

    public bool IsMovementComplete { get; protected set; }
    public bool IsShootingComplete { get; protected set; }
    public Snowball equippedSnowball;

    protected Vector2 target;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected float speed = 1.0f;
    [SerializeField] protected float distanceToStop = 0.1f;
    [SerializeField] protected float power = 20f;
    [SerializeField] private Vector2 minPower, maxPower;

    protected bool hasTarget;

    protected IEnumerator SetShootTarget()
    {
        Vector3 startPoint = Vector3.zero, endPoint;

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
                    // Sets start point for drag action.
                    startPoint = Camera.main.ScreenToWorldPoint(touch.position);
                    startPoint.z = 15;
                }

                else if (touch.phase == TouchPhase.Ended && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    // Sets end point for drag action.
                    endPoint = Camera.main.ScreenToWorldPoint(touch.position);
                    endPoint.z = 15;

                    /* 
                     * Sets the shoot target to the opposite vector of the start and end points.
                     * Vector is clamped so the shot has a maximum and minimum force.
                     */
                    target = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));

                    // Signals the target has been found.
                    hasTarget = true;

                    // Ends the loop.
                    done = true;
                }
            }

            // Works the same way as touch, but with the mouse. Used for debugging.
            else if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                startPoint.z = 15;
            }

            else if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 15;

                target = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));

                hasTarget = true;

                done = true;
            }
            yield return null;
        }

    }

    // Initiates shooting.
    public IEnumerator StartShoot()
    {
        IsShootingComplete = false;

        StartCoroutine(SetShootTarget());

        yield return new WaitUntil(() => hasTarget);

        Shoot();
    }

    // Fires a bullet in toward the target.
    protected virtual void Shoot()
    {
        equippedSnowball.Shoot(target, transform.position);

        // Resets target bool.
        hasTarget = false;

        // Tells the GameController that shooting is complete.
        IsShootingComplete = true;
    }

    public void SetEquippedSnowball(Snowballs s)
    {
        switch (s)
        {
            case Snowballs.Snowball:
                equippedSnowball = GetComponent<Snowball>();
                break;
            case Snowballs.HeatSeeking:
                equippedSnowball = GetComponent<SnowballHeatSeekPrefab>();
                break;
            case Snowballs.SnowShovel:
                equippedSnowball = GetComponent<SnowShovel>();
                break;
        }
    }
}
