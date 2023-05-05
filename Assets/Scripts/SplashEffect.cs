using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashEffect : MonoBehaviour
{
    public ParticleSystem effect;
    bool playing = false;
    float timer = 0.0f;
    float duration = 0.0f;
    private void Awake()
    {
        effect = GetComponent<ParticleSystem>();
    }
    public void PlayEffect()
    {
        Debug.Log("playing effect");

        effect.Play();
        playing = true;
        duration = effect.main.startLifetime.constant + effect.main.duration;
        timer = 0.0f;
    }
    private void Update()
    {
        if(playing)
        {
            timer += Time.deltaTime;

            if(timer >= duration)
            {
                ReturnToPool();
            }
        }
    }
    private void ReturnToPool()
    {
        playing = false;

        ObjectPoolManager.ReturnObjectToPool(gameObject, PooledObjects.SplashEffect);
    }
}
