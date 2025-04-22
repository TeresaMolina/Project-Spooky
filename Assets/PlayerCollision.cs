using UnityEngine;

public class PlayerCollision : MonoBehaviour
{
    public JumpscareManager jumpscareManager;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy")) // Enemy must have "Enemy" tag
        {
            jumpscareManager.TriggerJumpscare();
        }
    }
}
