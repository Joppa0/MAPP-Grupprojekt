using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public class Shooting : MonoBehaviour
{
    // Saves the different types of snowballs that can be equipped.
    public enum Snowballs
    {
        Snowball, HeatSeeking, SnowShovel
    }

    public bool IsShootingComplete { get; set; }
    public bool HasSnowballLanded {  get; set; }

    public Snowball equippedSnowball;

    private Vector3 target;
    private SightLine sightLine;
    private Animator anim;

    [SerializeField] private GameObject bullet;
    [SerializeField] private Vector2 minPower, maxPower;
    [SerializeField] private AudioClip[] throwSounds;

    private bool hasTarget;
    private Timer timer;

    // Changes the equipped snowball based on the value of the enum parameter.
    public void SetEquippedSnowball(Snowballs s)
    {
        switch (s)
        {
            case Snowballs.Snowball:
                equippedSnowball = GetComponent<Snowball>();
                break;
            case Snowballs.HeatSeeking:
                equippedSnowball = GetComponent<HeatSeekingSnowball>();
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
        HasSnowballLanded = false;

        StartCoroutine(SetShootTarget());

        yield return new WaitUntil(() => hasTarget);

        Shoot();

        // Wait until the snowball has landed.
        yield return new WaitUntil(() => HasSnowballLanded);

        // Tells the GameController that shooting is complete.
        IsShootingComplete = true;
    }

    // Finds and sets the direction to shoot in.
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
            // Break the loop if the player runs out of time.
            if (timer.timeRemaining <= 0)
            {
                done = true;
                IsShootingComplete = true;
                sightLine.EndLine();
            }

            // Check if player has touched the screen.
            if (Input.touchCount > 0)
            {
                // Get touch position.
                Touch touch = Input.GetTouch(0);

                // Check if the player just started touching the screen and didn't touch a UI element.
                if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    // Set start point for drag action.
                    startPoint = GetWorldPoint(touch.position);

                    sightLine.StartLine();
                }

                // Check if the player held down and moved their finger over the screen.
                else if (touch.phase == TouchPhase.Moved && startPoint.z == 15)
                {
                    // Set end point for drag action.
                    endPoint = GetWorldPoint(touch.position);

                    SetTargetPosition(startPoint, endPoint);

                    sightLine.UpdateLineRenderer(target, equippedSnowball);
                }

                // Check if the player released their finger from the screen.
                else if (touch.phase == TouchPhase.Ended && !EventSystem.current.IsPointerOverGameObject(touch.fingerId) && startPoint.z == 15)
                {
                    // Set end point for drag action.
                    endPoint = GetWorldPoint(touch.position);

                    SetTargetPosition(startPoint, endPoint);

                    // Signal the target has been found.
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

    // Gets the world point from the touch position on the screen.
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

    // Fires a snowball toward the target.
    private void Shoot()
    {
        anim.SetTrigger("Throw");

        // Randomly play one of the 4 throw sounds.
        GetComponent<AudioSource>().PlayOneShot(throwSounds[Random.Range(0, throwSounds.Length)], 18f);

        equippedSnowball.Shoot(target, transform.position);

        // Start checking if the snowball has landed
        StartCoroutine(CheckSnowballLanded());

        // Resets target bool.
        hasTarget = false;
    }

    private IEnumerator CheckSnowballLanded()
    {
        // Wait until the snowball is no longer present in the scene
        yield return new WaitUntil(() => GameObject.FindWithTag("Snowball") == null);

        HasSnowballLanded = true;
    }
}
