using UnityEngine;

public class BreakableBlock : MonoBehaviour
{
    [SerializeField] private AudioClip destroySound;
    private int health = 1;

    public void TakeDamage(int amount)
    {
        health -= amount;
        if (health <= 0)
        {
            AudioManager.Instance.PlaySFX(destroySound);
            Destroy(gameObject);
        }
    }
}