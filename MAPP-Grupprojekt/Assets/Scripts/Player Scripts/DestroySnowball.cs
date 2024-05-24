using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySnowball : MonoBehaviour
{
    [SerializeField] protected AudioClip hitSound;
    [SerializeField] protected GameObject featherParticles;

    private Rigidbody2D rgbd;
    protected float timer;
    private GameObject player;
    protected AudioSource audSou;
    private SpriteRenderer rend;

    // Start is called before the first frame update
     protected virtual void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        audSou = GetComponent<AudioSource>();
        rend = GetComponent<SpriteRenderer>();

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
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<TrailRenderer>().enabled = false;
            Camera.main.GetComponent<CameraController>().StartShake(0.05f, 0.15f);
            audSou.PlayOneShot(hitSound, 1f);
            Instantiate(featherParticles, other.transform.position, Quaternion.identity);
            featherParticles.GetComponent<DestroyAfterDelay>().DestroyParticles();
            Destroy(gameObject, 1f);
        }

        if (other.gameObject.CompareTag("Grounded"))
        {
            GetComponent<SpriteRenderer>().enabled = false;
            GetComponent<CircleCollider2D>().enabled = false;
            GetComponent<TrailRenderer>().enabled = false;
            Camera.main.GetComponent<CameraController>().StartShake(0.05f, 0.15f);
            audSou.PlayOneShot(hitSound, 1f);
            Destroy(gameObject, 1f);
        }
    }
}
