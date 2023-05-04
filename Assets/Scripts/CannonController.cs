using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    #region Member Variables
    [Header("Cannon Parts")]
    public Transform cannonBarrel;
    public Transform cameraPos;

    [Header("Pitch")]
    [SerializeField, Tooltip("The current pitch of the cannon")]
    private float cannonPitch = 0.0f;
    [SerializeField, Tooltip("The max angle the cannon can pitch")]
    private float maxPitch = 90.0f;
    [SerializeField, Tooltip("The min angle the cannon can pitch")]
    private float minPitch = -45.0f;
    [SerializeField, Tooltip("The rate at which the pitch changes when input is pressed.")]
    private float pitchRate = 1.0f;

    [Header("Yaw")]
    [SerializeField, Tooltip("The current yaw of the cannon")]
    private float cannonYaw = 0.0f;
    [SerializeField, Tooltip("The max angle the cannon can rotate")]
    private float maxYaw = 45.0f;
    [SerializeField, Tooltip("The min angle the cannon can rotate")]
    private float minYaw = -45.0f;
    [SerializeField, Tooltip("The rate at which yaw changes when input is pressed.")]
    private float yawRate = 1.0f;
    #endregion

    public Vector3 startingYawOffset;

    private Vector2 moveInput = Vector2.zero;

    private void Start()
    {
        startingYawOffset = transform.localRotation.eulerAngles;
    }

    #region Cannon Functions
    public void ChangePitch(float direction, float delta)
    {
        cannonPitch -= pitchRate * direction * delta;

        ClampPitch();

        cannonBarrel.localRotation = Quaternion.Euler(cannonPitch, 0.0f, 0.0f);
    }
    public void ChangeYaw(float direction, float delta) 
    {
        cannonYaw += yawRate * direction * delta;

        ClampYaw();

        transform.localRotation = Quaternion.Euler(new Vector3(0.0f, cannonYaw, 0.0f) + startingYawOffset);
    }
    public void SetPitch(float value)
    {
        cannonPitch = value;

        ClampPitch();
    }
    public void SetYaw(float value)
    {
        ClampYaw();
    }
    private void ClampPitch()
    {
        cannonPitch = Mathf.Clamp(cannonPitch, minPitch, maxPitch);
    }
    private void ClampYaw()
    {
        cannonYaw = Mathf.Clamp(cannonYaw, minYaw, maxYaw);
    }
    #endregion

    #region Fire
    public void FireCannon()
    {
        Debug.Log("Fire");
    }
    #endregion

    #region Input
    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }
    #endregion

    #region Update
    private void Update()
    {
        ChangePitch(moveInput.y, Time.deltaTime);
        ChangeYaw(moveInput.x, Time.deltaTime);


    }
    #endregion
}
