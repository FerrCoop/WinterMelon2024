using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManager : UIManager
{
    [SerializeField] private UnitOption[] unitOptions;
    [Space]
    [SerializeField] private GameObject buttons;
    [SerializeField] private GameObject researchPanel;
    [SerializeField] private GameObject teamPanel;
    [SerializeField] private TextMeshProUGUI xpText;
    [SerializeField] private GameObject levels;
    [Space]
    [SerializeField] private Button unlockButton;
    [SerializeField] private TextMeshProUGUI unlockText;

    private ResearchOption selectedOption;

    protected override void Awake()
    {
        base.Awake();
        SetXP();
    }

    public void SetXP()
    {
        xpText.text = GameManager.Instance.XP.ToString() + " XP";
    }

    public void ToggleResearch()
    {
        levels.SetActive(!levels.activeSelf);
        researchPanel.SetActive(!researchPanel.activeSelf);
        buttons.SetActive(!buttons.activeSelf);
        if (buttons.activeSelf)
        {
            unlockButton.gameObject.SetActive(false);
        }
    }

    public void ToggleTeamSelect()
    {
        teamPanel.SetActive(!teamPanel.activeSelf);
        buttons.SetActive(!buttons.activeSelf);
        levels.SetActive(!levels.activeSelf);

        if (teamPanel.activeSelf)
        {
            for (int i = 0; i < unitOptions.Length; i++)
            {
                if(i < GameManager.Instance.unlockedUnits)
                {
                    unitOptions[i].gameObject.SetActive(true);
                    unitOptions[i].Set(GameManager.Instance.unitUnlocks[i]);
                }
                else
                {
                    unitOptions[i].gameObject.SetActive(false);
                }
            }
        }
    }

    public void SelectResearch(ResearchOption _researchOption)
    {
        selectedOption = _researchOption;
        if (selectedOption == null)
        {
            unlockButton.gameObject.SetActive(false);
            return;
        }
        unlockButton.gameObject.SetActive(true);
        unlockText.text = "Unlock " + _researchOption.title;       
    }

    public void UnlockResearch()
    {
        if (GameManager.Instance.XP < selectedOption.cost)
        {
            return;
        }
        GameManager.Instance.AddXP(-selectedOption.cost);
        selectedOption.Unlock();
        SetXP();
    }

    public void ToggleUnit(UnitOption _option)
    {
        if (GameManager.Instance.units.Contains(_option.unitData))
        {
            GameManager.Instance.units.Remove(_option.unitData);
            _option.Select(false);
        }
        else
        {
            if (GameManager.Instance.AddUnit(_option.unitData))
            {
                _option.Select(true);
            }
            else
            {
                StartCoroutine(ShowTeamsizeError());
            }
        }
    }

    private IEnumerator ShowTeamsizeError()
    {
        yield return 0;
    }
}
