using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class PlaybackManager : MonoBehaviour
{
    public Dictionary<PLaybackChannelList, PlaybackChannel> channelDict = null;
    public AudioMixer mixer;

    #region Init Functions
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
    #endregion

    #region Playback
    public void PlaySound2D(AudioClip clip, PLaybackChannelList channel, float volume, bool loop = false)
    {
        if(!channelDict.ContainsKey(channel))
        {
            Debug.LogError("ERROR - Channel Dict does not have a chnanel for given name.");
            return;
        }

        AudioSource source = channelDict[channel].GetAudioSource();

        source.clip = clip;
        source.loop = loop;

        source.spatialBlend = 0.0f;

        source.volume = volume;

        source.Play();
    }
    public void PlaySound3D(AudioClip clip, PLaybackChannelList channel, float volume, Vector3 pos, bool loop = false)
    {
        if (!channelDict.ContainsKey(channel))
        {
            Debug.LogError("ERROR - Channel Dict does not have a chnanel for given name.");
            return;
        }

        AudioSource source = channelDict[channel].GetAudioSource();

        source.clip = clip;
        source.loop = loop;

        source.spatialBlend = 1.0f;

        source.volume = volume;

        source.transform.position = pos;

        source.Play();
    }
    #endregion
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
    private List<AudioSource> activeSources = new List<AudioSource>();

    private GameObject channelParent = null;
    private AudioMixerGroup mixerGroup;

    #region Init Functions
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
    #endregion

    #region Source Control
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
    public AudioSource GetAudioSource()
    {
        if(useList.Count <= 0)
        {
            CheckSourcesForDone();

            if (useList.Count <= 0)
            {
                CreateNewSource();
            }
        }

        if(useList.Count <= 0)
        {
            Debug.Log("ERROR - Could not generate any sources.");
            return null;
        }

        AudioSource source = useList.Dequeue();

        activeSources.Add(source);

        return source;
    }
    #endregion

    private void CheckSourcesForDone()
    {
        List<AudioSource> doneSources = new List<AudioSource>();

        foreach(AudioSource source in activeSources)
        {
            if (source.isPlaying == false)
            {
                doneSources.Add(source);
            }
        }

        foreach(AudioSource source in doneSources)
        {
            source.Stop();
            source.enabled = false;

            activeSources.Remove(source);
            useList.Enqueue(source);
        }
    }
}
