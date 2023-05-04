using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    public CannonController cannon;
    public PlayerMovement player;
    public PlayerInput input;

    public Vector2 moveInput = Vector2.zero;

    public PlayerControlMode currentMode = PlayerControlMode.Player;

    #region Init Functions
    private void Start()
    {
        player = GetComponent<PlayerMovement>();
        input = GetComponent<PlayerInput>();

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
    }
    #endregion

    #region Player
    private void HandlePlayerUpdate()
    {
        if (!CheckPlayerMode())
        {
            return;
        }

        //DeterminePlayerMouse();
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


        Debug.Log("Interact");
        //try posses cannon
    }
    private void DeterminePlayerMouse()
    {
        if (!CheckPlayerMode())
        {
            return;
        }

        Vector2 mouseDelta = Mouse.current.delta.ReadValue() * Time.smoothDeltaTime;

        player.SetLookInput(mouseDelta);
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

        input.SwitchCurrentActionMap("Player");
    }
    public void SwitchToCannon()
    {
        if(!CheckAnyValid())
        {
            return;
        }

        currentMode = PlayerControlMode.Cannon;

        input.SwitchCurrentActionMap("Cannon");
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
