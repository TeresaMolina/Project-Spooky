using UnityEngine;

public class PlayerMovementMain : MonoBehaviour
{

    public float walkSpeed = 3f;
    public float runSpeed = 6f;
    public Rigidbody2D rb;
    public Animator animator;

    Vector2 movement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    //void Start()
    //{
    //    rb = GetComponent<Rigidbody2D>;
    //}

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        //moveInput = Vector2(moveX, moveY).normalized;

        //diagonal mvmt
        movement = movement.normalized;

        //animator params
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);
        animator.SetFloat("Speed", movement.sqrMagnitude);

        //Left shift to run
        bool isRunning = Input.GetKey(KeyCode.LeftShift);
        animator.SetBool("isRunning", isRunning);


        //if moving store direction

        if(movement != Vector2.zero)
        {
            animator.SetFloat("LastHorizontal", movement.x);
            animator.SetFloat("LastVertical", movement.y);
        }




    }

    private void FixedUpdate()
    {
        float speed = animator.GetBool("isRunning") ? runSpeed : walkSpeed;
        //rb.angularVelocity = moveInput * moveSpeed;
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
}
