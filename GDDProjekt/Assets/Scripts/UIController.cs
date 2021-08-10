using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    public TextMeshProUGUI lvlTxt;
    public Slider hpSlider;
    public Slider overHeatSlider;
    public TextMeshProUGUI currentHealthTxt;
    public TextMeshProUGUI maxHealthTxt;
   


    public TextMeshProUGUI woodTxt;
    public TextMeshProUGUI stoneTxt;
    public TextMeshProUGUI ironTxt;
    public TextMeshProUGUI goldTxt;
    public TextMeshProUGUI essenceTxt;
    Manager manager;
    PlayerStats playerStats;
    PlayerController playerController;

    private void Start() {
        manager = GetComponent<Manager>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
    }

    private void Update()
    {
        lvlTxt.text = "Depth: " + manager.level;
        currentHealthTxt.text = Mathf.RoundToInt(playerStats.hp).ToString();
        maxHealthTxt.text = Mathf.RoundToInt(playerStats.maxhp).ToString();
        hpSlider.maxValue = playerStats.maxhp;
        hpSlider.value = playerStats.hp;
        
        //Collectibles
        woodTxt.text = playerStats.loot[0].ToString();
        stoneTxt.text = playerStats.loot[1].ToString();
        ironTxt.text = playerStats.loot[2].ToString();
        goldTxt.text = playerStats.loot[3].ToString();
        essenceTxt.text = playerStats.essence.ToString();
        
        overHeatSlider.maxValue = 10f;
        overHeatSlider.value = playerController.gunscript.overheat;

    }

}
