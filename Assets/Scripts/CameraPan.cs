using UnityEngine;

public class CameraPan : MonoBehaviour
{
    public float RecenterTime;
    public float CameraHorizontalOffset;
    public float CameraVerticalOffset;
    public float CameraHeight;
    public Transform playerTransform;
    public Rigidbody playerRb;
    public Vector3 currentCameraVelocity;

    void Awake()
    {
        transform.position = playerTransform.position;
        currentCameraVelocity = Vector3.zero;
    }

    void LateUpdate()
    {
        Vector2 playerDirection = new Vector2(playerRb.velocity.x, playerRb.velocity.z).normalized;
        Vector3 playerVelocity = Vector3.zero;
        Vector3 offSetPosition = new Vector3(playerTransform.position.x,CameraHeight, playerTransform.position.z);
        offSetPosition.x += CameraHorizontalOffset * playerDirection.x;
        offSetPosition.z += CameraVerticalOffset * playerDirection.y;

        transform.position = Vector3.SmoothDamp(transform.position, offSetPosition, ref currentCameraVelocity,RecenterTime);
    }
}
