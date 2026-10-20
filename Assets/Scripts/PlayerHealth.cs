using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip dieSound;

    [SerializeField] private float invincibilityTime = 1f; // used for taking no damage and how long the player is transparent
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

    private SpriteRenderer spriteRenderer;
    private Color originalColor;
    private float flashTimer;

    private void Awake()
    {
        currentHealth = maxHealth;
        movement = GetComponent<PlayerMovement>();
        body = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        originalColor = spriteRenderer.color;
    }

    // taken directly from Enemy.cs, except instead of red, it's transparency we're fixing
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

    public void TakeDamage(int amount, Vector2 hitSourcePosition)
    {
        // dont take damage while invincible or dead
        if (isDead || Time.time < invincibilityFramesDuration)
        {
            return;
        }

        currentHealth -= amount;
        
        invincibilityFramesDuration = Time.time + invincibilityTime;

        // transparency logic
        Color transparent = originalColor;
        transparent.a = 0.5f;               // half transparency
        spriteRenderer.color = transparent;
        flashTimer = invincibilityTime;

        movement.GetKnockedBack(hitSourcePosition);

        if (currentHealth <= 0)
        {
            StartCoroutine(Respawn());
        }
        else
        {
            AudioManager.Instance.PlaySFX(hurtSound);
        }
    }

    // This is how the screen fades to black
    private IEnumerator Fade(float beginningOpacity, float endOpacity)
    {
        // store the current color and start a timer
        Color color = fadeImage.color;
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            color.a = Mathf.Lerp(beginningOpacity, endOpacity, timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        // color.a means alpha, set the alpha of the color to match the endOpacity which is always 1
        color.a = endOpacity;
        // then finally assign it to the fadeImage
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
        AudioManager.Instance.PlaySFX(dieSound);

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

        // copy and pasted transparency logic, probably should make this a method
        Color transparent = originalColor;
        transparent.a = 0.5f;
        spriteRenderer.color = transparent;
        flashTimer = invincibilityTime;
    }
}