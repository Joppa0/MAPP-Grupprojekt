using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    public float followSpeed = 5f;
    public float zoomSpeed = 2f;
    public float zoomAmount = 2f;

    private GameObject snowball;
    private bool isFollowingSnowball = false;
    private Vector3 originalPosition;
    private Vector3 difference;
    private Vector3 vectorZ;
    private Bounds cameraBounds;
    private Vector3 targetPosition;
    private Camera mainCamera;

    private void Awake() => mainCamera = Camera.main;

    void Start()
    {
        originalPosition = transform.position;

        var height = mainCamera.orthographicSize;
        var width = height * mainCamera.aspect;

        var minX = Globals.WorldBounds.min.x + width;
        var maxX = Globals.WorldBounds.extents.x - width;

        var minY = Globals.WorldBounds.min.y + height;
        var maxY = Globals.WorldBounds.extents.y - height;

        cameraBounds = new Bounds();
        cameraBounds.SetMinMax(
            new Vector3(minX, minY, 0.0f),
            new Vector3(maxX, maxY, 0.0f)
        );
    }

    void Update()
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

                // Optionally, you can calculate the difference between target position and original position here
                difference = targetPosition - originalPosition;

                // Clamp target position within camera bounds
                targetPosition = GetCameraBounds();

                // Move camera towards snowball
                vectorZ = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
                vectorZ.z = -10; // Ensure correct z-position for 2D camera
                transform.position = vectorZ;

                // Zoom in towards snowball
                transform.Translate(Vector3.forward * zoomAmount * Time.deltaTime * zoomSpeed);
            }
            else
            {
                // If snowball is destroyed or inactive, stop following
                isFollowingSnowball = false;
                StartCoroutine(ReturnToOriginalPosition());
            }
        }
    }

    private Vector3 GetCameraBounds()
    {
        return new Vector3(
            Mathf.Clamp(targetPosition.x, cameraBounds.min.x, cameraBounds.max.x),
            Mathf.Clamp(targetPosition.y, cameraBounds.min.y, cameraBounds.max.y),
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
        while (Vector3.Distance(transform.position, originalPosition) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, originalPosition, followSpeed * Time.deltaTime);
            yield return null;
        }
    }
}