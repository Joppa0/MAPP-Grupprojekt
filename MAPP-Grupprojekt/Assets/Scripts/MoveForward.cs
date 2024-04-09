using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveForward : MonoBehaviour
{

    [SerializeField] private float thrust = 20;

    private Rigidbody2D rgbd;
    private float timer;

    // Start is called before the first frame update
    void Start()
    {

        rgbd = GetComponent<Rigidbody2D>();

        rgbd.AddRelativeForce(Vector3.up * thrust);

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

}
