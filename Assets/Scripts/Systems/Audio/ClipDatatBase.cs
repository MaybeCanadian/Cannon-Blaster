using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClipDatatBase
{
    public static Dictionary<ClipListNames, ClipList> clipListDict = null;
    public static Dictionary<TrackList, AudioClip> trackDict = null;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();
    }
    private static void CheckInit()
    {
        if(clipListDict == null)
        {
            Init();
        }
    }
    private static void Init()
    {
        LoadClipLists();

        LoadTrackList();
    }
    private static void LoadClipLists()
    {
        clipListDict = new Dictionary<ClipListNames, ClipList>();

        foreach(int name in Enum.GetValues(typeof(ClipListNames)))
        {
            ClipList list = Resources.Load<ClipList>("Audio/Clips/" + (ClipListNames)name);

            if(list == null)
            {
                continue;
            }

            clipListDict.Add((ClipListNames)name, list);
        }
    }
    private static void LoadTrackList()
    {
        trackDict = new Dictionary<TrackList, AudioClip>();

        foreach(int name in Enum.GetValues(typeof(TrackList)))
        {
            AudioClip track = Resources.Load<AudioClip>("Audio/Tracks/" + (TrackList)name);

            if (track == null)
            {
                continue;
            }

            trackDict.Add((TrackList)name, track);
        }
    }
    #endregion

    #region Clip Control
    public static ClipList GetList(ClipListNames name)
    {
        CheckInit();

        if(!clipListDict.ContainsKey(name))
        {
            Debug.LogError("ERROR - Clip List Dict does not have a value for the given name.");
            return null;
        }

        return clipListDict[name];
    }
    public static AudioClip GetTrack(TrackList name)
    {
        CheckInit();

        if(!trackDict.ContainsKey(name))
        {
            Debug.LogError("ERROR - No Track exists in track dict for given name.");
            return null;
        }

        return trackDict[name]; 
    }
    #endregion
}

[System.Serializable]
public enum ClipListNames 
{
    CannonFire,
    WaterSplash,
    BoatHit
}

[System.Serializable]
public enum TrackList
{
    Skirmish,
    Fearless
}

