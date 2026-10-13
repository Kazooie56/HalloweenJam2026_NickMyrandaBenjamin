using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int health = 3;

    [SerializeField] private float flashDuration = 0.1f; // on hit flash
    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private float flashTimer;

    private Rigidbody2D rigidBody;
    private Transform player;
    private bool playerInRange;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
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
            Destroy(gameObject);
        }

        // turn the ghost red?
        spriteRenderer.color = Color.red;
        flashTimer = flashDuration;
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