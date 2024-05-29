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
        mainCamera = Camera.main; //Get the main camera component
    }

    void Start()
    {
        originalPosition = transform.position; //Store original position of the camera
        originalOrthographicSize = mainCamera.orthographicSize; //Store the original ortographic size of the camera
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

    //Calculate the bounds within which the camera can move
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
    // Clamp the target position within the camera bounds
    private Vector3 GetClampedPosition(Vector3 targetPos)
    {
        return new Vector3(
            Mathf.Clamp(targetPos.x, cameraBounds.min.x, cameraBounds.max.x),
            Mathf.Clamp(targetPos.y, cameraBounds.min.y, cameraBounds.max.y),
            transform.position.z
        );
    }

    // find the snwoball and follow it
    void FindAndFollowSnowball()
    {
        snowball = GameObject.FindGameObjectWithTag("Snowball");
        if (snowball != null)
        {
            isFollowingSnowball = true;
        }
    }

    // Coroutine to return the camera to its original position and zoom level
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

    // Method to start the Coroutine.
    // Otherwise the effect wouldn't play if Shake is called by a gameobject that is immedietly destroyed.
    public void StartShake(float magnitude, float duration)
    {
        StartCoroutine(Shake(magnitude, duration));
    }

    // Shakes the camera by the specified amount for the specified time.
    private IEnumerator Shake(float magnitude, float duration)
    {
        // Save start position.
        Vector3 startPos = transform.position;

        float elapsed = 0;

        while (elapsed < duration)
        {
            // Get random x and y values to move the camera in.
            float x = Random.Range(-magnitude, magnitude);
            float y = Random.Range(-magnitude, magnitude);

            // Update camera positon.
            transform.position = new Vector3(transform.position.x + x, transform.position.y + y, startPos.z);

            elapsed += Time.deltaTime;

            // Wait for next frame.
            yield return null;
        }
    }
}