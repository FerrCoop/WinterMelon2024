using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Team : MonoBehaviour
{
    [SerializeField] private LayerMask enemyLayer;

    public abstract int Direction { get; }
    public LayerMask EnemyLayer { get { return enemyLayer; } }

    private void Awake()
    {
        GetComponent<Unit>().DeathEvent += OnDeath;
    }

    public abstract void OnDeath();
}
