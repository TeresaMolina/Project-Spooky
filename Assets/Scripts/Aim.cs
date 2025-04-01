using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Aim : MonoBehaviour
{
    public Camera cam;
    public Light2D lightSource; // Assign your Light2D component here
    public float raycastDistance = 10f; // Maximum distance light can reach

    void Update()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 lookDir = mousePos - transform.position;
        
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f); // Adjust offset as needed

        // this part is me messing around with the Raycast, to check for obstacles
        RaycastHit2D hit = Physics2D.Raycast(transform.position, lookDir.normalized, raycastDistance);
        
        if (hit.collider != null)
        {
            // If hit, adjust light's range to stop at obstacle
            lightSource.pointLightOuterRadius = hit.distance;
        }
        else
        {
            // If no hit, reset light's range to maximum
            lightSource.pointLightOuterRadius = raycastDistance;
        }
    }
}
