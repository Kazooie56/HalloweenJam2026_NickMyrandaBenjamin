using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [Header("Input Action Reference")]
    [SerializeField] private InputActionReference interactAction;

    [Header("Target ID")]
    [SerializeField] private GameObject targetInteractable;
    private IInteractable target;
    private InteractPrompt currentPrompt;

    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = ServiceHub.Instance.DialogueManager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable foundInteractable))
        {
            target = foundInteractable;
            targetInteractable = other.gameObject;

            // Each object with IInteractable will have a script in it's children that has the class InteractPrompt
            // clowns have talk, doors have enter, this sets that prompt as "currentPrompt".
            currentPrompt = other.GetComponentInChildren<InteractPrompt>();
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable foundInteractable))
        {
            // moved your logic into a "ClearTarget" Method with slight changes now that we have removable text.
            ClearTarget();
        }
    }

    private void ClearTarget()
    {
        // take the prompt of our character
        if (currentPrompt != null)
        {
            // hide it
            currentPrompt.SetVisible(false);
        }

        // back to square 1
        currentPrompt = null;
        target = null;
        targetInteractable = null;
    }
    private void Update()
    {
        // Pausing the game will pause the text
        if (PauseMenu.IsPaused)
        {
            return;
        }

        // If we have a currentPrompt
        if (currentPrompt != null)
        {
            // we check the dialogueManager if it exists AND
            // we are in a conversation, and it has a bool built in to check that.
            // Then keep it true.
            bool inDialogue = dialogueManager != null && dialogueManager.inDialogue;
            currentPrompt.SetVisible(inDialogue == false);
        }

        // combined the two if statements together
        if (interactAction.action.WasPressedThisFrame() && target != null)
        {
            target.Interact();
        }
    }
}
