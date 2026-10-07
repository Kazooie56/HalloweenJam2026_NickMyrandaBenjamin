using TMPro;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UIManager : MonoBehaviour
{ 
    [Header("Dialogue References")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text nameText;

    [Header("Typing Settings")]
    [SerializeField] private float timeBetweenCharacters = 0.05f;
    private Coroutine typingCoroutine;

    private void Awake()
    {
        HideDialoguePanel();
    }

    //Dialogue
    public void ShowDialoguePanel()
    {
        dialoguePanel.SetActive(true);
    }

    public void HideDialoguePanel()
    {
        dialoguePanel.SetActive(false);
    }

    public void SetDialogueText(string dialogueString)
    {
        if(typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine = StartCoroutine(TypeText(dialogueString));
    }

    public void SetNameText(string nameString)
    {
        nameText.text = nameString;
    }

    private IEnumerator TypeText(string textToType)
    {
        dialogueText.text = textToType;
        dialogueText.maxVisibleCharacters = 0;

        yield return null;

        int totalCharacters = textToType.Length;
        int visibleCount = 0;

        while (visibleCount <= totalCharacters)
        {
            dialogueText.maxVisibleCharacters = visibleCount;
            visibleCount++;

            yield return new WaitForSeconds(timeBetweenCharacters);
        }
    }
}
