using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class HeatSeeking : MoveForward
{
    [SerializeField] private float moveSpeed = 20;
    [SerializeField] private float rotateSpeed = 20;

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

        Move();
        if (canHeatSeek)
        {
            HeatSeek();
        }
    }

    private void Move()
    {
        transform.Translate(Vector3.up * moveSpeed * Time.deltaTime);
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
}
