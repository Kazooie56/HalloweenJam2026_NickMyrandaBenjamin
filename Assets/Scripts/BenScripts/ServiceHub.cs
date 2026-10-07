using System;
using UnityEngine;

public class ServiceHub : MonoBehaviour
{
    public static ServiceHub Instance { get; private set; }

    [SerializeField] private UIManager uiManager;
    [SerializeField] private DialogueManager dialogueManager;
    [SerializeField] private GameObject player;


    public UIManager UIManager => uiManager;
    public DialogueManager DialogueManager => dialogueManager;
    public GameObject Player => player;

    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
        }

        else if (Instance != null)
        {
            Destroy(gameObject);
        }
    }


}
