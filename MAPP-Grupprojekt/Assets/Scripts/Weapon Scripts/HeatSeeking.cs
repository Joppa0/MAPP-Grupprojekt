using System.Collections;
using System.Collections.Generic;
// using UnityEditor.Timeline;
using UnityEngine;

public class HeatSeeking : MoveForward
{
    public float MoveSpeed { get; set; }
    [SerializeField] private float rotateSpeed = 20;
    [SerializeField] private GameObject smokePrefab;
    private GameObject smoke;

    private bool canHeatSeek;

    private Transform target;

    public override void SnowballBase()
    {
        base.SnowballBase();
    }

    // Start is called before the first frame update
    void Start()
    {
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

    private void HeatSeek()
    {
        Vector3 aimPos = target.position - transform.position;

        float rotation = Mathf.Atan2(-aimPos.x, aimPos.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, rotation), rotateSpeed * Time.deltaTime);
    }

    private IEnumerator StartHeatSeeking()
    {
        yield return new WaitForSeconds(1);
        canHeatSeek = true;
        FindNearestPlayer();
    }

    private void FindNearestPlayer()
    {
        foreach (GameObject player in GameObject.FindGameObjectsWithTag("Player")) {
            if (target == null)
            {
                target = player.transform;
            }
            else if (Vector3.Distance(player.transform.position, transform.position) < Vector3.Distance(target.position, transform.position))
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
        if (collision.CompareTag("Grounded") || collision.CompareTag("Player"))
        {
            smoke.GetComponent<DestroyAfterDelay>().DestroyParticles();
        }

        base.OnTriggerEnter2D(collision);
    }
}
