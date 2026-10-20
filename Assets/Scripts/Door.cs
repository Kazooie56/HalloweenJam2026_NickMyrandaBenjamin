using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    // if you serialize an Enum, you get a convenient dropdown menu in the inspector.
    [SerializeField] private SceneLoader.GameScene destination;
    [SerializeField] private bool isDisabled;

    [Header("Progress")]
    [SerializeField] private ProgressUnlocked unlockRequirement = ProgressUnlocked.None; // this is for unlocking level 1
    [SerializeField] private ProgressUnlocked unlockOnEnter = ProgressUnlocked.None; // this is for swapping the clown

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private InteractPrompt prompt;

    [Header("Sprites")]
    [SerializeField] private Sprite closedSprite;
    [SerializeField] private Sprite openSprite;

    private void OnEnable()
    {
        ProgressManager.onChanged += Refresh;
    }

    private void OnDisable()
    {
        ProgressManager.onChanged -= Refresh;
    }

    private void Start()
    {
        Refresh();
    }
    private void SetDisabled(bool disabled)
    {
        // doors that should be enabled begin enabled, those that shouldn't arent.
        isDisabled = disabled;
        UpdateDoor();
    }

    private void Refresh()
    {
        // if the door needs a specific unlocked requirement, then it's locked until you get that requirement
        if (unlockRequirement != ProgressUnlocked.None)
        {
            isDisabled = !ProgressManager.Get(unlockRequirement);
        }
        UpdateDoor();
    }

    public void Interact()
    {
        if (isDisabled)
        {
            return;
        }

        ProgressManager.Set(unlockOnEnter);
        SceneLoader.Load(destination);
    }

    private void UpdateDoor()
    {
        if (isDisabled)
        {
            spriteRenderer.sprite = closedSprite;
        }
        else
        {
            spriteRenderer.sprite = openSprite;
        }

        if (prompt != null)
        {
            prompt.CanShow = !isDisabled;

            if (isDisabled)
            {
                prompt.SetVisible(false);
            }
        }
    }
}