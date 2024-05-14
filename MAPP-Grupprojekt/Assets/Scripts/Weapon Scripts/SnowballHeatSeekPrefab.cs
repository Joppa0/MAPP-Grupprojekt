using System.Collections;
using UnityEngine;

public class SnowballHeatSeekPrefab : Snowball
{
    [SerializeField] private float minSpeed, maxSpeed;
    public override void Shoot(Vector3 target, Vector3 playerPos)
    {
        // Calculates the rotation in degrees.
        float rotation = Mathf.Atan2(-target.x, target.y) * Mathf.Rad2Deg;

        // Spawns a new bullet with the desired rotation.
        GameObject ball = Instantiate(snowball, playerPos + Vector3.Normalize(target) * 1.5f, Quaternion.Euler(0, 0, rotation));

        ball.GetComponent<HeatSeeking>().MoveSpeed = Mathf.Clamp(target.magnitude * power, minSpeed, maxSpeed);

        ball.GetComponent<HeatSeeking>().SpawnSmokeCloud();
    }
}
