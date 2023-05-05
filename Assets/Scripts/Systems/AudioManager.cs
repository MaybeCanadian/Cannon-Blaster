using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AudioManager
{
    static PlaybackManager manager = null;
    static int startAmount = 10;

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
        GameObject managerOBJ = new GameObject();
        managerOBJ.name = "[Audio]";

        manager = managerOBJ.AddComponent<PlaybackManager>();

        GameObject.DontDestroyOnLoad(managerOBJ);

        manager.SetUpPlaybackManager(startAmount);
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
}
