using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public UnitData[] units;

    private const int MAX_UNITS = 4;

    private int numUnits; 

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);

        StatTracker.LoadData();

        units = new UnitData[MAX_UNITS];
    }

    public void AddUnit(UnitData _unitData)
    {
        if (numUnits < MAX_UNITS)
        {
            units[numUnits] = _unitData;
            numUnits++;
        }
    }

    public void OnApplicationQuit()
    {
        StatTracker.SaveStats();
    }
}
