using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{

    [Header("Input Action References")]
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private InputActionReference jumpAction;

    [Header("Movement Values")]
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpVelocity = 20f;

    // rigidBody is used for movement and moveInput is the value
    private Rigidbody2D rigidBody;
    private float moveInput;

    // A value we use to know which direction the projectiles are coming from
    public float facingDirection = 1f;

    private bool isGrounded;
    private float oldYPosition;
    private float stillTimer;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        oldYPosition = transform.position.y;
    }

    private void OnEnable()
    {
        moveAction.action.Enable();
        jumpAction.action.Enable();
    }

    private void OnDisable()
    {
        moveAction.action.Disable();
        jumpAction.action.Disable();
    }

    private void Update()
    {
        // .x means only the horizontal movement matters
        moveInput = moveAction.action.ReadValue<Vector2>().x;

        if (moveInput != 0f)
        {
            // returns either -1 or 1 depending on if it's positive or negative
            // 
            facingDirection = Mathf.Sign(moveInput);
        }

        rigidBody.linearVelocity = new Vector2(moveInput * speed, rigidBody.linearVelocity.y);

        UpdateGrounded();

        // if we press jump and we're grounded, jump.
        if (jumpAction.action.WasPressedThisFrame() && isGrounded == true)
        {
            // when we write rigidBody.linearVelocity for an updated Vector2, it doesn't change anything.
            // Launch the player upwards and the Rigidbody 2D pulls them down naturally with Gravity.
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpVelocity);

            // reset everything, keep this within the if statement to immediately prevent multiple jumps
            isGrounded = false;
            stillTimer = 0f;
            oldYPosition = transform.position.y;
        }
    }

    private void UpdateGrounded()
    {
        // float y is our CURRENT y position
        float y = transform.position.y;

        // If the player's height has changed by 0.01 or more since the last saved height,
        // OR the player is currently moving up or down faster than 0.1 units per second,
        // then they are not grounded
        if (Mathf.Abs(y - oldYPosition) >= 0.01f || Mathf.Abs(rigidBody.linearVelocity.y) > 0.1f)
        {
            oldYPosition = y;
            stillTimer = 0f;
            isGrounded = false;
        }
        else
        {
            // wait the float amount of time before you can be considered grounded again
            stillTimer += Time.deltaTime;
            if (stillTimer >= 0.05f)
            {
                isGrounded = true;
            }
        }
    }
}