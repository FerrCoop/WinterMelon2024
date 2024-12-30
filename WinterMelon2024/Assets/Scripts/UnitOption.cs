using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UnitOption : MonoBehaviour
{
    public UnitData unitData;
    [SerializeField] private Image unitImage;

    private void Awake()
    {
        unitImage.sprite = unitData.unitArt;
    }

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
}
