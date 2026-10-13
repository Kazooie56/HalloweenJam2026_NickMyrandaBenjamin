using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerBalloonDropping : MonoBehaviour
{
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private InputActionReference moveAction;
    [SerializeField] private Balloon balloonPrefab;
    [SerializeField] private float cooldown = 1f;
    public bool isUnlocked = false;     // unlocks after you talk to the clown in the lobby a second time

    private PlayerMovement movement;
    private float nextDropTime;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }
    private void Start()
    {
        CheckifUnlocked();
    }
    private void OnEnable()
    {
        attackAction.action.Enable();
        moveAction.action.Enable();
        ProgressManager.onChanged += CheckifUnlocked;
    }

    private void OnDisable()
    {
        attackAction.action.Disable();
        moveAction.action.Disable();
        ProgressManager.onChanged -= CheckifUnlocked;
    }

    // Setting up this || or, means I can just toggle it in the Inspector
    private void CheckifUnlocked()
    {
        isUnlocked = isUnlocked || ProgressManager.Get(ProgressUnlocked.ClownTalked2);
    }

    private void Update()
    {
        if (!isUnlocked || !movement.canMove || PauseMenu.IsPaused)
        {
            return;
        }

        bool holdingDown = moveAction.action.ReadValue<Vector2>().y < -0.5f;

        if (holdingDown && attackAction.action.WasPressedThisFrame() && Time.time >= nextDropTime)
        {
            nextDropTime = Time.time + cooldown;
            Instantiate(balloonPrefab, transform.position, Quaternion.identity);
        }
    }
}
