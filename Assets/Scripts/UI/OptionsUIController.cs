using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsUIController : MonoBehaviour
{
    public void OnBackButtonPressed()
    {
        SceneManager.LoadScene("Menu");
    }
}
