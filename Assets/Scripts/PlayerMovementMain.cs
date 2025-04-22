using UnityEngine;

public class PlayerMovementMain : MonoBehaviour
{

    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    void Update()
    {
        movement = Vector2.zero;

        // Check for input and immediately update facing direction
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            movement.y = 1;
            animator.SetFloat("LastHorizontal", 0);
            animator.SetFloat("LastVertical", 1);
        }
        else if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            movement.y = -1;
            animator.SetFloat("LastHorizontal", 0);
            animator.SetFloat("LastVertical", -1);
        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            movement.x = -1;
            animator.SetFloat("LastHorizontal", -1);
            animator.SetFloat("LastVertical", 0);
        }
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            movement.x = 1;
            animator.SetFloat("LastHorizontal", 1);
            animator.SetFloat("LastVertical", 0);
        }

        // Live movement update for walking/running
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        bool isRunning = Input.GetKey(KeyCode.LeftShift);
    }


    private void FixedUpdate()
    {
        float speed = animator.GetBool("isRunning") ? runSpeed : walkSpeed;
        //rb.angularVelocity = moveInput * moveSpeed;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
