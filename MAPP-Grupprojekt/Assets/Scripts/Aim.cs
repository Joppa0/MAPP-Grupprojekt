using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Aim : MonoBehaviour
{
    [SerializeField] private GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetTouch();
    }

    private void GetTouch()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            Vector2 touchPos = touch.position;
            AimAt(Camera.main.ScreenToWorldPoint(touchPos));
        }

        if (Input.GetMouseButtonDown(0))
        { 
            Vector2 pos = Input.mousePosition;
            AimAt(Camera.main.ScreenToWorldPoint(pos));
        }
    }

    private void AimAt(Vector2 touchPos)
    {
        Vector2 aimPos = touchPos - new Vector2(transform.position.x, transform.position.y);

        float rotation = Mathf.Atan2(-aimPos.x, aimPos.y) * Mathf.Rad2Deg;

        Instantiate(bullet, transform.position, Quaternion.Euler(0, 0, rotation));
    }
}
