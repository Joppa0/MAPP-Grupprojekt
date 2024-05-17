using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shooting : MonoBehaviour
{
    public enum Snowballs
    {
        Snowball, HeatSeeking, SnowShovel
    }

    public bool IsShootingComplete { get; set; }
    public Snowball equippedSnowball;

    public Vector3 target;
    private SightLine sightLine;
    private Animator anim;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Vector2 minPower, maxPower;

    private bool hasTarget;
    private Timer timer;

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

    private void Start()
    {
        equippedSnowball = GetComponent<Snowball>();
        sightLine = GetComponent<SightLine>();
        anim = GetComponent<Animator>();
        timer = GameObject.Find("GameController").GetComponent<Timer>();
    }

    // Initiates shooting.
    public IEnumerator StartShoot()
    {
        IsShootingComplete = false;

        StartCoroutine(SetShootTarget());

        yield return new WaitUntil(() => hasTarget);

        Shoot();

        // Tells the GameController that shooting is complete.
        IsShootingComplete = true;
    }

    private IEnumerator SetShootTarget()
    {
        if (IsShootingComplete)
        {
            yield break;
        }

        Vector3 startPoint = Vector3.zero, endPoint = Vector3.zero;
        bool done = false;

        while (!done)
        {
            if (timer.timeRemaining <= 0)
            {
                done = true;
                IsShootingComplete = true;
                sightLine.EndLine();
            }

            // Checks if player has touched the screen.
            if (Input.touchCount > 0)
            {
                // Gets touch position.
                Touch touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    // Sets start point for drag action.
                    startPoint = GetWorldPoint(touch.position);

                    sightLine.StartLine();
                }

                else if (touch.phase == TouchPhase.Moved && startPoint.z == 15)
                {
                    // Sets end point for drag action.
                    endPoint = GetWorldPoint(touch.position);

                    SetTargetPosition(startPoint, endPoint);

                    sightLine.UpdateLineRenderer(target, equippedSnowball);
                }

                else if (touch.phase == TouchPhase.Ended && !EventSystem.current.IsPointerOverGameObject(touch.fingerId) && startPoint.z == 15)
                {
                    // Sets end point for drag action.
                    endPoint = GetWorldPoint(touch.position);

                    SetTargetPosition(startPoint, endPoint);

                    // Signals the target has been found.
                    hasTarget = true;

                    done = true;

                    sightLine.EndLine();
                }
            }

            // Works the same way as touch, but with the mouse. Used for debugging.
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                startPoint = GetWorldPoint(Input.mousePosition);

                sightLine.StartLine();
            }

            else if (Input.GetMouseButton(0) && startPoint.z == 15)
            {
                endPoint = GetWorldPoint(Input.mousePosition);

                SetTargetPosition(startPoint, endPoint);

                sightLine.UpdateLineRenderer(target, equippedSnowball);
            }

            else if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject() && startPoint.z == 15)
            {
                GetWorldPoint(Input.mousePosition);

                SetTargetPosition(startPoint, endPoint);

                hasTarget = true;

                done = true;

                sightLine.EndLine();
            }
            yield return null;
        }
    }

    private Vector3 GetWorldPoint(Vector2 position)
    {
        Vector3 point = Camera.main.ScreenToWorldPoint(position);
        point.z = 15;
        return point;
    }

    private void SetTargetPosition(Vector3 startPoint, Vector3 endPoint)
    {
        /* 
         * Sets the shoot target to the opposite vector of the start and end points.
         * Vector is clamped so the shot has a maximum and minimum force.
         */
        target = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));
    }

    // Fires a snowball in toward the target.
    private void Shoot()
    {
        anim.SetTrigger("Throw");

        equippedSnowball.Shoot(target, transform.position);

        // Resets target bool.
        hasTarget = false;
    }
}
