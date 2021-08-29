using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class VillageUpgradeUiHolderSidescroll : MonoBehaviour
{
    public GameObject level;
    public GameObject ressource1Image;
    public GameObject ressource1Text;
    public GameObject ressource2Image;
    public GameObject ressource2Text;
    public GameObject ressource3Image;
    public GameObject ressource3Text;

    [HideInInspector] public TextMeshProUGUI levelTextMesh;
    [HideInInspector] public Image ressource1ImageFormat;
    [HideInInspector] public TextMeshProUGUI ressource1TextMesh;
    [HideInInspector] public Image ressource2ImageFormat;
    [HideInInspector] public TextMeshProUGUI ressource2TextMesh;
    [HideInInspector] public Image ressource3ImageFormat;
    [HideInInspector] public TextMeshProUGUI ressource3TextMesh;

    void Start()
    {
        levelTextMesh = level.GetComponent<TextMeshProUGUI>();

        ressource1ImageFormat = ressource1Image.GetComponent<Image>();
        ressource1TextMesh = ressource1Text.GetComponent<TextMeshProUGUI>();

        ressource2ImageFormat = ressource2Image.GetComponent<Image>();
        ressource2TextMesh = ressource2Text.GetComponent<TextMeshProUGUI>();

        ressource3ImageFormat = ressource3Image.GetComponent<Image>();
        ressource3TextMesh = ressource3Text.GetComponent<TextMeshProUGUI>();
    }

    public void show(int level, LevelCostsSidescroll levelCostsSidescroll, Sprite[] imageList)
    {
        //levelTextMesh.text = "Level " + level.ToString();
        level++;
        int amount = levelCostsSidescroll.level[level].componenten.Length;

        if (amount == 3)
        {
            ressource1ImageFormat.sprite = imageList[levelCostsSidescroll.level[level].componenten[0].id];
            ressource2ImageFormat.sprite = imageList[levelCostsSidescroll.level[level].componenten[1].id];
            ressource3ImageFormat.sprite = imageList[levelCostsSidescroll.level[level].componenten[2].id];

            ressource1TextMesh.text = levelCostsSidescroll.level[level].componenten[0].amount.ToString();
            ressource2TextMesh.text = levelCostsSidescroll.level[level].componenten[1].amount.ToString();
            ressource3TextMesh.text = levelCostsSidescroll.level[level].componenten[2].amount.ToString();
        }
        else if (amount == 2)
        {
            ressource1ImageFormat.sprite = imageList[levelCostsSidescroll.level[level].componenten[0].id];
            ressource2ImageFormat.sprite = null;
            ressource3ImageFormat.sprite = imageList[levelCostsSidescroll.level[level].componenten[1].id];

            ressource1TextMesh.text = levelCostsSidescroll.level[level].componenten[0].amount.ToString();
            ressource2TextMesh.text = null;
            ressource3TextMesh.text = levelCostsSidescroll.level[level].componenten[1].amount.ToString();
        }
        else
        {
            ressource1ImageFormat.sprite = null;
            ressource2ImageFormat.sprite = imageList[levelCostsSidescroll.level[level].componenten[0].id];
            ressource3ImageFormat.sprite = null;

            ressource1TextMesh.text = null;
            ressource2TextMesh.text = levelCostsSidescroll.level[level].componenten[0].amount.ToString();
            ressource3TextMesh.text = null;
        }
    }


}
