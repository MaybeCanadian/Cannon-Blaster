using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneChanger
{
    public static void GoToGameFromMain()
    {
        CannonManager.RemoveAllCannons();

        TargetSpawner.ResetManager();

        ObjectPoolManager.ResetManager();

        SceneManager.LoadScene("MainGame");

    }
    public static void GoToMainFromGame()
    {
        CannonManager.RemoveAllCannons();

        TargetSpawner.ResetManager();

        ObjectPoolManager.ResetManager();

        SceneManager.LoadScene("Menu");

    }
    public static void GoToMainFromOptions()
    {
        SceneManager.LoadScene("Menu");
    }
    public static void GoToOptionsFromMain()
    {
        SceneManager.LoadScene("Options");
    }
}
