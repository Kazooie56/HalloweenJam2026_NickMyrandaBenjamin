using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Balloon : MonoBehaviour
{
    [SerializeField] private float maxScale = 2f;
    [SerializeField] private float timeTillBlowup = 1f;
    [SerializeField] private int damage = 2;
    [SerializeField] private float explosionDuration = 0.1f;
    [SerializeField] private AudioClip inflateSound;
    [SerializeField] private AudioClip popSound;

    [SerializeField] private CircleCollider2D blastCollider;
    private void Awake()
    {
        blastCollider.enabled = false; // off while growing
    }
    private void Start()
    {
        StartCoroutine(GrowAndPop());
        AudioManager.Instance.PlaySFX(inflateSound);
    }

    private IEnumerator GrowAndPop()
    {
        Vector3 startSize = transform.localScale;
        Vector3 endSize = startSize * maxScale;
        float timeThatPassed = 0f;
        
        // loop as long as it takes till timeTillBlowup
        while (timeThatPassed < timeTillBlowup)
        {
            // starts a timer that begins ticking each frame
            // progress is a representation of how close we are to finishing the while loop
            // and the Lerp moves from start size to end size based on that progress
            timeThatPassed += Time.deltaTime;
            float progress = timeThatPassed / timeTillBlowup;
            transform.localScale = Vector3.Lerp(startSize, endSize, progress);
            yield return null;
        }

        Pop();
    }

    private void Pop()
    {
        blastCollider.enabled = true;
        AudioManager.Instance.PlaySFX(popSound);
        Destroy(gameObject, explosionDuration);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
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
    }
}