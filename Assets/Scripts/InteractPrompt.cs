using TMPro;
using UnityEngine;

public class InteractPrompt : MonoBehaviour
{
    [SerializeField] private TMP_Text interactPrompt;

    // Only exist for doors, NPC's will always be able to show
    public bool CanShow = true;

    private void Awake()
    {
        // if prompt doesn't exist, find the text component on the object and make it invisible
        if (interactPrompt == null)
        {
            interactPrompt = GetComponent<TMP_Text>();
        }

        SetVisible(false);
    }

    public void SetVisible(bool visible)
    {
        if (CanShow == false)
        {
            interactPrompt.enabled = false;
            return;
        }

        interactPrompt.enabled = visible;
    }
}