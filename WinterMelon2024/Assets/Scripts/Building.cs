using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Building : MonoBehaviour, IDamagable
{
    [SerializeField] private float regenPerSecond;
    [Space]
    [SerializeField] private Image healthBar;

    private float maxHealth = 1000f;
    private float currentHealth;

    private float HealthNormalized { get { return currentHealth / maxHealth; } }

    public virtual void HandleTick()
    {
        currentHealth = Mathf.MoveTowards(currentHealth, maxHealth, regenPerSecond);
    }

    public bool Damage(float _damage)
    {
        currentHealth -= _damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            HandleDeath();
            return true;
        }
        return false;
    }

    public void SetMaxHealth(float _maxHealth)
    {
        maxHealth = _maxHealth;
    }

    protected void UpdateHealthBar()
    {
        healthBar.fillAmount = HealthNormalized;
    }

    protected virtual void HandleDeath()
    {
        Debug.Log("Building Destroyed");
    }
}
