using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsUIController : MonoBehaviour
{
    int masterVolume;
    int effectVolume;
    int musicVolume;



    private void Start()
    {
        masterVolume = SaveAndLoadController.LoadSettng(SettingNames.Master);
        effectVolume = SaveAndLoadController.LoadSettng(SettingNames.Effect);
        musicVolume = SaveAndLoadController.LoadSettng(SettingNames.Music);
    }

    #region Button Events
    public void OnBackButtonPressed()
    {
        SceneChanger.GoToMainFromOptions();
    }

    #region Master
    public void OnMasterUpPressed()
    {
        masterVolume++;

        masterVolume = Mathf.Min(masterVolume, 10);

        AudioManager.SetMixerVolume(PLaybackChannelList.Master, masterVolume);

        SaveAndLoadController.SaveSetting(SettingNames.Master, masterVolume);
    }
    public void OnMasterDownPressed()
    {
        masterVolume--;

        masterVolume = Mathf.Max(masterVolume, 0);

        AudioManager.SetMixerVolume(PLaybackChannelList.Master, masterVolume);

        SaveAndLoadController.SaveSetting(SettingNames.Master, masterVolume);
    }
    #endregion

    #region Effect
    public void OnEffectsUpPressed()
    {
        effectVolume++;

        effectVolume = Mathf.Min(effectVolume, 10);

        AudioManager.SetMixerVolume(PLaybackChannelList.Effect, effectVolume);

        SaveAndLoadController.SaveSetting(SettingNames.Effect, effectVolume);

    }
    public void OnEffectDownPressed()
    {
        effectVolume--;

        effectVolume = Mathf.Max(effectVolume, 0);

        AudioManager.SetMixerVolume(PLaybackChannelList.Effect, effectVolume);

        SaveAndLoadController.SaveSetting(SettingNames.Effect, effectVolume);
    }
    #endregion

    #region Music
    public void OnMusicUpPressed()
    {
        musicVolume++;

        musicVolume = Mathf.Min(musicVolume, 10);

        AudioManager.SetMixerVolume(PLaybackChannelList.Music, musicVolume);

        SaveAndLoadController.SaveSetting(SettingNames.Music, musicVolume);

    }
    public void OnMusicDownPressed() 
    {
        musicVolume--;

        musicVolume = Mathf.Max(musicVolume, 0);

        AudioManager.SetMixerVolume(PLaybackChannelList.Music, musicVolume);

        SaveAndLoadController.SaveSetting(SettingNames.Music, musicVolume);

    }
    #endregion

    #endregion
}
