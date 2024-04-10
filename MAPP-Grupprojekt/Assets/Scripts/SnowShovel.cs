using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowShovel : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private bool canShoot = true;
    [SerializeField] private float shootCooldown = 0.5f;
    [SerializeField] private float bulletSpacing = 0.5f; // Avstånd mellan huvudkulan och de sidokulorna

    // Update is called once per frame
    void Update()
    {
        GetTouch();
    }

    private void GetTouch()
    {
        if (Input.touchCount > 0 && canShoot)
        {
            Touch touch = Input.GetTouch(0);
            StartCoroutine(ShootCooldown());
            Shoot(Camera.main.ScreenToWorldPoint(touch.position));
        }
        else if (Input.GetMouseButtonDown(0) && canShoot) // Vänsterklick
        {
            StartCoroutine(ShootCooldown());
            Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
        else if (Input.GetMouseButtonDown(2) && canShoot) // Mittenklick
        {
            StartCoroutine(ShootCooldown());
            ShootWithSpread(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void Shoot(Vector2 touchPos)
    {
        Vector2 aimPos = touchPos - new Vector2(transform.position.x, transform.position.y);
        float rotation = Mathf.Atan2(-aimPos.x, aimPos.y) * Mathf.Rad2Deg;
        Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, rotation));
    }

    // Ny metod för att skjuta med spridning
    private void ShootWithSpread(Vector2 touchPos)
    {
        // Skjuter den centrala kulan
        Shoot(touchPos);

        // Beräknar position och rotation för sidokulorna
        Vector2 leftPos = new Vector2(touchPos.x - bulletSpacing, touchPos.y);
        Vector2 rightPos = new Vector2(touchPos.x + bulletSpacing, touchPos.y);

        Shoot(leftPos);
        Shoot(rightPos);
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
}
