using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SaveAndLoadController
{
    public static int LoadSavedSetting(SettingNames setting)
    {
        //Debug.Log("loaded");

        return PlayerPrefs.GetInt(setting.ToString(), 5);
    }
    public static void SaveLoadedSetting(SettingNames setting, int value)
    {
        PlayerPrefs.SetInt(setting.ToString(), value);

        PlayerPrefs.Save();
    }
}

[System.Serializable]
public enum SettingNames
{
    Master,
    Effect,
    Music
}
