using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    private bool canShoot = true;
    [SerializeField] private float shootCooldown = 0.5f;

    // Update is called once per frame
    void Update()
    {
        GetTouch();
    }

    private void GetTouch()
    {
        if (Input.touchCount > 0 && canShoot)
        {
            // Gets touch.
            Touch touch = Input.GetTouch(0);

            // Starts shooting cooldown.
            StartCoroutine(ShootCooldown());

            // Calls the method to shoot with the world position of the touch.
            Shoot(Camera.main.ScreenToWorldPoint(touch.position));
        }

        else if (Input.GetMouseButtonDown(0))
        {
            Shoot(Camera.main.ScreenToWorldPoint(Input.mousePosition));
        }
    }

    private void Shoot(Vector2 touchPos)
    {
        // Gets the direction to aim in.
        Vector2 aimPos = touchPos - new Vector2(transform.position.x, transform.position.y);

        // Calculates the rotation in degrees.
        float rotation = Mathf.Atan2(-aimPos.x, aimPos.y) * Mathf.Rad2Deg;

        // Spawns a new bullet with the desired rotation.
        Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, rotation));
    }

    private IEnumerator ShootCooldown()
    {
        canShoot = false;
        yield return new WaitForSeconds(shootCooldown);
        canShoot = true;
    }
}
