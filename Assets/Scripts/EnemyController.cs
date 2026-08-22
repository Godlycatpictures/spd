using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private float PlayerJumpOnKill = 100f; // hundra �r ingenting
    [SerializeField] private int enemyDmg = 1; // hur mkt skada spelren tar

    [SerializeField] private float knockbackForce = 200f; // knockback kraften
    [SerializeField] private float upKnockback = 100f;
    [SerializeField] private AudioClip hitSFX;


    private bool canMove = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;
        
        transform.Translate(new Vector2(moveSpeed, 0) * Time.deltaTime);

        if (moveSpeed > 0)
            rend.flipX = false;
        else if (moveSpeed < 0)
            rend.flipX = true;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("EnemyBlock"))
        {
            moveSpeed = -moveSpeed; // "l�ter logiskt, det �r v�l logiskt"
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed; // "l�ter logiskt, det �r v�l logiskt"
        }

        if (other.gameObject.CompareTag("Player"))
        {

            if (other.transform.position.x > transform.position.x) // kolla vilken sida spelaren �r p�
            {
                other.gameObject.GetComponent<PlayerController>().KnockBack(knockbackForce, upKnockback); // knockback �t h�ger
            }
            else
            {
                other.gameObject.GetComponent<PlayerController>().KnockBack(-knockbackForce, upKnockback); // knockback �t v�nster
            }
            TempCollisionDisable(); // ska inte kunna d�da enemien som knuffat dig upp�t

            other.gameObject.GetComponent<HealthScript>().TakeDamage(enemyDmg);

            //other.gameObject.GetComponent<PlayerController>().TakeDamage(enemyDmg); // skada spelaren 


        }
    }

    public void TempCollisionDisable()
    {
        foreach (var collider in GetComponents<BoxCollider2D>())
        {
            collider.enabled = false;
        }
        Invoke("EnableCollision", 0.5f);
    }
    public void EnableCollision()
    {
        foreach (var collider in GetComponents<BoxCollider2D>())
        {
            collider.enabled = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            AudioManager.Instance.PlaySFX(hitSFX);
            // h�r ish ska ljudet spelas
            other.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(other.GetComponent<Rigidbody2D>().linearVelocity.x, 0); // hastighet nollst�lls ner�t s� den enklare �ker upp�t
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, PlayerJumpOnKill)); // trampolin hoppet som ksk inte skulle anv�ndas �nvands h�r :o

            GetComponent<Animator>().SetTrigger("Killed");
            //GetComponent<Animator>().SetTrigger("Hit");
            foreach (var collider in GetComponents<CapsuleCollider2D>())
            {
                collider.enabled = false;
            }
            GetComponent<Rigidbody2D>().gravityScale = 0;
            GetComponent<Rigidbody2D>().linearVelocity = Vector2.zero;
            canMove = false;

            Destroy(gameObject, 0.5f);

        }
    }
}
