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
    [SerializeField] private float knockbackX = 6f;
    [SerializeField] private float knockbackY = 8f;
    [SerializeField] private float knockbackDuration = 0.75f;

    // rigidBody is used for movement and moveInput is the value
    private Rigidbody2D rigidBody;
    private float moveInput;

    // A value we use to know which direction the player and projectiles should face.
    public float facingDirection = 1f;

    private float knockbackTimer;
    private bool jumpRequested;

    [SerializeField] private Transform visual;

    private bool isGrounded;
    private float oldYPosition;
    private float stillTimer;

    public bool canMove;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();

        oldYPosition = transform.position.y;

        canMove = true;
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
        if (PauseMenu.IsPaused)
        {
            return;
        }

        if (!canMove || knockbackTimer > 0f)
        {
            moveInput = 0f;
            return;
        }

        // .x means only the horizontal movement matters
        moveInput = moveAction.action.ReadValue<Vector2>().x;

        if (moveInput != 0f)
        {
            // returns either -1 or 1 depending on if it's positive or negative
            facingDirection = Mathf.Sign(moveInput);
            UpdateFacing();
        }

        if (jumpAction.action.WasPressedThisFrame())
        {
            jumpRequested = true;
        }
    }

    private void FixedUpdate()
    {
        // player can't move until they are done taking knockback
        if (knockbackTimer > 0f)
        {
            knockbackTimer -= Time.fixedDeltaTime;
            jumpRequested = false;
            return;
        }

        // no jumping if can't move
        if (!canMove)
        {
            jumpRequested = false;
            rigidBody.linearVelocity = new Vector2(0f, rigidBody.linearVelocity.y);
            return;
        }

        UpdateGrounded();

        // the actual movement line
        rigidBody.linearVelocity = new Vector2(moveInput * speed, rigidBody.linearVelocity.y);

        // if jumpRequested was made true from Update, and we're grounded
        if (jumpRequested && isGrounded)
        {
            // replace our rigidbody's velocity vertically and add our jump velocity instead
            rigidBody.linearVelocity = new Vector2(rigidBody.linearVelocity.x, jumpVelocity);

            // reset everything, keep this within the if statement to immediately prevent multiple jumps
            isGrounded = false;
            stillTimer = 0f;
            oldYPosition = transform.position.y;
        }

        jumpRequested = false;
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

    private void UpdateFacing()
    {
        // just inverts the image's scale depending on the direction you're looking, 
        // since it always returns -1 or 1, it always flips perfectly.
        Vector3 scale = visual.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDirection;
        visual.localScale = scale;
    }

    public void GetKnockedBack(Vector2 hitSourcePosition)
    {
        // returns either -1 or 1 depending on if it's positive or negative
        // if player is more to the right, the x is higher, therefore, move right.
        float direction = Mathf.Sign(transform.position.x - hitSourcePosition.x);

        knockbackTimer = knockbackDuration;
        rigidBody.linearVelocity = new Vector2(direction * knockbackX, knockbackY);
        isGrounded = false;
    }
}