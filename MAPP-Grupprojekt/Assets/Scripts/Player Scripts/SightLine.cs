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

        // Calculate position for each point along the line.
        for (int i = 0; i < linePositions.Length; i++)
        {
            // Space each line point.
            linePositions[i].x = target.x > 0 ? transform.position.x + i: transform.position.x - i;

            // Assign the height value.
            linePositions[i].y = GetLinePositionHeight(radians, gravity, initialVelocity, i);
        }

        for (int i = 0; i < linePositions.Length - 1; i++)
        {
            // Check if the line will intersect anything else than a player.
            RaycastHit2D hit = Physics2D.Raycast(linePositions[i], linePositions[i + 1] - linePositions[i], Vector3.Distance(linePositions[i], linePositions[i + 1]), ~LayerMask.GetMask("No player hit"));

            if (hit.collider != null)
            {
                // Last point on the line becomes the hit point, effectively cutting the line.
                linePositions[linePositions.Length - 1] = hit.point;

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
                    linePositions[j].y = GetLinePositionHeight(radians, gravity, initialVelocity, distance);
                }
                break;
            }
        }

        lineRenderer.SetPositions(linePositions);
    }

    // Calculate y-position of a point on the line according to the trajectory formula.
    private float GetLinePositionHeight(float radians, float gravity, float initialVelocity, float x)
    {
        return transform.position.y + x * Mathf.Tan(radians) - gravity * (x * x /
                (2 * (initialVelocity * initialVelocity) * (Mathf.Cos(radians) * Mathf.Cos(radians))));
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
