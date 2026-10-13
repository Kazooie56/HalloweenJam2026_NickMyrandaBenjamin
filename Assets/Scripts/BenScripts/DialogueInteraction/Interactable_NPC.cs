using UnityEngine;

public class Interactable_NPC : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueManager dialogueManager;

    [Header("Name")]
    public string characterName;

    [Header("Dialogue")]
    [TextArea] public string[] sentences;

    [Header("Progress")]
    [SerializeField] private ProgressUnlocked unlock = ProgressUnlocked.None;

    private bool startRequested;   // we called DialogueManager's StartDialogue
    private bool talking;

    private void Start()
    {
        dialogueManager = ServiceHub.Instance.DialogueManager;

        if (dialogueManager == null) Debug.LogError("DialogueManager not found in serviceHub");
    }

    public void Interact()
    {
        if (dialogueManager.inDialogue == true)
        {
            dialogueManager.DisplayNextString();
        }

        else
        {
            dialogueManager.StartDialogue(characterName, sentences);
            startRequested = true;
        }
    }

    private void Update()
    {
        if (dialogueManager == null)
        {
            return;
        }
        // this means we're in a conversation
        if (startRequested && dialogueManager.inDialogue)
        {
            startRequested = false;
            talking = true;
        }
        
        // when the conversation is over, updates progress.
        if (talking && !dialogueManager.inDialogue)
        {
            talking = false;
            ProgressManager.Set(unlock);
        }
    }
}