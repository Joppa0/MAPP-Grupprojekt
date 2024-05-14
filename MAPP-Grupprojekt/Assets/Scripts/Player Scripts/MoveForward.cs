using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private Rigidbody2D rgbd;
    protected float timer;
    private GameObject player;

    public virtual void SnowballBase()
    {

    }

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
            Destroy(gameObject);
        }

        if (other.gameObject.CompareTag("Grounded"))
        {
            Destroy(gameObject);
        }
    }
}
