using Newtonsoft.Json.Linq;
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
        if(mixer == null && manager == null)
        {
            Init();
        }
    }
    private static void Init()
    {
        Debug.Log("Test");

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
        SetMixerVolume(PLaybackChannelList.Master, SaveAndLoadController.LoadSavedSetting(SettingNames.Master));
        SetMixerVolume(PLaybackChannelList.Effect, SaveAndLoadController.LoadSavedSetting(SettingNames.Effect));
        SetMixerVolume(PLaybackChannelList.Music, SaveAndLoadController.LoadSavedSetting(SettingNames.Music));
    }
    #endregion

    #region Playback
    public static void PlaySound2D(AudioClip clip, PLaybackChannelList channel, float volume = 1.0f, bool loop = false)
    {
        if (clip == null)
        {
            Debug.LogError("ERROR - Cannot play a null clip.");
            return;
        }

        CheckInit();

        manager.PlaySound2D(clip, channel, volume, loop);
    }
    public static void PlaySound3D(AudioClip clip, PLaybackChannelList channel, Vector3 pos, float volume = 1.0f, bool loop = false)
    {
        if (clip == null)
        {
            Debug.LogError("ERROR - Cannot play a null clip.");
            return;
        }

        CheckInit();

        manager.PlaySound3D(clip, channel, volume, pos, loop);
    }
    public static void StopAllChannel(PLaybackChannelList channel)
    {
        CheckInit();

        manager.StopChannelAll(channel);
    }
    #endregion

    #region Mixer
    public static void SetMixerVolume(PLaybackChannelList channel, int amount)
    {
        CheckInit();

        string name = channel + "Volume";

        float value = amount / 10.0f;

        if(value == 0)
        {
            value = 0.0001f;
        }

        mixer.SetFloat(name, Mathf.Log10(value) * 20);
    }
    #endregion
}
