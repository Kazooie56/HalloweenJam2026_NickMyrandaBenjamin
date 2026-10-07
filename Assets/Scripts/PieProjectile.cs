using UnityEngine;

public class PieProjectile : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float timeBeforeDespawn = 0.1f;

    private Rigidbody2D rigidBody;

    private void Awake()
    {
        rigidBody = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 direction)
    {
        rigidBody.linearVelocity = direction * speed;
        Destroy(gameObject, timeBeforeDespawn);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            return;
        }

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        // if it hits anything other than the player, it's destroyed.
        Destroy(gameObject);
    }
}
