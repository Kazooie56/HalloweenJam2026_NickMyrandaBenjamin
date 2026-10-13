using UnityEngine;

public class Door : MonoBehaviour, IInteractable
{
    // if you serialize an Enum, you get a convenient dropdown menu in the inspector.
    [SerializeField] private SceneLoader.GameScene destination;
    [SerializeField] private bool isDisabled;

    [Header("Progress")]
    [SerializeField] private ProgressUnlocked unlockedByFlag = ProgressUnlocked.None; // this is for unlocking level 1
    [SerializeField] private ProgressUnlocked flagToSetOnEnter = ProgressUnlocked.None; // this is for swapping the clown

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
        if (unlockedByFlag != ProgressUnlocked.None)
        {
            isDisabled = !ProgressManager.Get(unlockedByFlag);
        }
        UpdateDoor();
    }

    public void Interact()
    {
        if (isDisabled)
        {
            return;
        }

        ProgressManager.Set(flagToSetOnEnter);
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