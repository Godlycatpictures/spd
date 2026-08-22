using System.Collections;
using UnityEngine;

public class GhostController : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private SpriteRenderer rend;
    [SerializeField] private float TempDamagePrevention = 1f; // ska inte r�ka d� av att st� i en sp�ke
    [SerializeField] private int enemyDmg = 1; // hur mkt skada spelren tar

    [SerializeField] private bool canMove = true;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rend = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (!canMove)
            return;

        transform.Translate(new Vector2(0, moveSpeed) * Time.deltaTime);

    }
    private void OnTriggerEnter2D(Collider2D other)
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



            other.gameObject.GetComponent<HealthScript>().TakeDamage(enemyDmg);
            


            //other.gameObject.GetComponent<PlayerController>().TakeDamage(enemyDmg); // skada spelaren 


        }
    }
    
    
}
