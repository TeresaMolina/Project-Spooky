using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target; // Assign to player
    public float smoothSpeed = 0.125f; // Speed at which camera follows player
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Adjust this for zoom level

    void LateUpdate()
    {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        // Optional: Keep camera rotation aligned with player's direction
        // transform.rotation = Quaternion.Euler(0f, 0f, target.rotation.eulerAngles.z);
    }
}
