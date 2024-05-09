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
    [SerializeField] private Vector3 vectorZ;

    void Start()
    {
        originalPosition = transform.position;
    }

    void Update()
    {

        Debug.Log("Camera Position: " + transform.position);
        Debug.Log("Camera Rotation: " + transform.rotation.eulerAngles);

        if (!isFollowingSnowball)
        {
            FindAndFollowSnowball();
        }
        else
        {
            if (snowball != null)
            {
                // Move camera towards snowball
                Vector3 targetPosition = snowball.transform.position;
                vectorZ = Vector3.Lerp(transform.position, targetPosition, followSpeed * Time.deltaTime);
                vectorZ.z = -10;
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
