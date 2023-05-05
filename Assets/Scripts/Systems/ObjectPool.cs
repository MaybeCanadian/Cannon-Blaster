using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ObjectPoolManager
{
    public static int startAmount = 10;
    public static Dictionary<PooledObjects, Pool> poolDict = null;
    public static GameObject poolParent = null;

    #region Init Functions
    public static void OutSideInit()
    {
        CheckInit();
    }
    private static void CheckInit()
    {
        if (poolDict == null) 
        {
            Init();
        }
    }
    private static void Init()
    {
        SetUpParent();

        SetUpOBJDict();

        //Debug.Log("Object pool init");
    }
    private static void SetUpParent()
    {
        if (poolParent != null)
        {
            return;
        }

        poolParent = new GameObject();
        poolParent.name = "[Pools]";
    }
    private static void SetUpOBJDict()
    {
        poolDict = new Dictionary<PooledObjects, Pool>();

        foreach(int value in Enum.GetValues(typeof(PooledObjects)))
        {
            GameObject prefab = Resources.Load<GameObject>("Prefabs/" + (PooledObjects)value);

            if(prefab == null)
            {
                continue;
            }

            Pool pool = new Pool(prefab, startAmount, poolParent.transform);

            poolDict.Add((PooledObjects)value, pool);
        }
    }
    #endregion

    #region Object Control
    /// <summary>
    /// Returns an object from the pool of the given type. Object will be set inactive in scene.
    /// </summary>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static GameObject GetObjectFromPool(PooledObjects obj)
    {
        CheckInit();

        if(!poolDict.ContainsKey(obj))
        {
            Debug.LogError("ERROR - Could not get object from pool, Pool Dictionary does not contain a dictionary for that object.");
            return null;
        }

        return poolDict[obj].GetObjFromPool();
    }
    /// <summary>
    /// Returns the obj back to the pool, will fail if the pool cannot be found. Returns if failed or succeded.
    /// </summary>
    /// <param name="obj"></param>
    /// <param name="objType"></param>
    /// <returns></returns>
    public static bool ReturnObjectToPool(GameObject obj, PooledObjects objType)
    {
        CheckInit();

        if (!poolDict.ContainsKey(objType))
        {
            Debug.LogError("ERROR - Could not return object to pool, pool dictionary does not contain a pool for the object.");
            return false;
        }

        poolDict[objType].ReturnOBJToPool(obj);

        return true;
    }
    #endregion

    public static void ResetManager()
    {
        CheckInit();

        poolDict.Clear();

        GameObject.Destroy(poolParent);

        poolDict = null;
    }
}

[System.Serializable]
public class Pool 
{
    GameObject poolParent;
    GameObject objPrefab;
    Queue<GameObject> poolQueue;

    public Pool(GameObject prefab, int startingAmount, Transform parent)
    {
        objPrefab = prefab;

        if(prefab == null)
        {
            Debug.LogError("ERROR - Pool cannot be made with a null prefab");
            return;
        }
        poolParent = new GameObject();
        poolParent.transform.parent = parent.transform;
        poolParent.name = prefab.name;

        Populate(startingAmount);
    }
    private void Populate(int amount)
    {
        poolQueue = new Queue<GameObject>();

        for(int i = 0; i < amount; i++)
        {
            AddObjToPool();
        }
    } 
    private void AddObjToPool()
    {
        GameObject obj = GameObject.Instantiate(objPrefab, poolParent.transform);
        obj.SetActive(false);

        poolQueue.Enqueue(obj);
    }
    public void ReturnOBJToPool(GameObject obj)
    {
        obj.SetActive(false);
        poolQueue.Enqueue(obj);
    }
    public GameObject GetObjFromPool()
    {
        if(poolQueue.Count > 0)
        {
            return poolQueue.Dequeue(); 
        }

        AddObjToPool();

        if(poolQueue.Count <= 0)
        {
            Debug.LogError("ERROR - Could not get obj from pool.");
            return null;
        }

        return poolQueue.Dequeue();
    }
}

[System.Serializable]
public enum PooledObjects
{
    CannonBall,
    ShipTarget,
    SplashEffect
}
