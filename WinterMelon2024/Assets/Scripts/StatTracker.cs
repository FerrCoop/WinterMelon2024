using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatTracker : MonoBehaviour
{
    private static int unitsSpawned;
    private static int unitsKilled;
    private static int unitsLost;

    public static void LoadData()
    {
        unitsSpawned = PlayerPrefs.GetInt("unitsSpawned");
        unitsKilled = PlayerPrefs.GetInt("unitsKilled");
        unitsLost = PlayerPrefs.GetInt("unitsLost");
    }

    public static void IncrementUnitsSpawned()
    {
        unitsSpawned++;
    }

    public static void IncrementUnitsKilled()
    {
        unitsKilled++;
    }

    public static void IncrementUnitsLost()
    {
        unitsLost++;
    }

    public static void SaveStats()
    {
        PlayerPrefs.SetInt("unitsSpawned", unitsSpawned);
        PlayerPrefs.SetInt("unitsKilled", unitsKilled);
        PlayerPrefs.SetInt("unitsLost", unitsLost);
        PlayerPrefs.Save();
    }
        
}
