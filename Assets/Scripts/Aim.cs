using UnityEngine;
using UnityEngine.Rendering.Universal;

public class Aim : MonoBehaviour
{
    public Camera cam;
    public Light2D lightSource;
    public Vector2 lookDir;
    public float maxLightDistance = 8f;
    public LayerMask obstacleLayer;

    void LateUpdate()
    {
        Vector3 mousePos = cam.ScreenToWorldPoint(new Vector3(
            Input.mousePosition.x, 
            Input.mousePosition.y, 
            -cam.transform.position.z
        ));

        lookDir = (mousePos - transform.position).normalized;
        
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, angle - 90f);

        HandleLightOcclusion();
    }

    void HandleLightOcclusion()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position, 
            lookDir, 
            maxLightDistance, 
            obstacleLayer
        );

        lightSource.pointLightOuterRadius = hit.collider ? hit.distance : maxLightDistance;
        lightSource.intensity = Random.Range(0.9f, 1.1f);

        // Shadow illumination check
        RaycastHit2D[] hits = Physics2D.RaycastAll(
            transform.position,
            lookDir,
            lightSource.pointLightOuterRadius
        );
    
        foreach (RaycastHit2D shadowHit in hits)
        {
            ShadowEnemy shadow = shadowHit.collider.GetComponent<ShadowEnemy>();
            if (shadow != null)
            {
                shadow.SetIlluminated(true);
            }
        }
    }
}
