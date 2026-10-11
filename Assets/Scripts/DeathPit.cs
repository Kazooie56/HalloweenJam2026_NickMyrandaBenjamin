using UnityEngine;

public class DeathPit : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // if Player collides with the deathpit
        if (other.CompareTag("Player"))
        {
            // Kill them instantly
            other.GetComponent<PlayerHealth>().FallInDeathPit();
        }
    }
}