using UnityEngine;
using UnityEngine.Events;

public class PlayerMovementSidescroll : MonoBehaviour
{
    //public variables
    public LayerMask whatIsGround;
    public Transform groundCheck;
    public float jumpForce = 400f;
    public float movementSmoothing = .05f;
    public float runspeed = 40f;
    public bool inEvent = false;
    public UnityEvent onLandEvent;
    
    //private variables
    Rigidbody2D rb2D;
    Vector3 velocity = Vector3.zero;
    bool grounded;
    bool facingRight = true;
    bool jump = false;
    float groundedRadius = .2f;
    float horizontalMove = 0f;

    private void Awake()
    {
        rb2D = GetComponent<Rigidbody2D>();
        if (onLandEvent == null) onLandEvent = new UnityEvent();
    }

    void Update()
    {
        if (!inEvent)
        {
            horizontalMove = Input.GetAxisRaw("Horizontal") * runspeed;
            if (Input.GetButtonDown("Jump")) jump = true;
        }
    }

    private void FixedUpdate()
    {
        bool wasGrounded = grounded;
        grounded = false;

        Collider2D[] colliders = Physics2D.OverlapCircleAll(groundCheck.position, groundedRadius, whatIsGround);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject != gameObject)
            {
                grounded = true;
                if (!wasGrounded) onLandEvent.Invoke();
            }
        }

        if (!inEvent)
        {
            Move(horizontalMove * Time.fixedDeltaTime, jump);
            jump = false;
        }
        else Move(0, false);
    }


    public void Move(float move, bool jump)
    {
        Vector3 targetVelocity = new Vector2(move * 10f, rb2D.velocity.y);
        rb2D.velocity = Vector3.SmoothDamp(rb2D.velocity, targetVelocity, ref velocity, movementSmoothing);

        if (move > 0 && !facingRight) Flip();
        else if (move < 0 && facingRight) Flip();

        if (grounded && jump)
        {
            grounded = false;
            rb2D.AddForce(new Vector2(0f, jumpForce));
        }
    }


    private void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}