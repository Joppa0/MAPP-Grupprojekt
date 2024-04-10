using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class HeatSeeking : MoveForward
{
    private float thrust = 900;
    [SerializeField] private float speed = 20;

    private Rigidbody2D rgbd;
    private float timer;

    private bool canHeatSeek;

    private Transform playerTransform;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();

        rgbd.AddRelativeForce(Vector3.up * thrust);

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
    }

    private void FixedUpdate()
    {
        Move();
        if (canHeatSeek)
        {
            HeatSeek();
        }
    }

    private void Move()
    {
        //transform.position = transform.Translate(transform.position);
    }

    private void HeatSeek()
    {
        //transform.position = Vector3.Lerp(transform.position, playerTransform.position, speed * Time.deltaTime);

        //Vector2 newVelocity = (playerTransform.position - transform.position).normalized * speed;

        //rgbd.velocity = Vector3.Cross(newVelocity, rgbd.velocity);

        //transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, transform.rotation.z + 0.01f);
        
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
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
            if (playerTransform == null)
            {
                playerTransform = player.transform;
            }
            else if (Vector3.Distance(player.transform.position, transform.position) < Vector3.Distance(playerTransform.position, transform.position))
            {
                playerTransform = player.transform;
            }
        }
    }
}
