using System.Collections;
using UnityEngine;

public class Credits : MonoBehaviour
{
    [SerializeField] private RectTransform creditsText; // the text that goes upwards
    [SerializeField] private float endY = 1180f;
    [SerializeField] private float scrollSpeed = 100f;
    [SerializeField] private GameObject congratulationsScreen;
    [SerializeField] private float fadeDuration = 1.5f;

    private CanvasGroup congratulationsScreenGroup;

    private void Start()
    {
        congratulationsScreenGroup = congratulationsScreen.GetComponent<CanvasGroup>();
        congratulationsScreen.SetActive(false);

        StartCoroutine(PlayCredits());
    }

    private IEnumerator PlayCredits()
    {
        while (creditsText.anchoredPosition.y < endY)
        {
            creditsText.anchoredPosition += Vector2.up * scrollSpeed * Time.deltaTime;
            yield return null;
        }

        // Set alpha to 0 BEFORE enabling so it doesn't flash for a frame
        congratulationsScreenGroup.alpha = 0f;
        congratulationsScreen.SetActive(true);
        yield return Fade(0f, 1f);
    }

    // taken from PlayerHealth
    private IEnumerator Fade(float beginningOpacity, float endOpacity)
    {
        float timer = 0f;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            congratulationsScreenGroup.alpha = Mathf.Lerp(beginningOpacity, endOpacity, timer / fadeDuration);
            yield return null;
        }

        congratulationsScreenGroup.alpha = endOpacity;
    }
}