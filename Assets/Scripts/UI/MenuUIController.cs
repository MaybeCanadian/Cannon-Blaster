using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuUIController : MonoBehaviour
{
    private void Start()
    {
        AudioManager.OutSideInit();
    }
    public void OnPlayButtonPress()
    {
        SceneChanger.GoToGameFromMain();
    }
    public void OnOptionsButtonPress()
    {
        SceneChanger.GoToOptionsFromMain();
    }
    public void OnQuitButtonPress()
    {
        Application.Quit();
    }
}
