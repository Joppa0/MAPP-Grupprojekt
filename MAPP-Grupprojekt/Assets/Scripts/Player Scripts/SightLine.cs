using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(LineRenderer))]
public class SightLine : MonoBehaviour
{
    [SerializeField] private float lineDarkeningMultiplier;
    [SerializeField] private float maxLength = 10;

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

        Vector3[] linePositions = new Vector3[lineRenderer.positionCount];

        // Calculate gravity for the equipped snowball.
        float gravity = Mathf.Abs(Physics.gravity.y) * snowball.GetSnowball().GetComponent<Rigidbody2D>().gravityScale;

        float timeStep = 0.1f;

        float maxTime = 2;

        List<Vector3> points = new List<Vector3>();

        for (float i = 0; i < maxTime; i += timeStep)
        {
            Vector3 newPoint = new Vector3();

            newPoint.x = target.x > 0 ? transform.position.x + (initialVelocity * Mathf.Cos(radians) * i) : transform.position.x - (initialVelocity * Mathf.Cos(radians) * i);

            newPoint.y = transform.position.y + (initialVelocity * Mathf.Sin(radians) * i) - (0.5f * (gravity * i * i));

            //Vector2 start = points.ToArray()[points.Count - 1];

            //RaycastHit2D hit = Physics2D.Raycast(start, newPoint, Vector2.Distance(start, newPoint), ~LayerMask.GetMask("No player hit"));

            points.Add(newPoint);
        }

        UpdateLineColor(target.magnitude);

        lineRenderer.positionCount = points.Count;

        lineRenderer.SetPositions(points.ToArray());
    }

    // Calculate y-position of a point on the line according to the trajectory formula.
    private float GetLinePositionHeight(float radians, float gravity, float initialVelocity, float x)
    {
        return transform.position.y + x * Mathf.Tan(radians) - gravity * (x * x /
                (2 * (initialVelocity * initialVelocity) * (Mathf.Cos(radians) * Mathf.Cos(radians))));
    }

    private void UpdateLineColor(float force)
    {
        // Gets the new color by converting the force to a value from 0-1, then subtracts it from the old color.
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
