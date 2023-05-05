using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameUIHandler : MonoBehaviour
{
    public GameObject GameUI;
    public GameObject PauseUI;
    public GameObject CannonUI;
    public GameObject PlayerUI;

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
        PlayerUI.SetActive(true);
    }

    private void Resume()
    {
        GameUI.SetActive(true);
        PauseUI.SetActive(false);
        PlayerUI.SetActive(true);
    }
    private void Pause()
    {
        GameUI.SetActive(false);
        PauseUI.SetActive(true);
        PlayerUI.SetActive(false);
    }
    private void SwitchToPlayer()
    {
        CannonUI.SetActive(false);
        PlayerUI.SetActive(true);
    }
    private void SwitchToCannon()
    {
        CannonUI.SetActive(true);
        PlayerUI.SetActive(false);
    }
}
