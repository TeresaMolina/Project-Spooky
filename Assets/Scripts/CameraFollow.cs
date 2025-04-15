using UnityEngine;

public class SmoothCameraFollow : MonoBehaviour
{
    public Transform target; // Assign player object
    public float smoothSpeed = 5f;
    public Vector3 offset = new Vector3(0f, 0f, -10f); // Z for 2D ortho cam

    void LateUpdate()
    {
        if (target == null) return;

        // Follow only X/Y position, maintain Z offset
        Vector3 targetPosition = target.position + offset;
        targetPosition.z = offset.z; // Lock Z-axis
        
        transform.position = Vector3.Lerp(
            transform.position, 
            targetPosition, 
            smoothSpeed * Time.deltaTime
        );
    }
}
