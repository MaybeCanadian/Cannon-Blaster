using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    public void OnPlayButtonPress()
    {
        SceneManager.LoadScene("MainGame");
    }
    public void OnOptionsButtonPress()
    {
        SceneManager.LoadScene("Options");
    }
    public void OnQuitButtonPress()
    {
        Application.Quit();
    }
}
