using System.Collections;
using UnityEngine;

public class Snowball : Shooting
{
    private bool hasShot = false; // Kontroll om ett skott redan har avfyrats

    // Update is called once per frame
    void Update()
    {
        /*if (Input.GetMouseButtonDown(0) && !hasShot) // Kontrollerar om vänster musknapp är nedtryckt och inget skott har avfyrats
        {
            StartCoroutine(StartShoot());
            //hasShot = true; // Förhindrar ytterligare skott
        }*/
    }

    protected override void Shoot()
    {
        base.Shoot(); // Anropar Shoot från superklassen för att hantera skottet

        // Eftersom vi vill ha samma mekanik för var kulan ska skapas, återanvänder vi kod från SnowShovel
        /*
        Vector2 aimPos = target - new Vector2(transform.position.x, transform.position.y);
        float rotation = Mathf.Atan2(-aimPos.x, aimPos.y) * Mathf.Rad2Deg;
        float spawnDistance = 2.0f; // Samma avstånd som SnowShovel
        Vector3 spawnPosition = transform.position + (Vector3)(aimPos.normalized * spawnDistance);
        Instantiate(bullet, spawnPosition, Quaternion.Euler(0, 0, rotation));
        */
    }

    public void ResetShot()
    {
        hasShot = false; // Metod för att återställa skott-status så att SingleShot kan avfyra igen
    }
}
