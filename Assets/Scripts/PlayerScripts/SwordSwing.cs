using UnityEngine;
using UnityEngine.InputSystem;


public class SwordSwing : MonoBehaviour
{
    [SerializeField] private int Damage = 1;
    

    [SerializeField] private PlayerController playerController;
    [SerializeField] private GameObject PlayerSword;

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction attackAction;

    [SerializeField] private SceneInfo sceneInfo;

    [SerializeField] private Transform playerPos;
    [SerializeField] private Vector3 offset = new Vector3(2, 0, 5);
    private float lastDirection = 1;


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

        Vector3 directionOffset = new Vector3(offset.x * direction, offset.y, offset.z); 

        if (attackAction.WasPressedThisFrame() &&  sceneInfo.HasSword() == true) 
        {
            Quaternion rotation = direction > 0 ? Quaternion.Euler(0, 0, -90) : Quaternion.Euler(0, 0, 90);
            GameObject swordInstance = Instantiate(PlayerSword, playerPos.position + new Vector3(offset.x * direction, offset.y, offset.z), rotation);
            SwordLifetime swordLifetime = swordInstance.GetComponent<SwordLifetime>();
            if (swordLifetime != null)
                swordLifetime.Setup(playerPos, playerController, offset);
        }
    }
}
