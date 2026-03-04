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
        if(GetComponent<Animator>() != null)
            GetComponent<Animator>().SetTrigger("Sword");   

        foreach (var collider in GetComponents<CapsuleCollider2D>())
        {
            collider.enabled = false;
        }

        GetComponent<Rigidbody2D>().gravityScale = 0; // inte röra sig
        GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;

        Destroy(gameObject, 0.5f); // delay
    }
}
