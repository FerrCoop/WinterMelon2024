using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : UIManager
{
    public LevelData levelData;
    [Space]
    [SerializeField] private TextMeshProUGUI displayText;
    [SerializeField] private TextMeshProUGUI currencyText;
    [Space]
    [SerializeField] private UnitSpawner[] unitButtons;    

    FriendlyManager friendlyManager;

    private List<UnitData> availableUnits;
    private Image[] buttonImages;

    protected override void Awake()
    {
        base.Awake();
        friendlyManager = FindObjectOfType<FriendlyManager>();

        availableUnits = GameManager.Instance.units;

        buttonImages = new Image[unitButtons.Length];
        for (int i = 0; i < unitButtons.Length; i++)
        {
            buttonImages[i] = unitButtons[i].GetComponent<Image>();
        }

        SetButtons();
    }

    public void SetCurrencyText(float _current, float _max)
    {
        currencyText.text = Mathf.FloorToInt(_current).ToString() + " / " + Mathf.FloorToInt(_max).ToString();
    }

    public void SpawnUnit(UnitData _unitData)
    {
        friendlyManager.SpawnUnit(_unitData);
    }

    public void TogglePause()
    {

    }

    private void SetButtons()
    {
        for (int i = 0; i < GameManager.MAX_UNITS; i++)
        {
            if (i >= availableUnits.Count)
            {
                unitButtons[i].gameObject.SetActive(false);
                continue;
            }

            unitButtons[i].SetUnit(availableUnits[i]);
            //Cost + buyable
            unitButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = 
                Mathf.RoundToInt(availableUnits[i].baseCost * friendlyManager.CostMultiplier).ToString();
            buttonImages[i].color = new Color(buttonImages[i].color.r, buttonImages[i].color.g, buttonImages[i].color.b, 0.6f);

            //Cooldown
            unitButtons[i].SetCD(1f);
        }
    }

    public void DisplayText(string _text)
    {
        displayText.text = _text;
        if(_text == "Victory")
        {
            displayText.color = Color.blue;
        }
        else
        {
            displayText.color = Color.red;
        }
        displayText.gameObject.SetActive(true);
    }

    public void UpdateButtons(float _currency, float[] _cooldowns)
    {
        for (int i = 0; i < availableUnits.Count; i++)
        {
            //Set Cost
            unitButtons[i].GetComponentInChildren<TextMeshProUGUI>().text =
                Mathf.RoundToInt(availableUnits[i].baseCost * friendlyManager.CostMultiplier).ToString();

            //Set Cooldown
            float _cd = _cooldowns[i] / (availableUnits[i].baseCooldown * friendlyManager.CooldownMultiplier);
            if (_cd >= 1f)
            {
                unitButtons[i].SetCD(0f);
            }
            else
            {
                unitButtons[i].SetCD(_cd);
            }           

            float _transparency = 0.6f;
            if (_cooldowns[i] > availableUnits[i].baseCooldown * friendlyManager.CooldownMultiplier
                && _currency > availableUnits[i].baseCost * friendlyManager.CostMultiplier)
            {
                _transparency = 1f;
            }
            buttonImages[i].color = new Color(buttonImages[i].color.r, buttonImages[i].color.g, buttonImages[i].color.b, _transparency);
        }
    }
}
