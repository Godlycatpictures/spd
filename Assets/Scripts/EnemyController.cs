using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private float PlayerJumpOnKill = 100f; // hundra är ingenting
    [SerializeField] private int enemyDmg = 1; // hur mkt skada spelren tar

    [SerializeField] private float knockbackForce = 200f; // knockback kraften
    [SerializeField] private float upKnockback = 100f;


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
            moveSpeed = -moveSpeed; // "låter logiskt, det är väl logiskt"
        }

        if (other.gameObject.CompareTag("Enemy"))
        {
            moveSpeed = -moveSpeed; // "låter logiskt, det är väl logiskt"
        }

        if (other.gameObject.CompareTag("Player"))
        {

            if (other.transform.position.x > transform.position.x) // kolla vilken sida spelaren är på
            {
                other.gameObject.GetComponent<PlayerController>().KnockBack(knockbackForce, upKnockback); // knockback åt höger
            }
            else
            {
                other.gameObject.GetComponent<PlayerController>().KnockBack(-knockbackForce, upKnockback); // knockback åt vänster
            }
            TempCollisionDisable(); // ska inte kunna döda enemien som knuffat dig uppåt

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
            // här ish ska ljudet spelas
            other.GetComponent<Rigidbody2D>().linearVelocity = new Vector2(other.GetComponent<Rigidbody2D>().linearVelocity.x, 0); // hastighet nollställs neråt så den enklare åker uppåt
            other.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, PlayerJumpOnKill)); // trampolin hoppet som ksk inte skulle användas änvands här :o

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
