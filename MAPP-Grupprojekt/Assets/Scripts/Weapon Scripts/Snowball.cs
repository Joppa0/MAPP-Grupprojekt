using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Snowball : MonoBehaviour
{
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected float power = 5;

    public GameObject GetBullet() { return bullet; }

    public virtual void Shoot(Vector3 target, Vector3 playerPos)
    {
        // Calculates the rotation in degrees.
        float rotation = Mathf.Atan2(-target.x, target.y) * Mathf.Rad2Deg;

        // Spawns a new bullet with the desired rotation.
        GameObject ball = Instantiate(bullet, playerPos, Quaternion.Euler(0, 0, rotation));
        StartCoroutine(DisableCollider(ball));
        ball.GetComponent<Rigidbody2D>().AddForce(target * power, ForceMode2D.Impulse);
    }

    private IEnumerator DisableCollider(GameObject ball)
    {
        ball.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(0.1f);

        ball.GetComponent <Collider2D>().enabled = true;
    }
}
