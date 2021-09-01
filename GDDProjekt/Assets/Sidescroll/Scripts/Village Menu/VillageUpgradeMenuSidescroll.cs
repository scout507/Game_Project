using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;


public class VillageUpgradeMenuSidescroll : MonoBehaviour
{
    public TextMeshProUGUI[] ressourcesText;
    GameManager gameManager;
    public Sprite[] imageList;

    public VillageUpgradeUiHolderSidescroll sniperNpc;
    public LevelCostsSidescroll sniperNpclevelCostsSidescroll;

    public VillageUpgradeUiHolderSidescroll mortarNpc;
    public LevelCostsSidescroll mortarNpclevelCostsSidescroll;

    public VillageUpgradeUiHolderSidescroll normalNpc;
    public LevelCostsSidescroll normalNpclevelCostsSidescroll;

    public VillageUpgradeUiHolderSidescroll turret;
    public LevelCostsSidescroll turretlevelCostsSidescroll;

    public VillageUpgradeUiHolderSidescroll wall;
    public LevelCostsSidescroll walllevelCostsSidescroll;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        sniperNpclevelCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/sniperNpclevelCostsSidescroll.json"));
        mortarNpclevelCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/mortarNpclevelCostsSidescroll.json"));
        normalNpclevelCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/normalNpclevelCostsSidescroll.json"));
        walllevelCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/walllevelCostsSidescroll.json"));
        turretlevelCostsSidescroll = JsonUtility.FromJson<LevelCostsSidescroll>(File.ReadAllText(Application.dataPath + "/turretlevelCostsSidescroll.json"));
    }

    void Update()
    {
        for (int i = 0; i < ressourcesText.Length; i++)
        {
            ressourcesText[i].text = gameManager.resources[i].ToString();
        }

        sniperNpc.show(gameManager.sniperNpcLevel, sniperNpclevelCostsSidescroll, imageList);
        mortarNpc.show(gameManager.mortarNpcLevel, mortarNpclevelCostsSidescroll, imageList);
        normalNpc.show(gameManager.normalNpcLevel, normalNpclevelCostsSidescroll, imageList);
        wall.show(gameManager.wall, walllevelCostsSidescroll, imageList);
        turret.show(gameManager.turret, turretlevelCostsSidescroll, imageList);
    }

    //0: Sniper, 1: Mortar, 2: Normal, 3: Level, 4: Turret
    public void upgrade(int index)
    {
        int level = 0;
        LevelCostsSidescroll levelCostsSidescroll = new LevelCostsSidescroll();

        switch (index)
        {
            case 0:
                level = gameManager.sniperNpcLevel;
                levelCostsSidescroll = sniperNpclevelCostsSidescroll;
                break;
            case 1:
                level = gameManager.mortarNpcLevel;
                levelCostsSidescroll = mortarNpclevelCostsSidescroll;
                break;
            case 2:
                level = gameManager.normalNpcLevel;
                levelCostsSidescroll = normalNpclevelCostsSidescroll;
                break;
            case 3:
                level = gameManager.wall;
                levelCostsSidescroll = walllevelCostsSidescroll;
                break;
            case 4:
                level = gameManager.turret;
                levelCostsSidescroll = turretlevelCostsSidescroll;
                break;
        }

        level++;

        for (int i = 0; i < levelCostsSidescroll.level[level].componenten.Length; i++)
        {
            if (levelCostsSidescroll.level[level].componenten[i].amount > gameManager.resources[levelCostsSidescroll.level[level].componenten[i].id]) return;
        }

        for (int i = 0; i < levelCostsSidescroll.level[level].componenten.Length; i++)
        {
            gameManager.resources[levelCostsSidescroll.level[level].componenten[i].id] -= levelCostsSidescroll.level[level].componenten[i].amount;
        }

        switch (index)
        {
            case 0:
                gameManager.sniperNpcLevel++;
                break;
            case 1:
                gameManager.mortarNpcLevel++;
                break;
            case 2:
                gameManager.normalNpcLevel++;
                break;
            case 3:
                gameManager.wall++;
                break;
            case 4:
                gameManager.turret++;
                break;
        }
    }


}
