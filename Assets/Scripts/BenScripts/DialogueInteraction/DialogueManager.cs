using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> dialogueQueue;

    private UIManager uiManager;
    private PlayerInteraction playerInteraction;
    private PlayerMovement playerMovement;

    public bool inDialogue = false;

    private void Start()
    {
        uiManager = ServiceHub.Instance.UIManager;

        playerInteraction = ServiceHub.Instance.Player.GetComponent<PlayerInteraction>();

        playerMovement = ServiceHub.Instance.Player.GetComponent<PlayerMovement>();

        dialogueQueue = new Queue<string>();
    }

    public void StartDialogue(string name, string[] sentences)
    {
        uiManager.ShowDialoguePanel();

        inDialogue = true;
        playerMovement.canMove = false;

        uiManager.SetNameText(name);

        foreach (string currentString in sentences)
        {
            dialogueQueue.Enqueue(currentString);
        }

        DisplayNextString();
    }

    public void DisplayNextString()
    {
        if(dialogueQueue.Count == 0)
        {
            EndDialogue();
            return;
        }

        else if (dialogueQueue.Count > 0)
        {   
            uiManager.SetDialogueText(dialogueQueue.Dequeue());
        }
    }

    private void EndDialogue()
    {
        dialogueQueue.Clear();

        inDialogue = false;

        uiManager.HideDialoguePanel();

        playerMovement.canMove = true;
    }
}
