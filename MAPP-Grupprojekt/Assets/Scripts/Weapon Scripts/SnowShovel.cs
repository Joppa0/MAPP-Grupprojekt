using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SnowShovel : Snowball
{
    [SerializeField] private float bulletSpacing = 0.5f; // Avstånd mellan huvudkulan och sidokulorna

    public override void Shoot(Vector3 target, Vector3 playerPos)
    {
        base.Shoot(target, playerPos); 

        // Beräknar position och rotation för sidokulorna
        Vector2 leftPos = new Vector2(target.x - bulletSpacing, target.y);
        Vector2 rightPos = new Vector2(target.x + bulletSpacing, target.y);


        // Skjuter sidokulorna
        base.Shoot(leftPos, playerPos);
        base.Shoot(rightPos, playerPos);
    }
}
