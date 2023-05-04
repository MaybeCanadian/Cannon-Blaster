using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner instance;

    public List<Transform> spawnPoints;

    private List<Transform> available;

    private void Awake()
    {
        if(instance != this && instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
    }
    public void AddSpawnPoint(Transform pos)
    {
        SpawnPos spawn = new SpawnPos(pos);

        spawnPoints.Add(spawn);
    }

    public void AddSpawnPointsToAvailable()
    {
        foreach(Transform pos in spawnPoints)
        {
            available.Add(pos);
        }
    }
    public void SpawnTargetAtRandom()
    {
        if(available.Count <= 0) 
        {
            Debug.LogError("ERROR - No open spots to spawn");
            return;
        }

        int index = Random.Range(0, available.Count);

        Transform pos = available[index];

        GameObject ship = ObjectPoolManager.GetObjectFromPool(PooledObjects.ShipTarget);

        if(ship == null)
        {
            returnl
        }

        available.RemoveAt(index);
    }
    
}
