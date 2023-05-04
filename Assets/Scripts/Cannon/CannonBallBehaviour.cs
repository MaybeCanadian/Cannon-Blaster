using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonBallBehaviour : MonoBehaviour
{
    Rigidbody rb;
    public bool active = false;
    public PooledObjects objType = PooledObjects.CannonBall;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void FireBall(Vector3 launch, Vector3 start)
    {
        active = true;

        transform.position = start;

        rb.AddForce(launch, ForceMode.Impulse);

    }
    private void RemoveBall()
    {
        rb.velocity = Vector3.zero;

        active = false;

        ObjectPoolManager.ReturnObjectToPool(gameObject, objType);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(!active)
        {
            return;
        }

        WaterDeathPlane water = other.GetComponent<WaterDeathPlane>();

        if(water != null)
        {
            Debug.Log("Sploosh");

            RemoveBall();
            
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        
    }
}
