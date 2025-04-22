using UnityEngine;

public class KillerBehavior : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 3f;
    private FlashlightDetector detector;
    private SpriteRenderer spriteRenderer;

    public Sprite coverFaceSprite; // Sprite when lit
    public Sprite armsExtendedSprite; // Sprite when chasing

    void Start()
    {
        detector = GetComponent<FlashlightDetector>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (detector.isLit)
        {
            // Light is shining - Cover face!
            spriteRenderer.sprite = coverFaceSprite;
        }
        else
        {
            // No light - Chase player!
            spriteRenderer.sprite = armsExtendedSprite;

            // Move toward player
            Vector2 direction = (player.position - transform.position).normalized;
            transform.position += (Vector3)(direction * moveSpeed * Time.deltaTime);
        }
    }
}
