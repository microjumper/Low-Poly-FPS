using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float mouseSensitivity = 10f;

    // refereces
    private Camera mainCamera;
    private new Rigidbody rigidbody;

    private Vector2 movementDirection;
    private Vector2 cameraRotation;

    private void Awake()
    {
        mainCamera = Camera.main;
        rigidbody = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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
        // Using the camera's right and forward vectors ensures movement is relative to the camera's direction.
        Vector3 moveDirection = (mainCamera.transform.right * movementDirection.x + mainCamera.transform.forward * movementDirection.y).normalized;

        // Preserve the vertical component of the rigidbody's velocity (gravity, jump) while applying horizontal movement.
        moveDirection.y = rigidbody.linearVelocity.y;

        // Apply the movement direction to the rigidbody's linear velocity, scaling it by the movement speed.
        rigidbody.linearVelocity = moveDirection * speed;
    }


    private void Look()
    {
        transform.Rotate(cameraRotation.x * mouseSensitivity * Time.fixedDeltaTime * Vector3.up);
    }
}
