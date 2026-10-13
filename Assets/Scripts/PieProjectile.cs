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
        // this ignores the enemy's sight radius, without this, hitting the trigger that the enemy uses to see counts as hitting the enemy.
        if (other.isTrigger) return;

        // the NPC tag check was first used to make it not destroy on the NPC's
        // but I've just been tagging everything as NPC since we don't use the NPC tag for any responsibilities
        // it's just a quick bad way to make it go through things it should.
        // NOTE: NOW YOU CAN'T RELY ON ANYTHING COMPARING TO THE NPC TAG, SHOULD BE IRRELEVANT BUT JUST IN CASE.
        if (other.CompareTag("Player") || other.CompareTag("NPC"))
        {
            return;
        }

        Enemy enemy = other.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage);
        }

        BreakableBlock block = other.GetComponent<BreakableBlock>();
        if (block != null)
        {
            block.TakeDamage(damage);
        }

        // if it hits anything other than the player, it's destroyed.
        Destroy(gameObject);
    }
}