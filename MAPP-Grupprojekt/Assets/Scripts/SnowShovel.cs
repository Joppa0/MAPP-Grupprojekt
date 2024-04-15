using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class SnowShovel : Shooting
{
    [SerializeField] private float bulletSpacing = 0.5f; // Avstånd mellan huvudkulan och sidokulorna

    private bool canShoot = true; // Kontroll om spelaren kan skjuta

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0) && canShoot) 
        {
            StartCoroutine(StartShoot());
        }*/
    }

    protected override void Shoot()
    {
        base.Shoot(); 

        // Beräknar position och rotation för sidokulorna
        Vector2 leftPos = new Vector2(target.x - bulletSpacing, target.y);
        Vector2 rightPos = new Vector2(target.x + bulletSpacing, target.y);


        // Skjuter sidokulorna
        FireBullet(leftPos);
        FireBullet(rightPos);

    }

    private void FireBullet(Vector2 shootPosition)
    {
        /*Vector2 aimPos = shootPosition - new Vector2(transform.position.x, transform.position.y);
        float rotation = Mathf.Atan2(-aimPos.x, aimPos.y) * Mathf.Rad2Deg;
        float spawnDistance = 2.0f;  // Sätter ett spawnavstånd som undviker kollision med spelaren
                                     // Normaliserar aimPos först och sedan multiplicerar med spawnDistance för att beräkna spawn positionen
        Vector3 spawnPosition = transform.position + (Vector3)(aimPos.normalized * spawnDistance);
        Instantiate(bullet, spawnPosition, Quaternion.Euler(0, 0, rotation));*/

        // Gets the direction to aim in.
        Vector2 aimPos = target - new Vector2(-transform.position.x, transform.position.y);

        // Calculates the rotation in degrees.
        float rotation = Mathf.Atan2(aimPos.x, aimPos.y) * Mathf.Rad2Deg;

        // Spawns a new bullet with the desired rotation.
        GameObject ball = Instantiate(bullet, transform.position + Vector3.Normalize(target) * 1.5f, Quaternion.Euler(0, 0, rotation));
        ball.GetComponent<Rigidbody2D>().AddForce(target * power, ForceMode2D.Impulse);
    }


}
