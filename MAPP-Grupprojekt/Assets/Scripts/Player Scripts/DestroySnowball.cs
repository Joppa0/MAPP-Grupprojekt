using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySnowball : MonoBehaviour
{
    private Rigidbody2D rgbd;
    protected float timer;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Translate(Vector3.forward * speed * Time.deltaTime);        

        timer += Time.deltaTime;

        if(timer > 5)
        {
            Destroy(gameObject);
        }

    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Camera.main.GetComponent<CameraController>().StartShake(0.05f, 0.15f);
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Grounded"))
        {
            Camera.main.GetComponent<CameraController>().StartShake(0.05f, 0.15f);
            Destroy(gameObject);
        }
    }
}
