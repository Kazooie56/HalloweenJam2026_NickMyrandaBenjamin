using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private InputActionReference moveAction; // only for checking if we should place balloon or throw pie.
    [SerializeField] private PieProjectile piePrefab;
    [SerializeField] private float fireRate = 0.3f;

    private PlayerMovement movement;
    private float nextShotTime;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        attackAction.action.Enable();
        moveAction.action.Enable();
    }

    private void OnDisable()
    {
        attackAction.action.Disable();
        moveAction.action.Disable();
    }

    private void Update()
    {
        // only time canMove is disabled is when we deactivate all player control,
        // so a movement check here does the job.
        if (!movement.canMove || PauseMenu.IsPaused) return;

        bool holdingDown = moveAction.action.ReadValue<Vector2>().y < -0.5f;
        if (holdingDown) return;

        // if you shoot and it hasn't been too soon
        // you must use Time.time, Time.deltaTime is broken
        if (attackAction.action.WasPressedThisFrame() && Time.time >= nextShotTime)
        {
            // start cooldown for shooting and shoot
            nextShotTime = Time.time + fireRate;
            Shoot();
        }
    }

    private void Shoot()
    {
        // either moves in the negative or positive x direction based on facingDirection
        // the y is 0f because it moves in a straight line
        Vector2 direction = new Vector2(movement.facingDirection, 0f);

        Quaternion rotation;
        if (movement.facingDirection > 0f) // if you're looking right
        {
            rotation = Quaternion.identity; // the rotation will be to the right
        }
        else
        {
            rotation = Quaternion.Euler(0f, 180f, 0f); // pie is rotated 180 degrees
        }

        // This just makes the pie, and uses pie's Launch method for the movement.
        PieProjectile pie = Instantiate(piePrefab, transform.position, rotation);
        pie.Launch(direction);
    }
}