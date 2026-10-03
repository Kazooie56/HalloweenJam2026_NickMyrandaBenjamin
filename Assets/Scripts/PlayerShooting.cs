using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField] private InputActionReference attackAction;
    [SerializeField] private PieProjectile piePrefab;
    [SerializeField] private float cooldown = 0.3f;

    private PlayerMovement movement;
    private float nextShotTime;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        attackAction.action.Enable();
    }

    private void OnDisable()
    {
        attackAction.action.Disable();
    }

    private void Update()
    {
        // if you shoot and it hasn't been too soon
        // you must use Time.time, Time.deltaTime is broken
        if (attackAction.action.WasPressedThisFrame() && Time.time >= nextShotTime)
        {
            // start cooldown for shooting and shoot
            nextShotTime = Time.time + cooldown;
            Shoot();
        }
    }

    private void Shoot()
    {
        // either moves in the negative or positive x direction based on facingDirection
        // the y is 0f because it moves straight
        Vector2 direction = new Vector2(movement.facingDirection, 0f);

        // This just makes the pie, and uses pie's Launch method for the movement.
        PieProjectile pie = Instantiate(piePrefab, transform.position, Quaternion.identity);
        pie.Launch(direction);
    }
}