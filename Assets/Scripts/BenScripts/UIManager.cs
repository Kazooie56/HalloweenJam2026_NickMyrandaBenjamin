using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    //Dialogue
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text nameText;

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
        dialogueText.text = dialogueString;
    }

    public void SetNameText(string nameString)
    {
        nameText.text = nameString;
    }
}
