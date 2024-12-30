using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitSpawner : MonoBehaviour
{
    [SerializeField] private Image unitImage;
    [SerializeField] private Image cdBar;

    private UnitData unitData;
    private FriendlyManager friendlyManager;
    
    public void SetUnit(UnitData _unitData)
    {
        unitData = _unitData;
        unitImage.sprite = unitData.unitArt;
    }

    public void SpawnUnit()
    {
        if (friendlyManager == null)
        {
            friendlyManager = FindObjectOfType<FriendlyManager>();
        }
        friendlyManager.SpawnUnit(unitData);
    }

    public void SetCD(float _cd)
    {
        cdBar.fillAmount = _cd;
    }
}
