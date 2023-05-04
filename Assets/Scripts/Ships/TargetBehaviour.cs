using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    #region Event Dispatchers
    public delegate void TargetScoreEvent(float amount);
    public static TargetScoreEvent OnTargetHit;
    public static TargetScoreEvent OnTargetKill;
    #endregion

    [Min(1)]
    public int maxHealth = 3;
    public int currentHealth;

    public HealthBar healthBar;

    public int hitValue = 1;
    public int killValue = 5;

    private void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetPercent(currentHealth / maxHealth);
    }
    public void SetMaxHealth(int value)
    {
        maxHealth = value;

        currentHealth = Mathf.Max(currentHealth, maxHealth);

        UpdateHealthBar();
    }
    public void ResetHealth()
    {
        currentHealth = maxHealth;

        UpdateHealthBar();
    }
    public void takeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Die();
            return;
        }

        OnTargetHit?.Invoke(hitValue);

        UpdateHealthBar();
    }
    private void Die()
    {
        OnTargetKill?.Invoke(killValue);

        ObjectPoolManager.ReturnObjectToPool(gameObject, PooledObjects.ShipTarget);
    }
    private void UpdateHealthBar()
    {
        if(healthBar == null) 
        {
            Debug.LogError("ERROR - No health bar found.");
            return;
        }

        float percent = currentHealth * 1.0f / maxHealth * 1.0f;

        healthBar.SetPercent(percent);
    }
}
