using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class HeatSeeking : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 20;
    [SerializeField] private float rotateSpeed = 20;

    private float timer;

    private bool canHeatSeek;

    private Transform target;

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

        Move();
    }

    private void Move()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);

        if (canHeatSeek)
        {
            HeatSeek();
        }
    }

    private void HeatSeek()
    {
        // Gets the direction to aim in.
        Vector3 aimPos = target.position - transform.position;

        // Calculates the rotation in degrees.
        float rotation = Mathf.Atan2(-aimPos.x, aimPos.y) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, rotation), rotateSpeed * Time.deltaTime);

    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    private IEnumerator StartHeatSeeking()
    {
        yield return new WaitForSeconds(1.5f);
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
}
