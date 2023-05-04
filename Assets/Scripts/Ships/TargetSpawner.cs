using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public static TargetSpawner instance;

    public List<SpawnPos> spawnPoints;

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
    
}

[System.Serializable]
public class SpawnPos
{
    public Transform pos;
    public bool ocupied = false;

    public SpawnPos(Transform pos)
    {
        this.pos = pos;
        ocupied = false;
    }
}
