using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ClipDatatBase
{
    public static Dictionary<ClipListNames, ClipList> clipListDict = null;

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
    #endregion
}

[System.Serializable]
public enum ClipListNames 
{
    CannonFire,
    WaterSplash,
    BoatHit
}

