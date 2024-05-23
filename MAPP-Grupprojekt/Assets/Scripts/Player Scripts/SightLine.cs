using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(LineRenderer))]
public class SightLine : MonoBehaviour
{
    [SerializeField] private float lineDarkeningMultiplier;
    [SerializeField] private float maxLength = 10;
    [SerializeField] private float maxTime = 2;
    [SerializeField] private float timeStep = 0.1f;

    private LineRenderer lineRenderer;

    private Color initialColor;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        initialColor = lineRenderer.startColor;
    }

    // Sets new points for the sight line to show the snowball's predicted path.
    public void UpdateLineRenderer(Vector3 target, Snowball snowball)
    {
        // Don't draw a new line if the player hasn't started dragging their finger.
        if (target.magnitude <= 0)
            return;

        // Get rotation angle the snowball will be thrown from.
        float rotation = 90 + (Mathf.Atan2(-target.x, target.y) * Mathf.Rad2Deg);

        // Rotation can't be over 90.
        rotation = rotation > 90 ? 180 - rotation : rotation;

        // Convert rotation to radians, which is needed for Cos and Tan.
        float radians = rotation * Mathf.Deg2Rad;

        float initialVelocity = target.magnitude * snowball.GetPower() / snowball.GetSnowball().GetComponent<Rigidbody2D>().mass;

        // Calculate local gravity for the equipped snowball.
        float gravity = Mathf.Abs(Physics.gravity.y) * snowball.GetSnowball().GetComponent<Rigidbody2D>().gravityScale;

        List<Vector3> points = new List<Vector3>();

        points.Add(transform.position);

        float currentLength = 0;

        for (float i = timeStep; i < maxTime; i += timeStep)
        {
            Vector3 newPoint = Vector3.zero;

            // Get x and y values for the new point at the specified time during its flight.
            newPoint.x = GetLinePositionX(radians, initialVelocity, i, target);
            newPoint.y = GetLinePositionY(radians, initialVelocity, i, gravity);

            // Get the distance between the previous and new point.
            Vector3 prevPoint = points[points.Count - 1];
            float distance = maxLength - currentLength;
            currentLength += Vector3.Distance(prevPoint, newPoint);

            // If the total length of the line is over the allowed max length.
            if (currentLength > maxLength) 
            {
                // Get the point between the previous and new point that gives the line an exact distance matching the maximum allowed.
                points.Add(Vector3.MoveTowards(prevPoint, newPoint, distance));

                // Stop adding new points, since max distance is reached.
                break;
            }

            // Check if the line will collide with the environment.
            RaycastHit2D hit = Physics2D.Raycast(prevPoint, newPoint - prevPoint, Vector3.Distance(prevPoint, newPoint), ~LayerMask.GetMask("No player hit"));

            // Cut off the line at point of contact.
            if (hit.collider != null)
            {
                points.Add(hit.point);
                break;
            }

            points.Add(newPoint);
        }

        UpdateLineColor(target.magnitude);

        lineRenderer.positionCount = points.Count;
        lineRenderer.SetPositions(points.ToArray());
    }

    // Calculate x-position of a point on the line according to projectile motion.
    private float GetLinePositionX(float radians, float initialVelocity, float x, Vector3 target)
    {
        return target.x > 0 ? transform.position.x + (initialVelocity * Mathf.Cos(radians) * x) : transform.position.x - (initialVelocity * Mathf.Cos(radians) * x);
    }

    // Calculate y-position of a point on the line according to projectile motion.
    private float GetLinePositionY(float radians, float initialVelocity, float x, float gravity)
    {
        return transform.position.y + (initialVelocity * Mathf.Sin(radians) * x) - (0.5f * (gravity * x * x));
    }

    private void UpdateLineColor(float force)
    {
        // Gets the new color by converting the force to a value between 0-1, then subtracts it from the old color.
        Color color = initialColor - new Color(force * lineDarkeningMultiplier / 255, force * lineDarkeningMultiplier / 255, force * lineDarkeningMultiplier / 255, 0);

        lineRenderer.startColor = color;
        lineRenderer.endColor = color;
    }

    public void StartLine()
    {
        lineRenderer.enabled = true;
    }

    public void EndLine()
    {
        lineRenderer.enabled = false;
    }
}
