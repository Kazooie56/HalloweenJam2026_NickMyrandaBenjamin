using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        // Whichever checkpoint the player touched last becomes their respawn point
        if (other.TryGetComponent(out PlayerHealth playerHealth))
        {
            playerHealth.currentCheckpoint = transform;
        }
    }
}