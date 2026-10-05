using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> dialogueQueue;

    private UIManager uiManager;
    private PlayerInteraction playerInteraction;
    private PlayerMovement playerMovement;

    public bool inDialogue = false;

    private void Awake()
    {
        uiManager = ServiceHub.Instance.UIManager;

        
    }

}
