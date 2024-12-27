using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour, IDamagable
{
    [SerializeField] protected float maxHealth;
    [SerializeField] protected float armor;
    [SerializeField] protected float maxSpeed;
    [SerializeField] protected float range;

    protected Team teamData;

    private float currentHealth;
    
    private float NormalizedHealth { get { return currentHealth / maxHealth; } }
    protected float SpeedMultiplier { get; private set; }
    protected float DamageMultiplier { get; private set; }

    public Action DeathEvent;

    private void Awake()
    {
        currentHealth = maxHealth;
        SpeedMultiplier = 1f;
        DamageMultiplier = 1f;

        teamData = GetComponent<Team>();
    }

    public bool Damage(float _damage)
    {
        currentHealth -= Mathf.Clamp(_damage - armor, 0, Mathf.Infinity);

        if (currentHealth <= 0)
        {
            HandleDeath();
            return true;
        }

        return false;
    }
    
    public void Heal(float _healAmt)
    {
        currentHealth = Mathf.Clamp(currentHealth + _healAmt, 0, maxHealth);
    }

    protected virtual void HandleDeath()
    {
        if (DeathEvent != null)
        {
            DeathEvent();
        }
        Destroy(this.gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(this.transform.position, range);
    }
}
