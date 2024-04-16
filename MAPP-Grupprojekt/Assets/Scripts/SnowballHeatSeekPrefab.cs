using System.Collections;
using UnityEngine;

public class SnowballHeatSeekPrefab : Snowball
{
    public override void Shoot(Vector3 target, Vector3 playerPos)
    {
        // Gets the direction to aim in.
        Vector2 aimPos = target - playerPos;

        // Calculates the rotation in degrees.
        float rotation = Mathf.Atan2(-aimPos.x, aimPos.y) * Mathf.Rad2Deg;

        // Spawns a new bullet with the desired rotation.
        GameObject ball = Instantiate(bullet, playerPos + Vector3.Normalize(target) * 1.5f, Quaternion.Euler(0, 0, rotation));
        ball.GetComponent<Rigidbody2D>().AddForce(target * power, ForceMode2D.Impulse);
    }
}
