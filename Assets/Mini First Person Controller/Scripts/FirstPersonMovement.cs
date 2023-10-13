using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class FirstPersonMovement : MonoBehaviour
{
    [SerializeField] float speed = 5;

    [Header("Running")]
    [SerializeField] bool canRun = true;
    [SerializeField] float runSpeed = 9;
    [SerializeField] KeyCode runningKey = KeyCode.LeftShift;
    [SerializeField] float maxStamina = 100;
    [SerializeField] float substractStamina = .5f;
    

    Rigidbody rigidbody;
    /// <summary> Functions to override movement speed. Will use the last added override. </summary>
    public List<System.Func<float>> speedOverrides = new List<System.Func<float>>();

    private float h, v;

    public float CurrentStamina { private set; get; }
    public bool IsRunning { get; private set; }

    void Awake()
    {
        // Get the rigidbody on this.
        rigidbody = GetComponent<Rigidbody>();
        InvokeRepeating(nameof(SubstractStamina), 1, 1);
    }

    void FixedUpdate()
    {
        // Get targetMovingSpeed.
        float targetMovingSpeed = IsRunning ? runSpeed : speed;
        if (speedOverrides.Count > 0)
        {
            targetMovingSpeed = speedOverrides[speedOverrides.Count - 1]();
        }

        // Get targetVelocity from input.
        Vector2 targetVelocity =new Vector2(h * targetMovingSpeed, v * targetMovingSpeed);

        // Apply movement.
        rigidbody.velocity = transform.rotation * new Vector3(targetVelocity.x, rigidbody.velocity.y, targetVelocity.y);
    }
    public void InputMove(InputAction.CallbackContext context)
    {
        Vector2 move = context.ReadValue<Vector2>();
        h = move.x;
        v = move.y;
    }
    public void InputRun(InputAction.CallbackContext context)
    {
        // Update IsRunning from input.
        IsRunning = canRun && context.performed;
    }
    private void SubstractStamina()
    {
        if (!IsRunning)
            return;

        CurrentStamina -= substractStamina;
    }
}