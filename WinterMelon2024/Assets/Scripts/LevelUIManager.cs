using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelUIManager : UIManager
{
    [SerializeField] private TextMeshProUGUI currencyText;
    [Space]
    [SerializeField] private Button[] unitButtons;    

    FriendlyManager friendlyManager;

    private UnitData[] availableUnits;
    private Image[] buttonImages;
    private Image[] cooldownBars;

    protected override void Awake()
    {
        base.Awake();
        friendlyManager = FindObjectOfType<FriendlyManager>();

        availableUnits = GameManager.Instance.units;

        buttonImages = new Image[unitButtons.Length];
        cooldownBars = new Image[unitButtons.Length];
        for (int i = 0; i < unitButtons.Length; i++)
        {
            buttonImages[i] = unitButtons[i].GetComponent<Image>();
            cooldownBars[i] = unitButtons[i].GetComponentInChildren<Image>();
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
        for (int i = 0; i < availableUnits.Length; i++)
        {
            if (availableUnits[i] == null)
            {
                unitButtons[i].gameObject.SetActive(false);
                continue;
            }

            buttonImages[i].sprite = availableUnits[i].unitArt;
            //Cost + buyable
            unitButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = 
                Mathf.RoundToInt(availableUnits[i].baseCost * friendlyManager.CostMultiplier).ToString();
            buttonImages[i].color = new Color(buttonImages[i].color.r, buttonImages[i].color.g, buttonImages[i].color.b, 0.6f);

            //Cooldown
            cooldownBars[i].fillAmount = 1f;

            unitButtons[i].onClick.AddListener(delegate { SpawnUnit(availableUnits[i]); });
        }
    }

    public void UpdateButtons(float _currency, float[] _cooldowns)
    {
        for (int i = 0; i < availableUnits.Length; i++)
        {
            if (availableUnits[i] == null)
            {
                break;
            }

            //Set Cost
            unitButtons[i].GetComponentInChildren<TextMeshProUGUI>().text =
                Mathf.RoundToInt(availableUnits[i].baseCost * friendlyManager.CostMultiplier).ToString();

            //Set Cooldown
            float _cd = _cooldowns[i] / (availableUnits[i].baseCooldown * friendlyManager.CooldownMultiplier);
            if (_cd >= 1f)
            {
                cooldownBars[i].fillAmount = 0f;
            }
            else
            {
                cooldownBars[i].fillAmount = _cd;
            }           

            float _transparency = 0.6f;
            if (_cooldowns[i] < availableUnits[i].baseCooldown * friendlyManager.CooldownMultiplier
                && _currency > availableUnits[i].baseCost * friendlyManager.CostMultiplier)
            {
                _transparency = 1f;
            }
            buttonImages[i].color = new Color(buttonImages[i].color.r, buttonImages[i].color.g, buttonImages[i].color.b, _transparency);
        }
    }
}
