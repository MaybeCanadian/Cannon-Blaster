using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

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

    private Vector2 moveInput = Vector2.zero;

    [Header("Pitch")]
    public float cameraPitch = 0.0f;
    public float pitchSpeed = 10.0f;
    public float maxPitch = 90.0f;
    public float minPitch = -90.0f;

    [Header("Rotation")]
    public float bodyRotation = 0.0f;
    public float rotateSpeed = 10.0f;

    private Vector2 lookInput = Vector2.zero;
    private Vector3 startPos;

    [Header("Jumping")]
    public float jumpForce = 100.0f;

    #region Init Functions
    private void Awake()
    {
        Init();
    }
    private void Init()
    {
        charController = GetComponent<CharacterController>();

        startPos = transform.position;
    }
    #endregion

    #region Movement
    public void MoveCharacter(float delta)
    {
        if (charController == null)
        {
            Debug.LogError("ERROR - Could not find character controller");
            return;
        }
        //movement

        velocity += transform.forward * moveInput.y * moveSpeed * delta;
        velocity += transform.right * moveInput.x * moveSpeed * delta;

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
    public void RotateView(float delta)
    {
        cameraPitch -= lookInput.y * delta * pitchSpeed;

        bodyRotation += lookInput.x * delta * rotateSpeed;

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
    #endregion

    #region Update
    private void FixedUpdate()
    {
        MoveCharacter(Time.fixedDeltaTime);
    }
    private void LateUpdate()
    {
        RotateView(Time.smoothDeltaTime);
    }
    #endregion

    #region Inputs
    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }
    public void SetLookInput(Vector2 input)
    {
        lookInput = input;

        //Debug.Log(lookInput);
    }
    #endregion

    private void OnTriggerEnter(Collider other)
    {
        WaterSplashPlane splash = other.GetComponent<WaterSplashPlane>();

        if(splash != null)
        {
            charController.enabled = false;

            transform.position = startPos;

            charController.enabled = true;

            ClipList list = ClipDatatBase.GetList(ClipListNames.WaterSplash);

            if(list == null)
            {
                return;
            }

            AudioClip clip = list.GetClip(true);

            if(clip == null)
            {
                return;
            }

            AudioManager.PlaySound2D(clip, PLaybackChannelList.Effect);

            return;
        }
    }
}
