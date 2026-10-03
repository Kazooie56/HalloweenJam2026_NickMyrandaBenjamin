using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private float speed = 2f;
    [SerializeField] private int health = 3;

    private Rigidbody2D rigidBody;
    private Transform player;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        // returns either -1 or 1 depending on if it's positive or negative
        // if player is more to the right, the x is higher, therefore, move right.
        float directionToMove = Mathf.Sign(player.position.x - rigidBody.position.x);
        rigidBody.linearVelocity = new Vector2(directionToMove * speed, 0);
    }


    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // damage later
        }
    }
}