using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;

public abstract class Shooting : MonoBehaviour
{
    public bool IsMovementComplete { get; protected set; }
    public bool IsShootingComplete { get; protected set; }

    protected Vector2 target;
    [SerializeField] protected GameObject bullet;
    [SerializeField] protected float speed = 1.0f;
    [SerializeField] protected float distanceToStop = 0.1f;

    protected bool hasTarget;

    private void FixedUpdate()
    {
        Move();
    }

    protected virtual void Move()
    {
        if (!IsMovementComplete && hasTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, new Vector2(target.x, transform.position.y), Time.deltaTime * speed);
            if (Mathf.Abs(transform.position.x - target.x) <= distanceToStop)
            {
                hasTarget = false;
                IsMovementComplete = true;
            }
        }
    }

    protected IEnumerator SetTarget()
    {
        bool done = false;
        while (!done)
        {
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began && !EventSystem.current.IsPointerOverGameObject(touch.fingerId))
                {
                    target = Camera.main.ScreenToWorldPoint(touch.position);
                    hasTarget = true;
                    done = true;
                }
            }
            else if (Input.GetMouseButton(0) && !EventSystem.current.IsPointerOverGameObject())
            {
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                hasTarget = true;
                done = true;
            }
            yield return null;
        }
    }

    public IEnumerator StartMove()
    {
        IsMovementComplete = false;
        StartCoroutine(SetTarget());
        yield return new WaitUntil(() => hasTarget);
    }

    public IEnumerator StartShoot()
    {
        IsShootingComplete = false;
        StartCoroutine(SetTarget());
        yield return new WaitUntil(() => hasTarget);
        Shoot();
    }

    protected virtual void Shoot()
    {
        Vector2 aimPos = target - new Vector2(transform.position.x, transform.position.y);
        float rotation = Mathf.Atan2(-aimPos.x, aimPos.y) * Mathf.Rad2Deg;
        float spawnDistance = 2.0f;  // Ökad från 1.5f till 2.0f för att undvika omedelbar kollision
        Instantiate(bullet, transform.position + Vector3.Normalize(aimPos) * spawnDistance, Quaternion.Euler(0, 0, rotation));
        hasTarget = false;
        IsShootingComplete = true;
    }
}
