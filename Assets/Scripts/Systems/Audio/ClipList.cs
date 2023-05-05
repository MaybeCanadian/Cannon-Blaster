using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Clip List", menuName = "Audio/Clip List")]
public class ClipList : ScriptableObject
{
    public List<AudioClip> clips;

    public AudioClip GetClip(bool random, int id = -1)
    {
        if(clips.Count <= 0)
        {
            Debug.LogError("ERROR - Clip list is empty");
            return null;
        }

        if(random)
        {
            return clips[Random.Range(0, clips.Count)];
        }

        if (id >= clips.Count)
        {
            Debug.LogError("ERROR - Index out of range.");
            return null;
        }

        return clips[id];
    }
}
