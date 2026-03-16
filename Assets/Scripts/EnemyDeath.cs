using UnityEngine;

public class EnemyDeath : MonoBehaviour
{
    [SerializeField] private SpriteRenderer rend;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    public void Die()
    {
        Animator animator = GetComponent<Animator>();
        if (animator != null)
        {
            animator.SetTrigger("Sword");

            foreach (var collider in GetComponents<CapsuleCollider2D>())
            {
                collider.enabled = false;
            }
            

            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.gravityScale = 0;
            rb.linearVelocity = Vector2.zero;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void OnDeathAnimationComplete()
    {
        Destroy(gameObject);
    }
}
