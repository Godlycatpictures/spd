
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    //ändrad från tutorialen, bored bara ha saker med movement och animation för movement.

    // Movement
    private float horizontalValue;
    private float verticalValue;
    private bool isGrounded;
    private bool canMove;

    //stege
    private Vector2 moveValue;
    [Header("Climb")]
    [SerializeField] private float speed = 4f;
    [SerializeField] private bool isLaddered;
    [SerializeField] private bool isClimbing;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 300f;
    //special movement
    [Header("Movement 2")]
    [SerializeField] private float coyoteTime = 0.1f;
    [SerializeField] private float coyoteTimer;
    [SerializeField] private float jumpBufferTime = 0.1f;
    [SerializeField] private float jumpBufferTimer;

    //komponenter
    [Header("saker")]
    [SerializeField] private Transform leftFoot;
    [SerializeField] private Transform rightFoot;
    [SerializeField] private float rayDistance = 0.25f;
    [SerializeField] private LayerMask groundLayer;

    //typ mer komponenter
    private Rigidbody2D rgbd;
    private Animator anim;
    private SpriteRenderer rend;

    // movement
    [Header("Input")]
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction jumpAction;
    [SerializeField] private InputAction moveAction;

    

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
        anim = GetComponent<Animator>();

    }

    private void Update()
    {
        moveValue = moveAction.ReadValue<Vector2>();
        verticalValue = moveValue.y;
        horizontalValue = moveValue.x;
        
        if (isLaddered && Mathf.Abs(verticalValue) > 0f)
        {
            isClimbing = true;
        }
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
        if (isClimbing) //klättra
        {
            rgbd.gravityScale = 0f;
            rgbd.linearVelocity = new Vector2(rgbd.linearVelocity.x, verticalValue * speed);
        }
        else
        {
            
            rgbd.gravityScale = 1f;
        }

        if (!canMove) // fancy smancy
            return;
        
        // väsnter höger
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Climb"))
        {
            isLaddered = true;
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Climb"))
        {
            isLaddered = false;
            isClimbing = false;
            rgbd.linearVelocity = new Vector2(rgbd.linearVelocity.x, verticalValue * 0);
        }
    }
}
