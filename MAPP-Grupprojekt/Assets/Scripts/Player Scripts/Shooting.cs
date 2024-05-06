using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Rendering;

public class Shooting : MonoBehaviour
{

    
    public enum Snowballs
    {
        Snowball, HeatSeeking, SnowShovel
    }

    public bool IsShootingComplete { get; set; }
    public Snowball equippedSnowball;

    private Vector2 target;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Vector2 minPower, maxPower;

    private bool hasTarget;

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
    }

    private IEnumerator SetShootTarget()
    {
        if (IsShootingComplete)
        {
            yield break;
        }

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

                else if (touch.phase == TouchPhase.Ended && !EventSystem.current.IsPointerOverGameObject(touch.fingerId) && startPoint.z == 15)
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

                    done = true;
                }
            }

            // Works the same way as touch, but with the mouse. Used for debugging.
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                startPoint.z = 15;
            }

            if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject() && startPoint.z == 15)
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

        //snowballCamera.SwitchCameras(true);

        StartCoroutine(SetShootTarget());

        yield return new WaitUntil(() => hasTarget);

        Shoot();

        //yield return new WaitForSeconds(2f);

        //snowballCamera.SwitchCameras(false);
    }

    // Fires a bullet in toward the target.
    private void Shoot()
    {
        equippedSnowball.Shoot(target, transform.position);

        // Resets target bool.
        hasTarget = false;

        // Tells the GameController that shooting is complete.
        IsShootingComplete = true;
    }
}
