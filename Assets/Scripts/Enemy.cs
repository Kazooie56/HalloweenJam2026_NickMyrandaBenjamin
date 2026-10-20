using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Stats")]
    [SerializeField] private float speed = 2f;
    [SerializeField] private int health = 3;

    [Header("Sound Effects")]
    [SerializeField] private AudioClip hitSound;
    [SerializeField] private AudioClip deathSound;

    private SpriteRenderer spriteRenderer;      // used for flipping sprite + damage color
    private Rigidbody2D rigidBody;              // used for moving enemy + gravity
    private Transform player;                   // used for target for enemy
    private bool playerInRange;                 // used to determine if player is close enough

    // damage specific stuff
    private Color originalColor;                // original color of sprite 
    private float flashDuration = 0.1f;         // on hit flash duration
    private float flashTimer;



    // A value we use to know which direction the enemies should face.
    public float facingDirection = 1f;
    [SerializeField] private Transform visual;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    private void Update()
    {
        if (flashTimer > 0f)
        {
            flashTimer -= Time.deltaTime;
            if (flashTimer <= 0f)
            {
                spriteRenderer.color = originalColor;
            }
        }
    }
    private void FixedUpdate()
    {
        if (playerInRange)
        {
            // returns either -1 or 1 depending on if it's positive or negative
            // if player is more to the right, the x is higher, therefore, move right.
            float directionToMove = Mathf.Sign(player.position.x - rigidBody.position.x);

            // rigidBody.linearVelocity.y lets gravity do it's thing, they used to float when it was 0.
            rigidBody.linearVelocity = new Vector2(directionToMove * speed, rigidBody.linearVelocity.y);

            // returns either -1 or 1 depending on if it's positive or negative
            facingDirection = Mathf.Sign(directionToMove);
            UpdateFacing();
        }
        else
        {
            // don't move but let them fall
            rigidBody.linearVelocity = new Vector2(0, rigidBody.linearVelocity.y);
        }
    }

    // if they're in range, it's true, false if not.
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player")) playerInRange = false;
    }

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            AudioManager.Instance.PlaySFX(deathSound);
            Destroy(gameObject);
        }

        AudioManager.Instance.PlaySFX(hitSound);
        // turn the sprite/ghost enemies red. Color.___ has tons of crazy colors like bisque apparently
        spriteRenderer.color = Color.red;
        flashTimer = flashDuration;
    }

    private void UpdateFacing()
    {
        // just inverts the image's scale depending on the direction you're looking, 
        // since it always returns -1 or 1, it always flips perfectly.
        Vector3 scale = visual.localScale;
        scale.x = Mathf.Abs(scale.x) * facingDirection;
        visual.localScale = scale;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // if Player touches Enemy
        if (collision.gameObject.CompareTag("Player"))
        {
            // Get the player's health component and hurt them
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(1, transform.position);
            }
        }
    }
}