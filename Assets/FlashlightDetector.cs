using UnityEngine;

public class FlashlightDetector : MonoBehaviour
{
    public bool isLit = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Flashlight"))
        {
            isLit = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Flashlight"))
        {
            isLit = false;
        }
    }
}
