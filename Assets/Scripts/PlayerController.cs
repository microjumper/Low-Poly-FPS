using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float mouseSensitivity = 10f;

    private new Rigidbody rigidbody;

    private Vector2 movementDirection;
    private Vector2 cameraRotation;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        movementDirection = Vector2.zero;
        cameraRotation = Vector2.zero;
    }

    private void Update()
    {

    }

    private void FixedUpdate()
    {
        Move();
        Look();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                movementDirection = context.ReadValue<Vector2>();
                break;
            case InputActionPhase.Canceled:
                movementDirection = Vector2.zero;
                break;
        }
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Performed:
                cameraRotation = context.ReadValue<Vector2>();
                break;
            case InputActionPhase.Canceled:
                cameraRotation = Vector2.zero;
                break;
        }
    }

    private void Move()
    {
        rigidbody.linearVelocity = new (movementDirection.x * speed, rigidbody.linearVelocity.y, movementDirection.y * speed);
    }

    private void Look()
    {
        transform.Rotate(cameraRotation.x * mouseSensitivity * Time.fixedDeltaTime * Vector3.up);
    }
}
