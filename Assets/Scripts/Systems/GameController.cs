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
        CannonManager.SetAllState(true);

        CannonManager.SetCannonState(CannonPositions.FirstLeft, true);

        TargetSpawner.AddSpawnPointsToAvailable();

        for(int i = 0; i < 3; i++)
        {
            TargetSpawner.SpawnTargetAtRandom(3, 0, 5, true, 10, 5);
        }
    }
}
