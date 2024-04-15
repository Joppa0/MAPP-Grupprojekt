using System.Collections;
using UnityEngine;

public class Snowball : Shooting
{
    private bool hasShot = false; // Kontroll om ett skott redan har avfyrats

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0) && !hasShot) // Kontrollerar om v�nster musknapp �r nedtryckt och inget skott har avfyrats
        {
            StartCoroutine(StartShoot());
            //hasShot = true; // F�rhindrar ytterligare skott
        }*/
    }

    protected override void Shoot()
    {
        base.Shoot(); // Anropar Shoot fr�n superklassen f�r att hantera skottet

        // Eftersom vi vill ha samma mekanik f�r var kulan ska skapas, �teranv�nder vi kod fr�n SnowShovel
        /*
        Vector2 aimPos = target - new Vector2(transform.position.x, transform.position.y);
        float rotation = Mathf.Atan2(-aimPos.x, aimPos.y) * Mathf.Rad2Deg;
        float spawnDistance = 2.0f; // Samma avst�nd som SnowShovel
        Vector3 spawnPosition = transform.position + (Vector3)(aimPos.normalized * spawnDistance);
        Instantiate(bullet, spawnPosition, Quaternion.Euler(0, 0, rotation));
        */
    }

    public void ResetShot()
    {
        hasShot = false; // Metod f�r att �terst�lla skott-status s� att SingleShot kan avfyra igen
    }
}
