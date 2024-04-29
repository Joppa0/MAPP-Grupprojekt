using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SnowShovel : Snowball
{
    [SerializeField] private float bulletSpacing = 0.5f; // Avst�nd mellan huvudkulan och sidokulorna

    public override void Shoot(Vector3 target, Vector3 playerPos)
    {
        base.Shoot(target, playerPos); 

        // Ber�knar position och rotation f�r sidokulorna
        Vector2 leftPos = new Vector2(target.x - bulletSpacing, target.y);
        Vector2 rightPos = new Vector2(target.x + bulletSpacing, target.y);


        // Skjuter sidokulorna
        base.Shoot(leftPos, playerPos);
        base.Shoot(rightPos, playerPos);
    }
}
