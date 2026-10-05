using UnityEditor;
using UnityEngine;

public class Interactable_NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueManager dialogueManager;

    [Header("Name")]
    public string characterName;

    [Header("Dialogue")]
    [TextArea] public string[] sentences;

    private void Start()
    {
        dialogueManager = ServiceHub.Instance.DialogueManager;

        if (dialogueManager == null) Debug.LogError("DialogueManager not found in serviceHub");

    }

    public void Interact()
    {
        if(dialogueManager.inDialogue == true)
        {
            dialogueManager.DisplayNextString();
        }

        else
        {
            dialogueManager.StartDialogue(characterName, sentences);
        }
    }

}
