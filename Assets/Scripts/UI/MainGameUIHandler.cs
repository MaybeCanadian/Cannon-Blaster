using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameUIHandler : MonoBehaviour
{
    public GameObject GameUI;
    public GameObject PauseUI;

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
    }
    private void DisconnectEvents()
    {
        PlayerInputController.resume -= Resume;
        PlayerInputController.pause -= Pause;
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
}
