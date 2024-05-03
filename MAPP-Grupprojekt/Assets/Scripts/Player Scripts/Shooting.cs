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
    private LineRenderer lineRenderer;
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
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Initiates shooting.
    public IEnumerator StartShoot()
    {
        IsShootingComplete = false;

        StartCoroutine(SetShootTarget());

        yield return new WaitUntil(() => hasTarget);

        Shoot();
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

                    StartLine();
                }

                else if (touch.phase == TouchPhase.Moved && startPoint.z == 15)
                {
                    // Sets end point for drag action.
                    endPoint = Camera.main.ScreenToWorldPoint(touch.position);
                    endPoint.z = 15;

                    /* 
                     * Sets the shoot target to the opposite vector of the start and end points.
                     * Vector is clamped so the shot has a maximum and minimum force.
                     */
                    target = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));

                    UpdateLineRenderer();
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

                    EndLine();
                }
            }

            // Works the same way as touch, but with the mouse. Used for debugging.
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                startPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                startPoint.z = 15;

                StartLine();
            }

            else if (Input.GetMouseButton(0) && startPoint.z == 15)
            {
                endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 15;

                target = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));

                UpdateLineRenderer();
            }

            else if (Input.GetMouseButtonUp(0) && !EventSystem.current.IsPointerOverGameObject() && startPoint.z == 15)
            {
                endPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                endPoint.z = 15;

                target = new Vector2(Mathf.Clamp(startPoint.x - endPoint.x, minPower.x, maxPower.x), Mathf.Clamp(startPoint.y - endPoint.y, minPower.y, maxPower.y));

                hasTarget = true;

                done = true;

                EndLine();
            }
            yield return null;
        }
    }

    // Sets new points for the sight line to show the snowball's predicted path.
    private void UpdateLineRenderer()
    {
        // Don't draw a new line if the player hasn't started dragging their finger.
        if (target.magnitude <= 0)
            return;

        // Get rotation angle the snowball will be thrown from.
        float rotation = 90 + (Mathf.Atan2(-target.x, target.y) * Mathf.Rad2Deg);

        // Rotation can't be over 90.
        if (rotation > 90)
        {
            rotation = 180 - rotation;
        }

        // Convert rotation to radians, which is needed for Cos and Tan.
        float radians = rotation * Mathf.Deg2Rad;

        float initialVelocity = target.magnitude * 5 / equippedSnowball.GetBullet().GetComponent<Rigidbody2D>().mass;

        Vector3[] linePositions = new Vector3[lineRenderer.positionCount];
        linePositions[0] = transform.position;

        // Calculate gravity for the equipped snowball.
        float gravity = Mathf.Abs(Physics.gravity.y) * equippedSnowball.GetBullet().GetComponent<Rigidbody2D>().gravityScale;

        // Calculate position for each point along the line.
        for (int i = 1; i < linePositions.Length; i++)
        {
            // Space each line point.
            if (target.x > 0)
                linePositions[i].x = transform.position.x + i;
            else
                linePositions[i].x = transform.position.x - i;

            // Calculate y position according to the trajectory formula.
            linePositions[i].y = transform.position.y + i * Mathf.Tan(radians) - gravity * (i * i /
                (2 * (initialVelocity * initialVelocity) * (Mathf.Cos(radians) * Mathf.Cos(radians))));
        }

        for (int i = 0; i < linePositions.Length - 1; i++)
        {
            // Check if the line will intersect anything else than a player.
            RaycastHit2D hit = Physics2D.Raycast(linePositions[i], linePositions[i + 1] - linePositions[i], Vector3.Distance(linePositions[i], linePositions[i + 1]), ~LayerMask.GetMask("No player hit"));

            if (hit.collider != null)
            {
                // Last point on the line becomes the hit point, effectively cutting the line.
                linePositions[9] = hit.point;

                for (int j = 1; j < linePositions.Length - 1; j++)
                {
                    // Move x position for each point along the line to get an even distribution.
                    if (target.x > 0)
                        linePositions[j].x = linePositions[j - 1].x + Mathf.Abs(linePositions[0].x - linePositions[linePositions.Length - 1].x) / (linePositions.Length - 1);
                    else
                        linePositions[j].x = linePositions[j - 1].x - Mathf.Abs(linePositions[0].x - linePositions[linePositions.Length - 1].x) / (linePositions.Length - 1);

                    // Calculate distance along the line.
                    float distance = Mathf.Abs(linePositions[j].x - linePositions[0].x);

                    // Calculate new y position for the point after having moved.
                    linePositions[j].y = transform.position.y + distance * Mathf.Tan(radians) - gravity * (distance * distance /
                    (2 * (initialVelocity * initialVelocity) * (Mathf.Cos(radians) * Mathf.Cos(radians))));
                }
                break;
            }
        }

        lineRenderer.SetPositions(linePositions);
    }

    private void StartLine()
    {
        lineRenderer.enabled = true;
    }

    private void EndLine()
    {
        lineRenderer.enabled = false;
    }

    // Fires a snowball in toward the target.
    private void Shoot()
    {
        equippedSnowball.Shoot(target, transform.position);

        // Resets target bool.
        hasTarget = false;

        // Tells the GameController that shooting is complete.
        IsShootingComplete = true;
    }
}
