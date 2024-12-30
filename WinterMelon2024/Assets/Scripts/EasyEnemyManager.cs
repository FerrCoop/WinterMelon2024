using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EasyEnemyManager : EnemyManager
{
    [SerializeField] private UnitData baseEnemy;

    protected override void Awake()
    {
        availableUnits = new List<UnitData>();
        availableUnits.Add(baseEnemy);

        incomePerSecond = 5f;

        base.Awake();

        tickAction += HandleTick;
    }

    public override void HandleTick()
    {
        base.HandleTick();        
        if (currency > baseEnemy.baseCost)
        {
            SpawnUnit(baseEnemy);
        }
    }
}
