using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResearchOption : MonoBehaviour
{
    public string title;
    public int cost;
    [Space]
    [SerializeField] private Image[] researchUnlocks;
    [SerializeField] private UnitData[] unitUnlocks;
    [SerializeField] private Building[] buildingUnlocks;
    [Space]
    [SerializeField] private TextMeshProUGUI titleText;
    [SerializeField] private TextMeshProUGUI costText;

    private void Awake()
    {
        titleText.text = title;
        costText.text = cost.ToString() + " XP";
    }

    public void Unlock()
    {
        foreach (Image _image in researchUnlocks)
        {
            _image.color = new Color(_image.color.r, _image.color.g, _image.color.b, 1f);
        }
        GameManager.Instance.UnlockUnits(unitUnlocks.Length);
        GameManager.Instance.UnlockBuildings(buildingUnlocks.Length);
    }
}
