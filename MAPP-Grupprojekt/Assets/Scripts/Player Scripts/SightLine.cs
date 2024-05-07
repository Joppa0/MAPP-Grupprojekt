using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(LineRenderer))]
public class SightLine : MonoBehaviour
{
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Sets new points for the sight line to show the snowball's predicted path.
    public void UpdateLineRenderer(Vector3 target, GameObject snowball)
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

        float initialVelocity = target.magnitude * 5 / snowball.GetComponent<Rigidbody2D>().mass;

        Vector3[] linePositions = new Vector3[lineRenderer.positionCount];
        linePositions[0] = transform.position;

        // Calculate gravity for the equipped snowball.
        float gravity = Mathf.Abs(Physics.gravity.y) * snowball.GetComponent<Rigidbody2D>().gravityScale;

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

    public void StartLine()
    {
        lineRenderer.enabled = true;
    }

    public void EndLine()
    {
        lineRenderer.enabled = false;
    }
}
