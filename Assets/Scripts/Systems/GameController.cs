using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            Init();
        }
    }
    private void Init()
    {
        ObjectPoolManager.OutSideInit();
    }
    private void Start()
    {
        AudioManager.OutSideInit();

        AudioManager.StopAllChannel(PLaybackChannelList.Music);

        AudioClip track = ClipDatatBase.GetTrack(TrackList.Skirmish);

        AudioManager.PlaySound2D(track, PLaybackChannelList.Music, 1.0f, true);

        CannonManager.SetAllState(true);

        CannonManager.SetCannonState(CannonPositions.FirstLeft, true);

        TargetSpawner.AddSpawnPointsToAvailable();

        for(int i = 0; i < 3; i++)
        {
            TargetSpawner.SpawnTargetAtRandom(3, 0, 5, true, 10, 5);
        }
    }
}
