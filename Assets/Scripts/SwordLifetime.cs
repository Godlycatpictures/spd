using UnityEngine;

public class SwordLifetime : MonoBehaviour
{
    [SerializeField] private float lifetime = 1.5f;
    [SerializeField] private AnimationCurve movementCurve = AnimationCurve.EaseInOut(0, 0, 1, 1);
    
    private float direction;
    private float maxDistance;
    private float travelDuration;
    private float currentTravelTime = 0f;
    private float currentLifetime = 0f;
    private bool isTraveling = true;

    private Transform playerTransform;

    private Vector3 startPosition;
    private Vector3 targetPosition;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void Setup(Transform player, float lookDirection, float distance, float duration)
    {
        playerTransform = player;
        direction = lookDirection;
        maxDistance = distance;
        travelDuration = duration;

        transform.position = playerTransform.position;
    }

    void Update()
    {
        if (playerTransform == null) return;

        if (isTraveling)
        {
            
            currentTravelTime += Time.deltaTime;
            float t = Mathf.Clamp01(currentTravelTime / travelDuration);

            float curvedT = movementCurve.Evaluate(t);

            Vector3 basePosition = playerTransform.position;
            Vector3 directionVector = new Vector3(direction, 0, 0);

            startPosition = basePosition;
            targetPosition = basePosition + (directionVector * maxDistance);

            transform.position = Vector3.Lerp(startPosition, targetPosition, curvedT);

            transform.rotation = direction > 0 ? Quaternion.Euler(0, 0, -90) : Quaternion.Euler(0, 0, 90);

            if (t >= 1f)
            {
                isTraveling = false;
                currentLifetime = 0f;
            }
        }
        else
        {
            currentLifetime += Time.deltaTime;

            //throbbing
            transform.localScale = Vector3.one * (1 + Mathf.Sin(currentLifetime * 10) * 0.05f);

            if (currentLifetime >= lifetime)
            {
                Destroy(gameObject);
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<EnemyDeath>().Die();
        }
    }

    

    void OnDrawGizmosSelected()
    {
        if (playerTransform != null)
        {
            Gizmos.color = Color.yellow;
            Vector3 directionVector = new Vector3(direction, 0, 0);
            Vector3 endPos = playerTransform.position + (directionVector * maxDistance);
            Gizmos.DrawLine(playerTransform.position, endPos);
            Gizmos.DrawWireSphere(endPos, 0.2f);
        }
    }

}
