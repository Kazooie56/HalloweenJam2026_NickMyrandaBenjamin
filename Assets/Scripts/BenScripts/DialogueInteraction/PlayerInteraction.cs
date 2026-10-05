using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private IInteractable target;
    [SerializeField] private GameObject targetInteractable;

    private UIManager uiManager;

    private void Start()
    {
        uiManager = ServiceHub.Instance.UIManager;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable foundInteractable))
        {
            target = foundInteractable;
            targetInteractable = other.gameObject;

            //maybe show what button to press?
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.TryGetComponent(out IInteractable foundInteractable))
        {
            target = foundInteractable;
            targetInteractable = other.gameObject;

            // hide maybe prompt
        }
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (target != null)
            {
                //hide maybe prompt 
                
                target.Interact();
            }
        }
    }
}
