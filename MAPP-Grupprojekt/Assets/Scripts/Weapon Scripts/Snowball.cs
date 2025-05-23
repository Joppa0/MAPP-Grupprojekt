using System.Collections;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Snowball : MonoBehaviour
{
    [SerializeField] protected GameObject snowball;
    [SerializeField] protected float power = 5;

    public GameObject GetSnowball() { return snowball; }

    public float GetPower() { return power; }

    public virtual void Shoot(Vector3 target, Vector3 playerPos)
    {
        // Calculates the rotation in degrees.
        float rotation = Mathf.Atan2(-target.x, target.y) * Mathf.Rad2Deg;

        // Spawns a new bullet with the desired rotation.
        GameObject ball = Instantiate(snowball, playerPos, Quaternion.Euler(0, 0, rotation));
        StartCoroutine(DisableCollider(ball));
        ball.GetComponent<Rigidbody2D>().AddForce(target * power, ForceMode2D.Impulse);
    }

    // Disables the snowball's collider for a short time so it doesn't instantly collide with the player who threw it.
    private IEnumerator DisableCollider(GameObject ball)
    {
        ball.GetComponent<Collider2D>().enabled = false;

        yield return new WaitForSeconds(0.1f);

        ball.GetComponent <Collider2D>().enabled = true;
    }
}
