using System;
using UnityEngine;
public class ServiceHub : MonoBehaviour
{
    public static ServiceHub Instance { get; private set; }

    // holds references to important things
    [SerializeField] private UIManager uiManager;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject player;

    public UIManager UIManager => uiManager;
    public DialogueManager DialogueManager => dialogueManager;
    public GameObject Player => player;

    // instead of destroying every new ServiceHub, it destroys every old ServiceHub.
    // so we can use each scene's references
    private void Awake()
    {
        Instance = this;
    }
    private void OnDestroy()
    {
        if (Instance == this) Instance = null;
    }
}