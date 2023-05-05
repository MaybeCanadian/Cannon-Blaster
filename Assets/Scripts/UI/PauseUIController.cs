using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseUIController : MonoBehaviour
{
    public void OnPauseButtonPressed()
    {
        SceneChanger.GoToMainFromGame();
    }
}
