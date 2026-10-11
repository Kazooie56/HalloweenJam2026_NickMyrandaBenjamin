using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private float invincibilityTime = 1f;
    private int maxHealth = 3;

    [Header("Respawn")]
    [SerializeField] private Image fadeImage;       // move this logic somewhere else if I can polish.
    [SerializeField] private float fadeDuration = 0.5f;
    private Rigidbody2D body;

    public int currentHealth;
    public Transform currentCheckpoint;
    private float invincibilityFramesDuration;
    private bool isDead;
    private PlayerMovement movement;

    private void Awake()
    {
        currentHealth = maxHealth;
        movement = GetComponent<PlayerMovement>();
        body = GetComponent<Rigidbody2D>();
    }
    public void TakeDamage(int amount, Vector2 hitSourcePosition)
    {
        // dont take damage while invincible or dead
        if (isDead || Time.time < invincibilityFramesDuration)
        {
            return;
        }

        currentHealth -= amount;
        invincibilityFramesDuration = Time.time + invincibilityTime;

        movement.GetKnockedBack(hitSourcePosition);

        if (currentHealth <= 0)
        {
            StartCoroutine(Respawn());
        }
    }

    // This is how the screen fades to black
    private IEnumerator Fade(float beginningOpacity, float endOpacity)
    {
        Color color = fadeImage.color;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(beginningOpacity, endOpacity, timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = endOpacity;
        fadeImage.color = color;
    }

    public void FallInDeathPit()
    {
        if (isDead)
        {
            return;
        }

        currentHealth = 0;

        StartCoroutine(Respawn());
    }

    private IEnumerator Respawn()
    {
        isDead = true;
        movement.canMove = false;

        yield return Fade(0f, 1f);   // 0 - 1 means invisible to completely black

        // stop movement, move to checkpoint/respawn point, health is back to max.
        body.linearVelocity = Vector2.zero;
        body.position = currentCheckpoint.position;
        currentHealth = maxHealth;

        yield return Fade(1f, 0f);   // 1 - 0 means the opposite

        // reset the invincibility, allow player to move, and they're considered alive again
        invincibilityFramesDuration = Time.time + invincibilityTime;
        movement.canMove = true;
        isDead = false;
    }
}