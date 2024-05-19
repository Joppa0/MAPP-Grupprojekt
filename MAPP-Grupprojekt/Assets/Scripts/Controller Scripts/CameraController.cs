using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float followSpeed = 5f;
    public float zoomSpeed = 2f;
    public float zoomAmount = 7f;
    public float minOrthographicSize = 5f;
    public float maxOrthographicSize = 10f;

    private GameObject snowball;
    private bool isFollowingSnowball = false;
    private Vector3 originalPosition;
    private Bounds cameraBounds;
    private Vector3 targetPosition;
    private Camera mainCamera;
    private float originalOrthographicSize;

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    void Start()
    {
        originalPosition = transform.position;
        originalOrthographicSize = mainCamera.orthographicSize;
        CalculateCameraBounds();
    }

    void FixedUpdate()
    {
        if (!isFollowingSnowball)
        {
            FindAndFollowSnowball();
        }
        else
        {
            if (snowball != null)
            {
                targetPosition = snowball.transform.position;
                targetPosition = GetClampedPosition(targetPosition);

                Vector3 newPosition = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
                newPosition.z = -10;
                transform.position = newPosition;

                float newOrthographicSize = Mathf.Lerp(mainCamera.orthographicSize, zoomAmount, zoomSpeed * Time.deltaTime);
                mainCamera.orthographicSize = Mathf.Clamp(newOrthographicSize, minOrthographicSize, maxOrthographicSize);
            }
            else
            {
                isFollowingSnowball = false;
                StartCoroutine(ReturnToOriginalPosition());
            }
        }
    }

    private void CalculateCameraBounds()
    {
        var bounds = Globals.WorldBounds;
        float camVertExtent = mainCamera.orthographicSize;
        float camHorzExtent = camVertExtent * mainCamera.aspect;

        float minX = bounds.min.x + camHorzExtent;
        float maxX = bounds.max.x - camHorzExtent;
        float minY = bounds.min.y + camVertExtent;
        float maxY = bounds.max.y - camVertExtent;

        cameraBounds = new Bounds();
        cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0.0f),
            new Vector3(maxX, maxY, 0.0f)
        );
    }

    private Vector3 GetClampedPosition(Vector3 targetPos)
    {
        return new Vector3(
            Mathf.Clamp(targetPos.x, cameraBounds.min.x, cameraBounds.max.x),
            Mathf.Clamp(targetPos.y, cameraBounds.min.y, cameraBounds.max.y),
            transform.position.z
        );
    }

    void FindAndFollowSnowball()
    {
        snowball = GameObject.FindGameObjectWithTag("Snowball");
        if (snowball != null)
        {
            isFollowingSnowball = true;
        }
    }

    IEnumerator ReturnToOriginalPosition()
    {
        yield return new WaitForSeconds(0.5f);
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f || Mathf.Abs(mainCamera.orthographicSize - originalOrthographicSize) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, followSpeed * Time.deltaTime);
            mainCamera.orthographicSize = Mathf.Lerp(mainCamera.orthographicSize, originalOrthographicSize, zoomSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = originalPosition;
        mainCamera.orthographicSize = originalOrthographicSize;
    }
}