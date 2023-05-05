using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TargetSpawner 
{
    #region Event Dispatchers
    public delegate void TargetEvent();
    public static TargetEvent OnTargetSpawned;
    public static TargetEvent OnSpawnPosRequest;
    #endregion

    public static List<Transform> spawnPoints;

    private static List<Transform> available = new List<Transform>();

    private static int currentTargets = 0;

    private static void Init()
    {
        spawnPoints = new List<Transform>();
        available = new List<Transform>();

        ConnectEvents();
    }
    private static void CheckInit() 
    {
        if(spawnPoints == null)
        {
            Init();
        }
    }
    private static void ConnectEvents()
    {
        TargetBehaviour.OnTargetKill += OnTargetDestroyed;
    }
    private static void DisconnectEvents()
    {
        TargetBehaviour.OnTargetKill -= OnTargetDestroyed;
    }
    public static void AddSpawnPoint(Transform pos)
    {
        CheckInit();

        spawnPoints.Add(pos);

        //Debug.Log(spawnPoints.Count);
    }
    public static void AddSpawnPointsToAvailable()
    {
        CheckInit();

        //Debug.Log(spawnPoints.Count);

        foreach(Transform pos in spawnPoints)
        {
            available.Add(pos);
        }
    }
    public static void SpawnTargetAtRandom(int health, int hitValue, int killValue, bool moving = false, float dist = 0.0f, float moveTime = 0.0f)
    {
        CheckInit();

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
            Debug.LogError("ERROR - Ship from object pool is null.");
            return;
        }

        ship.SetActive(true);

        ship.transform.position = pos.position; 

        if(ship == null)
        {
            ObjectPoolManager.ReturnObjectToPool(ship, PooledObjects.ShipTarget);
            Debug.LogError("ERROR - Ship from pool is null.");
            return;
        }

        TargetBehaviour target = ship.GetComponent<TargetBehaviour>();

        if(target == null)
        {
            ObjectPoolManager.ReturnObjectToPool(ship, PooledObjects.ShipTarget);
            Debug.LogError("ERROR - Ship has no target script.");
            return;
        }

        target.SetMaxHealth(health);
        target.ResetHealth();

        OnTargetSpawned?.Invoke();

        available.RemoveAt(index);

        if(!moving)
        {
            return;
        }

        MovingShip movingShip = ship.GetComponent<MovingShip>();

        if(movingShip == null)
        {
            Debug.LogError("ERROR - Ship does not having movement behavious script.");
            return;
        }

        movingShip.SetShipUp(ship.transform.forward, dist, moveTime);
        movingShip.MoveShip(true);
    }
    private static void OnTargetDestroyed(float val)
    {
        currentTargets--;
    }
    public static void ResetManager()
    {
        spawnPoints = new List<Transform>();
        available = new List<Transform>();

        currentTargets= 0;
    }
}
