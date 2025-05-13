using UnityEngine;

public class PlayerMovementMain : MonoBehaviour
{
    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public Rigidbody2D rb;
    public Animator animator;
    
    private Vector2 movement;
    private Vector2 lastDirection;

    void Update()
    {
        // Input handling
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        movement = movement.normalized;

        // Animation parameters
        if (movement != Vector2.zero)
        {
            lastDirection = movement;
            animator.SetFloat("Horizontal", movement.x);
            animator.SetFloat("Vertical", movement.y);
        }
        
        animator.SetFloat("Speed", movement.sqrMagnitude);
        animator.SetBool("isRunning", Input.GetKey(KeyCode.LeftShift));
        animator.SetFloat("LastHorizontal", lastDirection.x);
        animator.SetFloat("LastVertical", lastDirection.y);
    }

    void FixedUpdate()
    {
        float speed = animator.GetBool("isRunning") ? runSpeed : walkSpeed;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
