using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    // Movement
    private float horizontalValue;
    private bool isGrounded;
    private bool canMove;

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
    //special movement
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float coyoteTimer;
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] private float jumpBufferTimer;
    private void Awake()
    {
        EnableMovement();
        playerInput = GetComponent<PlayerInput>();


        jumpAction = playerInput.actions["Jump"];
        moveAction = playerInput.actions["Move"];
    }


    private void Start()
    {
        EnableMovement();
        rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();

    }

    private void Update()
    {
        horizontalValue = moveAction.ReadValue<float>(); // vill typ ha tillgån när som från andra skripts
        if (canMove)
        {
            FlipSprite();
            CalcJumpBuffer();
            CalcCoyoteTime();
            Jump();
        }
        
    }

    private void FlipSprite()
    {
        
        // vänster höger schabang
        if (horizontalValue > 0)
            rend.flipX = false;
        else if (horizontalValue < 0)
            rend.flipX = true;

    }

    private void FixedUpdate()
    {
        if (!canMove) // fancy smancy
            return;

        rgbd.linearVelocity = new Vector2(horizontalValue * moveSpeed, rgbd.linearVelocity.y);
    }
    
    private void Jump()
    {
        // hopp
        if (jumpBufferTimer > 0f && coyoteTimer > 0f) // nya systemet är långt
        {
            Vector2 v = rgbd.linearVelocity;
            v.y = 0f;
            rgbd.linearVelocity = v;
            rgbd.AddForce(Vector2.up * jumpForce);

            coyoteTimer = 0f; // kunde nästan dubbelhoppa
            jumpBufferTimer = 0f;
        }
        
    }
    private void CalcJumpBuffer()
    {
        if (jumpAction.WasPressedThisFrame())
            jumpBufferTimer = jumpBufferTime;
        else if (jumpBufferTimer > 0f)
            jumpBufferTimer -= Time.deltaTime;
    }
    private void CalcCoyoteTime()
    {
        if (CheckIfGrounded())
            coyoteTimer = coyoteTime;
        else
            coyoteTimer -= Time.deltaTime;

    }

    public float GetDirection()
    {
        return horizontalValue;
    }


    private bool CheckIfGrounded()
    {
        RaycastHit2D leftHit = Physics2D.Raycast(leftFoot.position, Vector2.down, rayDistance, groundLayer);
        RaycastHit2D rightHit = Physics2D.Raycast(rightFoot.position, Vector2.down, rayDistance, groundLayer);

        return leftHit.collider != null || rightHit.collider != null;
    }

    public void KnockBack(float knockbackForce, float upKnockback)
    {

        canMove = false;
        rgbd.AddForce(new Vector2(knockbackForce, upKnockback));

        Invoke("EnableMovement", 0.25f);
    }
    private void EnableMovement()
    {
        canMove = true;
    }
   
}
