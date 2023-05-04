using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController charController;
    public Transform head;

    public Vector3 velocity = Vector3.zero;

    [Header("Movement")]
    public float moveSpeed = 1.0f;
    public float gravity = 1.0f;
    public float friction = 0.9f;
    public float zeroPoint = 0.01f;

    [Header("Pitch")]
    public float cameraPitch = 0.0f;
    public float pitchSpeed = 10.0f;
    public float maxPitch = 90.0f;
    public float minPitch = -90.0f;

    [Header("Rotation")]
    public float bodyRotation = 0.0f;
    public float rotateSpeed = 10.0f;

    [Header("Jumping")]
    public float jumpForce = 100.0f;

    private Vector2 input = Vector2.zero;

    private void Awake()
    {
        charController = GetComponent<CharacterController>();
    }

    public void MoveCharacter(Vector2 direction, float delta)
    {
        if (charController == null)
        {
            Debug.LogError("ERROR - Could not find character controller");
            return;
        }
        //movement

        velocity += transform.forward * direction.y * moveSpeed * delta;
        velocity += transform.right * direction.x * moveSpeed * delta;

        charController.Move(velocity * delta);

        velocity.x *= friction;
        if(Mathf.Abs(velocity.x) <= zeroPoint)
        {
            velocity.x = 0.0f;
        }

        velocity.z *= friction; 
        if(Mathf.Abs(velocity.z) <= zeroPoint)
        {
            velocity.z = 0.0f;
        }

        if (!charController.isGrounded)
        {
            velocity.y -= gravity * delta;
        }
        else
        {
            velocity.y = 0.0f;
        }

        //Debug.Log(input);
    }
    public void RotateView(Vector2 direction, float delta)
    {
        cameraPitch -= direction.y * delta * pitchSpeed;

        bodyRotation += direction.x * delta * rotateSpeed;

        cameraPitch = Mathf.Clamp(cameraPitch, minPitch, maxPitch);

        head.localRotation = Quaternion.Euler(cameraPitch, 0.0f, 0.0f);

        bodyRotation %= 360;

        transform.rotation = Quaternion.Euler(0.0f, bodyRotation, 0.0f);
    }
    public void Jump()
    {
        if(charController.isGrounded)
        {
            velocity.y += jumpForce;
        }
    }

    #region Debug Control
    private void FixedUpdate()
    {
        MoveCharacter(input, Time.fixedDeltaTime);

        DetermineMouse(Time.fixedDeltaTime);
    }
    private void LateUpdate()
    {
        DetermineMouse(Time.smoothDeltaTime);
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    private void DetermineMouse(float delta)
    {
        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * Time.smoothDeltaTime;

        RotateView(mouseDelta, delta);
    }
    public void OnMoveInput(InputAction.CallbackContext context)
    {
        input = context.ReadValue<Vector2>().normalized;
    }
    public void OnJumpInput(InputAction.CallbackContext context)
    {
        Jump();
    }
    #endregion
}
