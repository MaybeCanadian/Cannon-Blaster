using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour
{
    [Min(1)]
    public int maxHealth = 3;
    public int currentHealth;

    public HealthBar healthBar;

    private void Start()
    {
        currentHealth = maxHealth;

        healthBar.SetPercent(currentHealth / maxHealth);
    }

    public void takeDamage(int amount)
    {
        currentHealth -= amount;

        if(currentHealth <= 0)
        {
            Die();
        }

        UpdateHealthBar();
    }

    private void Die()
    {
        //play an effect

        Debug.Log("Target Destroyed");

        Destroy(gameObject);
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
