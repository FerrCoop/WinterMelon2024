using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseManager : Building
{
    [SerializeField] Transform unitSpawn;

    private float tickTracker;
    protected float currencyCap = 500f;
    protected float incomePerSecond = 10f;
    protected float currency;

    protected List<UnitData> availableUnits;
    protected Dictionary<UnitData, float> cooldownDict;

    public float CostMultiplier { get; private set; }
    public float CooldownMultiplier { get; private set; }

    public Action tickAction;

    protected override void Awake()
    {
        base.Awake();

        CostMultiplier = 1f;
        CooldownMultiplier = 1f;

        cooldownDict = new Dictionary<UnitData, float>();
        for (int i = 0; i < availableUnits.Count; i++)
        {
            cooldownDict.Add(availableUnits[i], 0f);
        }
    }

    protected virtual void Update()
    {
        tickTracker += Time.deltaTime;

        if (tickTracker > 1f)
        {
            tickTracker -= 1f;
            if (tickAction != null)
            {
                tickAction();
            }
        }

        foreach (UnitData _unit in cooldownDict.Keys.ToList<UnitData>())
        {
            cooldownDict[_unit] += Time.deltaTime;
        }
    }

    public override void HandleTick()
    {
        base.HandleTick();
        currency = Mathf.MoveTowards(currency, currencyCap, incomePerSecond);
    }

    public void SetIncomePerSecond(float _incomePerSecond)
    {
        incomePerSecond = _incomePerSecond;
    }

    public void IncrementIncomePerSecond(float _change)
    {
        incomePerSecond += _change;
    }

    public void SetCurrencyCap(float _cap)
    {
        currencyCap = _cap;
    }

    public void IncrementCurrencyCap(float _increment)
    {
        currencyCap += _increment;
    }

    public virtual bool SpawnUnit(UnitData _unitData)
    {
        cooldownDict.TryGetValue(_unitData, out float _cd);
        if (_cd < _unitData.baseCooldown * CooldownMultiplier || currency < _unitData.baseCost * CostMultiplier)
        {
            return false;
        }
        cooldownDict[_unitData] = 0f;
        currency -= _unitData.baseCost * CostMultiplier;
        Vector3 _jitter = new Vector3(UnityEngine.Random.Range(-0.3f, 0.3f), UnityEngine.Random.Range(-0.5f, 0.5f), 0f);
        Instantiate(_unitData.unit, unitSpawn.position + _jitter, Quaternion.identity);
        return true;
    }

}
