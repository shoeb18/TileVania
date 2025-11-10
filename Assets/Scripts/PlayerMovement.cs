using UnityEditor.Tilemaps;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] float movementSpeed = 10f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] LayerMask groundLayer;

    Vector2 moveInput;
    Rigidbody2D rb;
    Animator animator;
    BoxCollider2D boxCollider;

    private bool playerHasMoveInput;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        Movement();
        Flip();
    }

    void Movement()
    {
        rb.linearVelocity = new Vector2(moveInput.x * movementSpeed, rb.linearVelocityY);
        animator.SetBool("IsRunning", playerHasMoveInput);
    }

    void Flip()
    {
        playerHasMoveInput = Mathf.Abs(rb.linearVelocityX) > Mathf.Epsilon;
        if (playerHasMoveInput)
        {
            transform.localScale = new Vector2(Mathf.Sign(rb.linearVelocityX), 1f);
        }
    }
    
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
    }

    void OnJump(InputValue value)
    {
        if (value.isPressed && boxCollider.IsTouchingLayers(groundLayer))
        {
            rb.AddForceY(jumpForce, ForceMode2D.Impulse);
        }
    }
}
