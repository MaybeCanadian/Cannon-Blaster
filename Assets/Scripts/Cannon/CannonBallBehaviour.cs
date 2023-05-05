using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class CannonBallBehaviour : MonoBehaviour
{
    Rigidbody rb;
    public bool active = false;
    public PooledObjects objType = PooledObjects.CannonBall;

    public ClipListNames waterSounds = ClipListNames.WaterSplash;
    public ClipListNames boatHitSounds = ClipListNames.BoatHit;

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
    private void HitWater()
    {
        ClipList list = ClipDatatBase.GetList(waterSounds);

        if (list != null)
        {
            AudioClip clip = list.GetClip(true);

            if (clip != null)
            {
                AudioManager.PlaySound3D(clip, PLaybackChannelList.Effect, transform.position);
            }
        }

        GameObject splashEffect = ObjectPoolManager.GetObjectFromPool(PooledObjects.SplashEffect);

        if (splashEffect == null)
        {
            Debug.LogError("ERROR - Splash Effect from pool was null.");
            return;
        }

        splashEffect.SetActive(true);
        splashEffect.transform.position = transform.position;

        SplashEffect effect = splashEffect.GetComponent<SplashEffect>();

        if(effect == null)
        {
            ObjectPoolManager.ReturnObjectToPool(splashEffect, PooledObjects.SplashEffect);
            Debug.LogError("ERROR - Splash effect has no behabiour script.");
            return;
        }

        effect.PlayEffect();
    }
    private void HitDeathPlane()
    {
        RemoveBall();
    }
    private void HitBoat(TargetBehaviour target)
    {
        ClipList list = ClipDatatBase.GetList(boatHitSounds);

        if (list != null)
        {
            AudioClip clip = list.GetClip(true);

            if (clip != null)
            {
                AudioManager.PlaySound3D(clip, PLaybackChannelList.Effect, transform.position, 2.0f);
            }
        }

        target.takeDamage(1);

        RemoveBall();
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
            HitWater();

            return;
        }

        WaterDeathPlane deathPlane = other.GetComponent<WaterDeathPlane>();

        if(deathPlane != null)
        {
            HitDeathPlane();
            
            return;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        TargetBehaviour target = collision.gameObject.GetComponent<TargetBehaviour>();

        if (target != null)
        {
            HitBoat(target);

            return;
        }
    }
}
