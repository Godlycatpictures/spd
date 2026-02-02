using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // Movement
    private float horizontalValue;
    private bool isGrounded;

    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 300f;

    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;
    [SerializeField] private float rayDistance = 0.25f;
    [SerializeField] private LayerMask groundLayer;

    private Rigidbody2D rgbd;
    private SpriteRenderer rend;

    // movement
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction jumpAction;
    [SerializeField] private InputAction moveAction;

    private void Awake()
    {

        playerInput = GetComponent<PlayerInput>();


        jumpAction = playerInput.actions["Jump"];
        moveAction = playerInput.actions["Move"];
    }


    private void Start()
    {

        rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        horizontalValue = moveAction.ReadValue<float>();

        // vänster höger schabang
        if (horizontalValue > 0)
            rend.flipX = false;
        else if (horizontalValue < 0)
            rend.flipX = true;

        // hopp
        if (jumpAction.WasPressedThisFrame() && CheckIfGrounded()) // nya systemet är långt
        {
            Jump();
        }
    }

    private void FixedUpdate()
    {
        rgbd.linearVelocity = new Vector2(horizontalValue * moveSpeed, rgbd.linearVelocity.y);
    }

    private void Jump()
    {
        rgbd.AddForce(Vector2.up * jumpForce);
    }

    private bool CheckIfGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, groundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, groundLayer);

        return leftHit.collider != null || rightHit.collider != null;
    }
}
