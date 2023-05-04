using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetSpawner : MonoBehaviour
{
    public void SpawnTarget(Vector3 Pos, int health, float scoreValue)
    {
        GameObject target = ObjectPoolManager.GetObjectFromPool(PooledObjects.ShipTarget);

        if(target == null)
        {
            Debug.LogError("ERROR - Ship target from pool was null.");
            return;
        }

        target.transform.position = Pos;

        target.SetActive(true);

        TargetBehaviour behaviour = target.GetComponent<TargetBehaviour>(); 
        
        if(behaviour == null)
        {
            Debug.LogError("ERROR - Target does not have a behaviour script.");
            ObjectPoolManager.ReturnObjectToPool(target, PooledObjects.ShipTarget);
            return;
        }

        behaviour.SetMaxHealth(health);

        behaviour.ResetHealth();
    }
}
