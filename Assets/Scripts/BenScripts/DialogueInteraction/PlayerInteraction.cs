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
            target = null;
            targetInteractable = null;

            // hide maybe prompt
        }
    }

    private void Update()
    {
        if (interactAction.action.WasPressedThisFrame())
        {
            if (target != null)
            {
                //hide maybe prompt 

                target.Interact();
            }
        }
    }
}
