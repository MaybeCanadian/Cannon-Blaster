using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour
{
    #region Member Variables
    [Header("Cannon Parts")]
    public Transform cannonBarrel;
    public Transform firePoint;
    public Transform barrelBase;

    [Header("Cameras")]
    public Transform barrelCam;
    public Transform railingCam;

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
    private Vector3 startingYawOffset;

    [Header("Firing")]
    public PooledObjects projectile;
    public float fireForce = 100.0f;
    public float fireDelay = 1.0f;
    private float fireTimer = 0.0f;

    private Vector2 moveInput = Vector2.zero;
    #endregion

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
        if(fireTimer < fireDelay)
        {
            return;
        }

        fireTimer = 0.0f;

        GameObject ball = ObjectPoolManager.GetObjectFromPool(projectile);

        if (ball == null)
        {
            Debug.LogError("ERROR - Cannon ball to fire was null.");
            return;
        }

        ball.SetActive(true);

        Rigidbody ballRB = ball.gameObject.GetComponent<Rigidbody>();

        if(ballRB == null)
        {
            ObjectPoolManager.ReturnObjectToPool(ball, projectile);
            Debug.LogError("ERROR - Ball has no rigidbody.");
            return;
        }

        Vector3 direction = (firePoint.position - barrelBase.position).normalized;

        ball.transform.position = firePoint.position;

        ballRB.AddForce(direction * fireForce, ForceMode.Impulse);
    }
    private void FireTimerTick(float delta)
    {
        if(fireTimer < fireDelay)
        {
            fireTimer += delta;
        }
    }
    #endregion

    #region Input
    public void SetMoveInput(Vector2 input)
    {
        moveInput = input;
    }
    public Transform GetCameraPos(CameraPos posType)
    {
        switch(posType)
        {
            case CameraPos.Barrel:
                return barrelCam;
            case CameraPos.Railing:
                return railingCam;
        }

        return null;
    }
    #endregion

    #region Update
    private void Update()
    {
        ChangePitch(moveInput.y, Time.deltaTime);
        ChangeYaw(moveInput.x, Time.deltaTime);

        FireTimerTick(Time.deltaTime);
    }
    #endregion
}

[System.Serializable]
public enum CameraPos
{
    Barrel,
    Railing
}
