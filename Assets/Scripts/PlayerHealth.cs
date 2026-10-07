using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private int maxHealth = 3;
    [SerializeField] private float knockbackX = 6f;
    [SerializeField] private float knockbackY = 8f;
    [SerializeField] private float invincibilityTime = 1f;

    private int currentHealth;
    private float invincibilityFramesDuration;
    private PlayerMovement movement;

    private void Awake()
    {
        currentHealth = maxHealth;
        movement = GetComponent<PlayerMovement>();
    }

    // Take damage method here later

    private void Die()
    {
        // death logic here
    }
}
