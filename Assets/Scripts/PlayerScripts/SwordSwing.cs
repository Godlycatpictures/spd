using UnityEngine;
using UnityEngine.InputSystem;


public class SwordSwing : MonoBehaviour
{
    [SerializeField] private int Damage = 1;

    [Header("Info")]
    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject PlayerSword;

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction attackAction;

    [SerializeField] private SceneInfo sceneInfo;
   
    [Header("Psotioner o sånt")]
    [SerializeField] private Transform playerPos;
    [SerializeField] private Vector3 offset = new Vector3(2, 0, 5);
    [SerializeField] private float travelDuration = 0.3f;
    private float lastDirection = 1;

    [Header("Cooldown")]
    [SerializeField] private float cooldownTime = 0.5f; 
    [SerializeField] private bool canAttack = true;
    [SerializeField] private float cooldownTimer = 0f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions["Attack"];
    }

    private void Update()
    {
        float direction = playerController.GetDirection();

        if (direction == 0)
            direction = lastDirection;
        else
            lastDirection = direction;

        if (!canAttack)
        {
            cooldownTimer -= Time.deltaTime;
            if (cooldownTimer <= 0f)
            {
                canAttack = true;
                cooldownTimer = 0f;
            }
        }

        if (attackAction.WasPressedThisFrame() && sceneInfo.HasSword() == true && canAttack)
        {
            Quaternion rotation = direction > 0 ? Quaternion.Euler(0, 0, -90) : Quaternion.Euler(0, 0, 90);

            
            GameObject swordInstance = Instantiate(PlayerSword, playerPos.position, rotation);

            SwordLifetime swordLifetime = swordInstance.GetComponent<SwordLifetime>();
            if (swordLifetime != null)
            {
                
                swordLifetime.Setup(playerPos, direction, offset.magnitude, travelDuration);
            }

            canAttack = false;
            cooldownTimer = cooldownTime;
        }
    }
}
