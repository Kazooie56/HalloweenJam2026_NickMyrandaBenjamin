using UnityEngine;
using UnityEngine.UI;

public class PlayerHealthUI : MonoBehaviour
{
    [SerializeField] private PlayerHealth health;
    [SerializeField] private Image healthBar;

    [Header("Health Sprites")]
    [SerializeField] private Sprite threeHealthSprite;
    [SerializeField] private Sprite twoHealthSprite;
    [SerializeField] private Sprite oneHealthSprite;
    [SerializeField] private Sprite noHealthSprite;

    void Update()
    {
        if (health.currentHealth >= 3)
        {
            healthBar.sprite = threeHealthSprite;
        }
        else if (health.currentHealth == 2)
        {
            healthBar.sprite = twoHealthSprite;
        }
        else if (health.currentHealth == 1)
        {
            healthBar.sprite = oneHealthSprite;
        }
        else
        {
            healthBar.sprite = noHealthSprite;
        }
    }
}
