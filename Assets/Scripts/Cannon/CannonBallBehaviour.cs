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

        WaterSplashPlane splashPlane = other.GetComponent<WaterSplashPlane>();

        if (splashPlane != null)
        {
            Debug.Log("Sploosh");

            //play sound effect and other stuff here

            return;
        }

        WaterDeathPlane deathPlane = other.GetComponent<WaterDeathPlane>();

        if(deathPlane != null)
        {
            Debug.Log("Removed");

            RemoveBall();
            
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        TargetBehaviour target = collision.gameObject.GetComponent<TargetBehaviour>();

        if (target != null)
        {
            Debug.Log("Hit!");

            RemoveBall();

            return;
        }
    }
}
