using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    // if you serialize an Enum, you get a convenient dropdown menu in the inspector.
    [SerializeField] private SceneLoader.GameScene destination;
    [SerializeField] private bool isDisabled;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private InteractPrompt prompt;

    private void Start()
    {
        // doors that should be enabled begin enabled, those that shouldn't arent.
        UpdateDoor();
    }

    // Add a door enabler soon. For now this is unused
    public void SetDisabled(bool disabled)
    {
        isDisabled = disabled;
        UpdateDoor();
    }

    public void Interact()
    {
        if (isDisabled)
        {
            return;
        }

        SceneLoader.Load(destination);
    }

    private void UpdateDoor()
    {
        if (isDisabled)
        {
            spriteRenderer.color = new Color32(0, 0, 0, 150); // completely black and half transparent
            prompt.CanShow = false;
        }
        else
        {
            spriteRenderer.color = new Color32(0, 0, 0, 255); // no transparency
            prompt.CanShow = true;
        }
    }
}