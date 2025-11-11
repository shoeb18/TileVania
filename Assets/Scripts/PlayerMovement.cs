using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float climbSpeed = 10f;

    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    CapsuleCollider2D capsuleCollider;
    BoxCollider2D feetCollider;

    private bool playerHasHorizontalVelocity;
    private bool playerHasVerticalVelocity;
    private float defaultGravityScale;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        feetCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        defaultGravityScale = rb.gravityScale;
    }

    void Update()
    {
        Movement();
        FlipSprite();
        ClimbLadder();
    }

    void Movement()
    {
        rb.linearVelocity = new Vector2(moveInput.x * movementSpeed, rb.linearVelocityY);
        animator.SetBool("IsRunning", playerHasHorizontalVelocity);
    }

    void FlipSprite()
    {
        playerHasHorizontalVelocity = Mathf.Abs(rb.linearVelocityX) > Mathf.Epsilon;
        if (playerHasHorizontalVelocity)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.linearVelocityX), 1f);
        }
    }

    void ClimbLadder()
    {
        if (feetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            rb.linearVelocity = new Vector2(rb.linearVelocityX, moveInput.y * climbSpeed);
            rb.gravityScale = 0;

            playerHasVerticalVelocity = Mathf.Abs(rb.linearVelocityY) > Mathf.Epsilon;
            animator.SetBool("IsClimbing", playerHasVerticalVelocity);
        }
        else
        {
            rb.gravityScale = defaultGravityScale;
            animator.SetBool("IsClimbing", false);
        }
    }
    
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && feetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
        }
    }
}
