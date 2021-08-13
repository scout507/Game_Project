using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
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

    public GameObject exitScreen;
    public TextMeshProUGUI woodCountTxt;
    public TextMeshProUGUI stoneCountTxt;
    public TextMeshProUGUI ironCountTxt;
    public TextMeshProUGUI goldCountTxt;
    public TextMeshProUGUI bluessenceTxt;
    public TextMeshProUGUI greenssenceTxt;
    public TextMeshProUGUI redssenceTxt;
    public TextMeshProUGUI goldssenceTxt;

    public Image weapon1;
    public Image weapon2;
    public Image spinner;

    Manager manager;
    PlayerStats playerStats;
    PlayerController playerController;

    private void Start() {
        manager = GetComponent<Manager>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        weapon1.sprite = playerController.guns[0].GetComponent<Weapon>().iconActive;
        weapon2.sprite = playerController.guns[1].GetComponent<Weapon>().icon;
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
        if(playerController.weaponOneActive){
            weapon1.color = new Color(1f,1f,1f,1f);
            weapon2.color = new Color(1f,1f,1f,0.5f);
            weapon1.sprite = playerController.guns[0].GetComponent<Weapon>().iconActive;
            weapon2.sprite = playerController.guns[1].GetComponent<Weapon>().icon;
        }
        else{
            weapon1.color = new Color(1f,1f,1f,0.5f);
            weapon2.color = new Color(1f,1f,1f,1f);
            weapon1.sprite = playerController.guns[0].GetComponent<Weapon>().icon;
            weapon2.sprite = playerController.guns[1].GetComponent<Weapon>().iconActive;
        }


    }

    public void exitMenu(){
        exitScreen.SetActive(true);
        woodCountTxt.text = playerStats.loot[0].ToString();
        stoneCountTxt.text = playerStats.loot[1].ToString();
        ironCountTxt.text = playerStats.loot[2].ToString();
        goldCountTxt.text = playerStats.loot[3].ToString();
        bluessenceTxt.text = playerStats.loot[4].ToString();
        greenssenceTxt.text = playerStats.loot[5].ToString();
        redssenceTxt.text = playerStats.loot[6].ToString();
        goldssenceTxt.text = playerStats.loot[7].ToString();
    }

    public void continueButton(){
        manager.resumeGame();
        exitScreen.SetActive(false);
    }

    public void exitButton(){
        exitScreen.SetActive(false);
        SceneManager.LoadScene("Sidescroll");
    }


    public void spinanim(){
        spinner.GetComponent<Animator>().Play("spin");
    }

}
