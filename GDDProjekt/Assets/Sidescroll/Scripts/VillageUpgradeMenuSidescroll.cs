using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;


public class VillageUpgradeMenuSidescroll : MonoBehaviour
{
    public TextMeshProUGUI[] ressourcesText;
    public GameManager gameManager;
    public VillageUpgradeUiHolderSidescroll sniperNpc;
    public VillageUpgradeUiHolderSidescroll mortarNpc;
    public VillageUpgradeUiHolderSidescroll normalNpc;
    public VillageUpgradeUiHolderSidescroll turret;
    public VillageUpgradeUiHolderSidescroll wall;

    public LevelCostsSidescroll sniperlevelCostsSidescroll; 
    public Sprite[] imageList; 

    void Start()
    {   
        string test = File.ReadAllText(Application.dataPath + "/save1.json");
        LevelCostsSidescroll test2 = JsonUtility.FromJson<LevelCostsSidescroll>(test);
        Debug.Log(test2);
        //File.WriteAllText(Application.dataPath + "/save1.json", JsonUtility.ToJson(new LevelCostsSidescroll()));
    }

    // void Update()
    // {
    //     for (int i = 0; i < ressourcesText.Length; i++)
    //     {
    //         ressourcesText[i].text = gameManager.resources[i].ToString();
    //     }
    //     showSniperNpc();
    // }

    // void showSniperNpc()
    // {
    //     sniperNpc.levelTextMesh.text = "Level " + gameManager.sniperNpcLevel.ToString();
    //     int level = gameManager.sniperNpcLevel + 1;


    //     sniperNpc.ressource1ImageFormat.sprite = imageList[sniperlevelCostsSidescroll.ressource1Name[level]];
    //     sniperNpc.ressource1ImageFormat.sprite = imageList[sniperlevelCostsSidescroll.ressource2Name[level]];
    //     sniperNpc.ressource1ImageFormat.sprite = imageList[sniperlevelCostsSidescroll.ressource3Name[level]];

    //     sniperNpc.ressource1TextMesh.text = sniperlevelCostsSidescroll.ressource1Amount[level].ToString();
    //     sniperNpc.ressource1TextMesh.text = sniperlevelCostsSidescroll.ressource2Amount[level].ToString();
    //     sniperNpc.ressource1TextMesh.text = sniperlevelCostsSidescroll.ressource3Amount[level].ToString();
    // }

    // public void upgradeSniperNpc()
    // {


    // }
}
