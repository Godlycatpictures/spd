using UnityEngine;

public class SwordLifetime : MonoBehaviour
{
    [SerializeField] private float lifetime = 1.5f;
    private Transform playerTransform;
    private PlayerController playerController;
    private Vector3 offset;
    private float lastDirection = 1;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Setup(Transform player, PlayerController controller, Vector3 swordOffset)
    {
        playerTransform = player;
        playerController = controller;
        offset = swordOffset;
    }

    private void Update()
    {
        lifetime -= Time.deltaTime;

        if (lifetime <= 0)
        {
            Destroy(gameObject);
            return;
        }
        
        float direction = playerController.GetDirection();
        

        if (direction == 0)
            direction = lastDirection;
        else
            lastDirection = direction;

        transform.position = playerTransform.position + new Vector3(offset.x * direction, offset.y, offset.z);
        
        transform.rotation = direction > 0 ? Quaternion.Euler(0, 0, -90) : Quaternion.Euler(0, 0, 90);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyDeath>().Die();
        }
    }

}
