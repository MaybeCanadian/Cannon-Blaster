using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public static class AudioManager
{
    static PlaybackManager manager = null;
    static int startAmount = 10;

    static AudioMixer mixer = null;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();
    }
    private static void CheckInit()
    {
        if(manager == null)
        {
            Init();
        }
    }
    private static void Init()
    {
        LoadMixer();

        LoadSavedValues();

        GameObject managerOBJ = new GameObject();
        managerOBJ.name = "[Audio]";

        manager = managerOBJ.AddComponent<PlaybackManager>();

        GameObject.DontDestroyOnLoad(managerOBJ);

        manager.SetUpPlaybackManager(startAmount, mixer);
    }
    private static void LoadMixer()
    {
        mixer = Resources.Load<AudioMixer>("Audio/Mixer");

        if (mixer == null)
        {
            Debug.LogError("ERROR - Could not load the mixer from resources.");
            return;
        }
    }
    private static void LoadSavedValues()
    {
        SetMixerVolume(PLaybackChannelList.Master, SaveAndLoadController.LoadSettng(SettingNames.Master));
        SetMixerVolume(PLaybackChannelList.Effect, SaveAndLoadController.LoadSettng(SettingNames.Effect));
        SetMixerVolume(PLaybackChannelList.Music, SaveAndLoadController.LoadSettng(SettingNames.Music));
    }
    #endregion

    #region Playback
    public static void PlaySounce2D(AudioClip clip, PLaybackChannelList channel, float volume)
    {
        
    }
    public static void PlaySound3D(AudioClip clip, PLaybackChannelList channel, float volume, Vector3 pos)
    {

    }
    #endregion

    #region Mixer
    public static void SetMixerVolume(PLaybackChannelList channel, int amount)
    {
        string name = channel + "Volume";

        float volume = (20 - -80) / (amount / 10);

        mixer.SetFloat(name, volume);
    }
    #endregion
}
