using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameUIHandler : MonoBehaviour
{
    public GameObject GameUI;
    public GameObject PauseUI;
    public GameObject CannonUI;

    private void OnEnable()
    {
        ConnectEvents();
    }
    private void OnDisable()
    {
        DisconnectEvents();
    }
    private void ConnectEvents()
    {
        PlayerInputController.resume += Resume;
        PlayerInputController.pause += Pause;
        PlayerInputController.switchToPlayer += SwitchToPlayer;
        PlayerInputController.switchToCannon += SwitchToCannon;
    }
    private void DisconnectEvents()
    {
        PlayerInputController.resume -= Resume;
        PlayerInputController.pause -= Pause;
        PlayerInputController.switchToPlayer -= SwitchToPlayer;
        PlayerInputController.switchToCannon -= SwitchToCannon;
    }

    private void Start()
    {
        GameUI.SetActive(true);
        PauseUI.SetActive(false);
        CannonUI.SetActive(false);
    }

    private void Resume()
    {
        GameUI.SetActive(true);
        PauseUI.SetActive(false);
    }
    private void Pause()
    {
        GameUI.SetActive(false);
        PauseUI.SetActive(true);
    }
    private void SwitchToPlayer()
    {
        CannonUI.SetActive(false);
    }
    private void SwitchToCannon()
    {
        CannonUI.SetActive(true);
    }
}
