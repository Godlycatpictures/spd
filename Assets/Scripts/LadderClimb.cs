using UnityEngine;
using UnityEngine.InputSystem;

public class LadderClimb : MonoBehaviour
{

    private Vector2 verticalValue;
    private float speed = 8f;
    private bool isLaddered;
    private bool isClimbing;
    private Vector2 vector2Values;

    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private InputAction moveAction;
    [SerializeField] private InputAction climbAction;


    [SerializeField] private Rigidbody2D rgbd;
    [SerializeField] private SpriteRenderer rend; // typ nån rolig climb animation om finns

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
        
        verticalValue = moveAction.ReadValue<Vector2>();
        if(isLaddered && Mathf.Abs(verticalValue.y) > 0f)
        {
            isClimbing = true;
        }
    }

    private void FixedUpdate()
    {
        if (isClimbing)
        {
            rgbd.gravityScale = 0f;
            rgbd.linearVelocity = new Vector2(rgbd.linearVelocity.x, verticalValue.y * speed);
        }
        else
        {
            rgbd.gravityScale = 1f;
        }
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
        }
    }
}
