using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : BaseManager
{
    public Action DeathAction;

    protected override void HandleDeath()
    {
        base.HandleDeath();
        LevelUIManager uiManager = FindObjectOfType<LevelUIManager>();
        GameManager.Instance.AddXP(uiManager.levelData.levelXP);
        StartCoroutine(HandleVictory(uiManager));
    }

    private IEnumerator HandleVictory(LevelUIManager uIManager)
    {
        uIManager.DisplayText("Victory");
        yield return new WaitForSeconds(1f);
        uIManager.LoadScene("Lobby");
    }
}
