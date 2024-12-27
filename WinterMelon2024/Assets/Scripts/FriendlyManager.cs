using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FriendlyManager : BaseManager
{
    private LevelUIManager uiManager;

    protected override void Awake()
    {
        base.Awake();

        uiManager = FindObjectOfType<LevelUIManager>();
       
        tickAction += HandleTick;
    }

    protected override void Update()
    {
        base.Update();

        float[] _cooldowns = new float[availableUnits.Length];
        for (int i = 0; i < availableUnits.Length; i++)
        {
            if (availableUnits[i] == null)
            {
                break;
            }
            _cooldowns[i] = cooldownDict[availableUnits[i]];
        }

        uiManager.UpdateButtons(currency, _cooldowns);
    }

    public override void HandleTick()
    {
        base.HandleTick();
        UpdateUI();
    }

    protected void UpdateUI()
    {
        uiManager.SetCurrencyText(currency, currencyCap);
    }

    public override bool SpawnUnit(UnitData _unitData)
    {
        bool _spawned = base.SpawnUnit(_unitData);
        if (_spawned)
        {
            UpdateUI();
            StatTracker.IncrementUnitsSpawned();
        }
        return _spawned;
    }
}
