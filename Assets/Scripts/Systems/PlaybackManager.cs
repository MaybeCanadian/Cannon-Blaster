using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class PlaybackManager : MonoBehaviour
{
    public Dictionary<PLaybackChannelList, PlaybackChannel> channelDict = null;
    public AudioMixer mixer;
    int startingAmount;

    public void SetUpPlaybackManager(int startAmount)
    {
        LoadMixer();

        SetUpChannels(startAmount);
    }
    private void LoadMixer()
    {
        mixer = Resources.Load<AudioMixer>("Audio/Mixer");

        if(mixer == null)
        {
            Debug.LogError("ERROR - Could not load the mixer from resources.");
            return;
        }
    }
    private void SetUpChannels(int startAmount)
    {
        channelDict = new Dictionary<PLaybackChannelList, PlaybackChannel>();

        foreach(int name in Enum.GetValues(typeof(PLaybackChannelList)))
        {
            AudioMixerGroup group = GetMixerGroup((PLaybackChannelList)name);

            PlaybackChannel channel = new PlaybackChannel(group, transform, startAmount);

            channelDict.Add((PLaybackChannelList)name, channel);
        }
    }
    private AudioMixerGroup GetMixerGroup(PLaybackChannelList channel)
    {
        AudioMixerGroup[] groups = mixer.FindMatchingGroups(channel.ToString());

        if (groups.Length < 1)
        {
            Debug.Log("ERROR - Could not find matching audio channel");
            return null;
        }

        return groups[0];
    }
}

[System.Serializable]
public enum PLaybackChannelList
{
    Master,
    Effect,
    Music
}

[System.Serializable]
public class PlaybackChannel
{
    private List<AudioSource> sourceList = new List<AudioSource>();
    private Queue<AudioSource> useList = new Queue<AudioSource>();

    private GameObject channelParent = null;
    private AudioMixerGroup mixerGroup;

    public PlaybackChannel(AudioMixerGroup group, Transform parent, int startingAmount)
    {
        mixerGroup = group;

        SetUpParent(parent);

        SetUpSources(startingAmount);
    }

    private void SetUpParent(Transform parent)
    {
        channelParent = new GameObject();
        channelParent.transform.parent = parent;
        channelParent.name = mixerGroup.name;
    }
    private void SetUpSources(int startAmount)
    {
        for(int i = 0; i < startAmount; i++)
        {
            CreateNewSource();
        }
    }
    private void CreateNewSource()
    {
        GameObject newSource = new GameObject();
        newSource.name = mixerGroup.name;
        newSource.transform.parent = channelParent.transform;

        AudioSource source = newSource.AddComponent<AudioSource>();
        source.clip = null;
        source.playOnAwake = false;
        source.enabled = false;
        source.outputAudioMixerGroup = mixerGroup;

        sourceList.Add(source);
        useList.Enqueue(source);
    }
}
