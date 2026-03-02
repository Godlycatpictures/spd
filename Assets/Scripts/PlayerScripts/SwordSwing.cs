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


    private void Awake()
    {
        playerController = GetComponent<PlayerController>();
        
        playerInput = GetComponent<PlayerInput>();
        attackAction = playerInput.actions["Attack"];
    }

    private void Update()
    {
        if (attackAction.WasPressedThisFrame() &&  sceneInfo.HasSword() == true) 
        {
            Instantiate(PlayerSword);
        }
    }
}
