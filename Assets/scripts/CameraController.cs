using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform ball;  // Assign the ball object in the inspector
    public float distance = 10.0f;
    public float height = 5.0f;
    public float rotationSpeed = 5.0f;
    public float maxVerticalAngle = 80.0f;  // Maximum vertical angle (in degrees) the camera can move
    public float minVerticalAngle = -20.0f;  // Minimum vertical angle

    private float currentRotationX = 0f;
    private float currentRotationY = 0f;

    void Start()
    {
        // Initialize the current rotation based on the camera's starting position
        Vector3 angles = transform.eulerAngles;
        currentRotationX = angles.x;
        currentRotationY = angles.y;
    }

    void LateUpdate()
    {
        if (ball != null)
        {
            // Handle the rotation of the camera with mouse input
            currentRotationX += Input.GetAxis("Mouse X") * rotationSpeed;
            currentRotationY -= Input.GetAxis("Mouse Y") * rotationSpeed;

            // Clamp the vertical rotation to prevent full 360 degree movement
            currentRotationY = Mathf.Clamp(currentRotationY, minVerticalAngle, maxVerticalAngle);

            // Create the new rotation for the camera
            Quaternion rotation = Quaternion.Euler(currentRotationY, currentRotationX, 0);

            // Update camera position to follow the ball, maintaining set height and distance
            Vector3 positionOffset = new Vector3(0, height, -distance);
            Vector3 targetPosition = ball.position + rotation * positionOffset;

            // Avoid clipping through walls/props by using raycast to check if camera's line of sight is blocked
            RaycastHit hit;
            if (Physics.Linecast(ball.position, targetPosition, out hit))
            {
                // If something is hit, move the camera closer to avoid clipping
                targetPosition = hit.point;
            }

            transform.position = targetPosition;
            transform.LookAt(ball);  // Keep the camera looking at the ball
        }
    }

    // This method can be used to provide the direction based on camera's forward direction
    public Vector3 GetCameraForwardDirection()
    {
        Vector3 forward = transform.forward;
        forward.y = 0;  // Ignore the vertical component to keep movement on the horizontal plane
        return forward.normalized;
    }
}
