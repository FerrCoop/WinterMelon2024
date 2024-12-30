using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitOption : MonoBehaviour
{
    public UnitData unitData;
    [SerializeField] private Image unitImage;

    public void Select(bool _select)
    {
        if (_select)
        {
            GetComponent<Image>().color = Color.green;
        }
        else
        {
            GetComponent<Image>().color = Color.white;
        }
    }

    public void Set(UnitData _unitData)
    {
        unitData = _unitData;
        unitImage.sprite = unitData.unitArt;
    }
}
