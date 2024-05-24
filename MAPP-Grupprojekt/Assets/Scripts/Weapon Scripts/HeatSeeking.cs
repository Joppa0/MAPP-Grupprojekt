using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Timeline;
using UnityEngine;

public class HeatSeeking : DestroySnowball
{
    public float MoveSpeed { get; set; }
    [SerializeField] private float rotateSpeed = 20;
    [SerializeField] private GameObject smokePrefab;
    private GameObject smoke;

    private bool canHeatSeek;

    private Transform target;

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        StartCoroutine(StartHeatSeeking());
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;

        if (timer > 5)
        {
            Destroy(gameObject);
        }

        UpdateSmoke();
    }

    private void FixedUpdate()
    {
        if (canHeatSeek)
        {
            HeatSeek();
        }

        Move();
    }

    private void Move()
    {
        GetComponent<Rigidbody2D>().velocity = transform.up * MoveSpeed;
    }

    // Slowly rotates the snowball toward the target.
    private void HeatSeek()
    {
        // Find the vector pointing towards the target.
        Vector3 aimPos = target.position - transform.position;

        // Convert vector to rotation.
        float rotation = Mathf.Atan2(-aimPos.x, aimPos.y) * Mathf.Rad2Deg;

        // Rotate a fixed amount toward the target.
        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, rotation), rotateSpeed * Time.deltaTime);
    }

    private IEnumerator StartHeatSeeking()
    {
        FindTarget();
        yield return new WaitForSeconds(1);
        canHeatSeek = true;
    }

    private void FindTarget()
    {
        // Check for the player farthest away from the snowball, and sets it as the target.
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (target == null)
            {
                target = player.transform;
            }
            else if (Vector3.Distance(player.transform.position, transform.position) > Vector3.Distance(target.position, transform.position))
            {
                target = player.transform;
            }
        }
    }

    public void SpawnSmokeCloud()
    {
        smoke = Instantiate(smokePrefab, transform.position, smokePrefab.transform.rotation);
    }

    private void UpdateSmoke()
    {
        if (smoke == null)
        {
            return;
        }

        // Updates position to follow the snowball.
        smoke.transform.position = transform.position;

        // Sets rotation of the smoke to be in the opposite direction of the snowball.
        smoke.transform.eulerAngles = new Vector3(-transform.eulerAngles.z + 90, 90, -90);
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        // Destroy smoke effect.
        if (collision.CompareTag("Grounded") || collision.CompareTag("Player"))
        {
            smoke.GetComponent<DestroyAfterDelay>().DestroyParticles();
        }

        if (collision.gameObject.CompareTag("Player"))
        {
            smoke = null;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            Camera.main.GetComponent<CameraController>().StartShake(0.05f, 0.15f);
            audSou.PlayOneShot(hitSound, 1f);
            Destroy(gameObject, 1f);
        }

        if (collision.gameObject.CompareTag("Grounded"))
        {
            smoke = null;
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            Camera.main.GetComponent<CameraController>().StartShake(0.05f, 0.15f);
            audSou.PlayOneShot(hitSound, 1f);
            Destroy(gameObject, 1f);
        }
    }
}
