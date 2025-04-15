using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Aim : MonoBehaviour
{
    public Camera cam;
    public Light2D lightSource;
    public float maxLightDistance = 8f;
    public LayerMask obstacleLayer;

    void LateUpdate() // Changed from Update to LateUpdate
    {
        // Get mouse position in world space (with proper Z-depth)
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x, 
            Input.mousePosition.y, 
            -cam.transform.position.z // Compensate for camera Z-offset
        ));

        // Calculate direction from player to mouse
        Vector2 lookDir = (mousePos - transform.position).normalized;
        
        // Rotate flashlight
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        // Dynamic light distance with wall occlusion
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position, 
            lookDir, 
            maxLightDistance, 
            obstacleLayer
        );

        lightSource.pointLightOuterRadius = hit.collider 
            ? hit.distance 
            : maxLightDistance;
        
        
    }

        // Add to Aim script
    public ParticleSystem dustParticles;
    public float minFlickerSpeed = 0.1f;
    public float maxFlickerSpeed = 0.3f;

    void Update()
    {
          Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x, 
            Input.mousePosition.y, 
            -cam.transform.position.z // Compensate for camera Z-offset
        ));

        // Calculate direction from player to mouse
        Vector2 lookDir = (mousePos - transform.position).normalized;
        
        // Dust particles when light hits surfaces
        if (lightSource.pointLightOuterRadius < maxLightDistance)
        {
            dustParticles.transform.position = transform.position + (Vector3)lookDir * lightSource.pointLightOuterRadius;
            dustParticles.Emit(1);
        }

        // Random light flicker
        lightSource.intensity = Random.Range(0.9f, 1.1f);
    }

}
