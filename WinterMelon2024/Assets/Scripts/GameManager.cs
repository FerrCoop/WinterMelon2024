using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int XP { get { return xp; } }

    public UnitData[] unitUnlocks;

    public List<UnitData> units;

    public const int MAX_UNITS = 4;

    private int numUnits;
    private int currentLevel;
    private int unlockedUnits = 1;
    private int unlockedBuildings = 0;
    private int xp;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(this);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(this.gameObject);
        currentLevel = PlayerPrefs.GetInt("currentLevel");
        unlockedUnits = PlayerPrefs.GetInt("unlockedUnits");

        StatTracker.LoadData();

        units = new List<UnitData>();
    }

    public bool AddUnit(UnitData _unitData)
    {
        if(units.Count < MAX_UNITS)
        {
            units.Add(_unitData);
            return true;
        }
        return false;
    }

    public void AddXP(int _xp)
    {
        xp += _xp;
    }

    public void UnlockUnits(int _num)
    {
        unlockedUnits += _num;
    }

    public void UnlockBuildings(int _num)
    {
        unlockedBuildings += _num;
    }

    public void OnApplicationQuit()
    {
        PlayerPrefs.SetInt("currentLevel", currentLevel);
        PlayerPrefs.SetInt("unlockedUnits", unlockedUnits);
        StatTracker.SaveStats();
    }
}
