using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;
using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerInputController : MonoBehaviour
{
    [Header("Objects")]
    public CannonController cannon;
    public PlayerMovement player;
    public PlayerInput input;

    [Header("Object Checking")]
    public LayerMask cannonLayer;
    public float raycastCheckDist = 5.0f;
    public GameObject currentObject = null;

    [Header("Camera")]
    public CameraPos currentCameraType = CameraPos.Barrel;

    [Header("Inputs")]
    public PlayerControlMode currentMode = PlayerControlMode.Player;

    #region Init Functions
    private void Start()
    {
        player = GetComponent<PlayerMovement>();
        input = GetComponent<PlayerInput>();

        currentMode = PlayerControlMode.Player;
        CameraController.instance.AttachToObject(player.head);

        Cursor.lockState = CursorLockMode.Locked;
    }
    #endregion

    private void Update()
    {
        switch (currentMode)
        {
            case PlayerControlMode.Player:
                HandlePlayerUpdate();
                break;
            case PlayerControlMode.Cannon:
                HandleCannonUpdate();
                break;
        }
    }

    #region Cannon
    private void HandleCannonUpdate()
    {
        if (!CheckCannonMode())
        {
            return;
        }
    }
    public void OnCannonMoveInput(InputAction.CallbackContext context)
    {
        if (!CheckCannonMode())
        {
            return;
        }

        Vector2 moveInput = context.ReadValue<Vector2>();

        cannon.SetMoveInput(moveInput);
    } 
    public void OnCannonFireInput(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        if (!CheckCannonMode())
        {
            return;
        }

        cannon.FireCannon();
    }
    public void OnCannonLeaveInput(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        if (!CheckCannonMode())
        {
            return;
        }

        cannon = null;

        SwitchToPlayer();
    }
    public void OnCannonAngleSwitchInput(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        if (!CheckCannonMode())
        {
            return;
        }

        currentCameraType = (currentCameraType == CameraPos.Barrel) ? CameraPos.Railing : CameraPos.Barrel;

        CameraController.instance.AttachToObject(cannon.GetCameraPos(currentCameraType));
    }
    #endregion

    #region Player
    private void HandlePlayerUpdate()
    {

        if (!CheckPlayerMode())
        {
            return;
        }

        CheckInFront();
    }
    public void OnPlayerMoveInput(InputAction.CallbackContext context)
    {
        if (!CheckPlayerMode())
        {
            return;
        }

        Vector2 moveInput = context.ReadValue<Vector2>();

        player.SetMoveInput(moveInput);
    }
    public void OnPlayerJumpInput(InputAction.CallbackContext context)
    {
        if(!context.started)
        {
            return;
        }

        if(!CheckPlayerMode())
        {
            return;
        }

        player.Jump();
    }
    public void OnPlayerLookInput(InputAction.CallbackContext context)
    { 
        if (!CheckPlayerMode())
        {
            return;
        }

        Vector2 lookInput = context.ReadValue<Vector2>();

        player.SetLookInput(lookInput);
    }
    public void OnPlayerInteractInput(InputAction.CallbackContext context)
    {
        if (!context.started)
        {
            return;
        }

        if (!CheckPlayerMode())
        {
            return;
        }

        if(currentObject == null)
        {
            return;
        }

        CannonController cannonScript = currentObject.GetComponent<CannonController>();

        if(cannonScript == null)
        {
            Debug.Log("object is not a cannon");
            return;
        }

        cannon = cannonScript;

        SwitchToCannon();
    }
    private void CheckInFront()
    {

        if(!CheckPlayerMode())
        {
            return;
        }

        Vector3 startPos = player.head.transform.position;
        Vector3 direction = player.head.transform.forward;

        if(Physics.Raycast(startPos, direction, out RaycastHit hit, raycastCheckDist, cannonLayer))
        {
            Debug.DrawRay(startPos, direction, Color.green);
            currentObject = hit.collider.gameObject;
        }
        else
        {
            Debug.DrawRay(startPos, direction, Color.red);
            currentObject = null;
        }
    }
    #endregion

    #region Mode Switching
    public void SwitchToPlayer()
    {
        if(!CheckAnyValid())
        {
            return;
        }

        currentMode = PlayerControlMode.Player;

        CameraController.instance.AttachToObject(player.head);

        input.SwitchCurrentActionMap("Player");
    }
    public void SwitchToCannon()
    {
        if(!CheckAnyValid())
        {
            return;
        }

        currentMode = PlayerControlMode.Cannon;

        CameraController.instance.AttachToObject(cannon.GetCameraPos(currentCameraType));

        input.SwitchCurrentActionMap("Cannon");

        Debug.Log("switched");
    }
    private bool CheckAnyValid()
    {
        if (cannon == null && player == null)
        {
            currentMode = PlayerControlMode.NULL;

            input.SwitchCurrentActionMap("Debug");

            return false;
        }

        return true;
    }
    private bool CheckPlayerMode()
    {
        if (player == null)
        {
            Debug.LogError("ERROR - no player found");
            SwitchToCannon();
            return false;
        }

        return true;
    }
    private bool CheckCannonMode()
    {
        if (cannon == null)
        {
            Debug.Log("ERROR - No active cannon");
            SwitchToPlayer();
            return false;
        }

        return true;
    }
    #endregion

    #region Debug
    public void SwapMode(InputAction.CallbackContext context)
    {
        if(!context.started)
        {
            return;
        }

        if(currentMode == PlayerControlMode.Player)
        {
            SwitchToCannon();
            return;
        }

        SwitchToPlayer();
        return;
    }
    #endregion
}

[System.Serializable]
public enum PlayerControlMode
{
    NULL,
    Player,
    Cannon
}
