using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 1f;
    Rigidbody2D rb;
    bool enemyHasHorizontalVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = new Vector2(moveSpeed, 0f);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        moveSpeed = -moveSpeed;
        FlipEnemy();
    }

    void FlipEnemy()
    {
        enemyHasHorizontalVelocity = Mathf.Abs(rb.linearVelocityX) > Mathf.Epsilon;

        if (enemyHasHorizontalVelocity)
        {
            transform.localScale = new Vector2(-(Mathf.Sign(rb.linearVelocityX)), 1f);
        }
    }
}
