using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CannonManager
{
    public static Dictionary<CannonPositions, CannonController> cannons = new Dictionary<CannonPositions, CannonController> ();

    #region Add / Remove
    public static void AddCannon(CannonController cannon, CannonPositions pos)
    {
        if(cannons.ContainsKey(pos))
        {
            Debug.LogError("ERROR - Cannon dict already has a cannon for given pos.");
            return;
        }

        cannons.Add(pos, cannon);
    }
    public static void RemoveAllCannons()
    {
        cannons.Clear();
    }
    #endregion

    #region States
    public static void SetCannonState(CannonPositions pos, bool value)
    {
        if(!cannons.ContainsKey(pos))
        {
            Debug.LogError("ERROR - No cannon in dictionary for given position.");
            return;
        }

        cannons[pos].gameObject.SetActive(value);
    }
    public static void SetAllState(bool value)
    {
        foreach(int name in Enum.GetValues(typeof(CannonPositions)))
        {
            if(!cannons.ContainsKey((CannonPositions)name))
            {
                continue;
            }

            cannons[(CannonPositions)name].gameObject.SetActive(value);
        }
    }
    #endregion
}

[System.Serializable]
public enum CannonPositions
{
    FirstLeft,
    SecondLeft,
    ThirdLeft,
    FourthLeft,
    FirstRight,
    SecondRight,
    ThirdRight,
    FourthRight
}
