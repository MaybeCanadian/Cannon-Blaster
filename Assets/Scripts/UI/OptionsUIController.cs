using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionsUIController : MonoBehaviour
{
    int masterVolume;
    int effectVolume;
    int musicVolume;

    public TMP_Text masterText;
    public TMP_Text effectText;
    public TMP_Text musicText;

    private void Start()
    {
        masterVolume = SaveAndLoadController.LoadSavedSetting(SettingNames.Master);
        effectVolume = SaveAndLoadController.LoadSavedSetting(SettingNames.Effect);
        musicVolume = SaveAndLoadController.LoadSavedSetting(SettingNames.Music);

        UpdateMaster();
        UpdateEffect();
        UpdateMusic();
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

        UpdateMaster();
    }
    public void OnMasterDownPressed()
    {
        masterVolume--;

        masterVolume = Mathf.Max(masterVolume, 0);

        UpdateMaster();
    }
    private void UpdateMaster()
    {
        AudioManager.SetMixerVolume(PLaybackChannelList.Master, masterVolume);

        SaveAndLoadController.SaveLoadedSetting(SettingNames.Master, masterVolume);

        masterText.text = masterVolume.ToString();
    }
    #endregion

    #region Effect
    public void OnEffectsUpPressed()
    {
        effectVolume++;

        effectVolume = Mathf.Min(effectVolume, 10);

        UpdateEffect();

    }
    public void OnEffectDownPressed()
    {
        effectVolume--;

        effectVolume = Mathf.Max(effectVolume, 0);

        UpdateEffect();
    }
    private void UpdateEffect()
    {
        AudioManager.SetMixerVolume(PLaybackChannelList.Effect, effectVolume);

        SaveAndLoadController.SaveLoadedSetting(SettingNames.Effect, effectVolume);

        effectText.text = effectVolume.ToString();
    }
    #endregion

    #region Music
    public void OnMusicUpPressed()
    {
        musicVolume++;

        musicVolume = Mathf.Min(musicVolume, 10);

        UpdateMusic();
    }
    public void OnMusicDownPressed() 
    {
        musicVolume--;

        musicVolume = Mathf.Max(musicVolume, 0);

        UpdateMusic();
    }
    private void UpdateMusic()
    {
        AudioManager.SetMixerVolume(PLaybackChannelList.Music, musicVolume);

        SaveAndLoadController.SaveLoadedSetting(SettingNames.Music, musicVolume);

        musicText.text = musicVolume.ToString();
    }
    #endregion

    #endregion
}
