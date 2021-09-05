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
    public Slider esSlider;
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

    public GameObject stunHolder;
    public TextMeshProUGUI stunTime;
    public GameObject slowHolder;
    public TextMeshProUGUI slowTime;
    public GameObject dashHolder;
    public TextMeshProUGUI dashTime;
    public GameObject poisonHolder;
    public TextMeshProUGUI poisonStacks;

    public GameObject deathScreen;
    public TextMeshProUGUI deathScreenText;

    public GameObject dialogueHolder;
    public TextMeshProUGUI diaNametxt;
    public TextMeshProUGUI diatxt;
    public string dialogueName;
    public string dialogue;

    public GameObject tutorial;

    Manager manager;
    PlayerStats playerStats;
    PlayerController playerController;
    GameManager gameManager;

    private void Start() {
        manager = GetComponent<Manager>();
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
        playerController = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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
        esSlider.maxValue = playerStats.maxEnergyShield;
        esSlider.value = playerStats.energyShield;
        
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

        diaNametxt.text = dialogueName;
        diatxt.text = dialogue;

        
        
        

        if(playerController.moveBlockTimer > 0.2){
            stunTime.text =  playerController.moveBlockTimer.ToString("F1");
            stunHolder.SetActive(true);
        } 
        else stunHolder.SetActive(false);
        
        if(playerController.slowTimer > 0){
            slowTime.text =  playerController.slowTimer.ToString("F1");
            slowHolder.SetActive(true);
        } 
        else slowHolder.SetActive(false);
        
        if(playerController.dashTimer > 0){
            dashTime.text =  playerController.dashTimer.ToString("F1");
            dashHolder.SetActive(true);
        } 
        else dashHolder.SetActive(false);
        if(playerController.poisonStacks > 0){
            poisonStacks.text =  playerController.poisonStacks.ToString();
            poisonHolder.SetActive(true);
        } 
        else poisonHolder.SetActive(false);

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
        manager.resumeGame();
        manager.saveToManager();
        gameManager.saveGame();
        SceneManager.LoadScene("Sidescroll");
    }


    public void spinanim(){
        spinner.GetComponent<Animator>().Play("spin");
    }

    public void startDialogue(){
        dialogueHolder.SetActive(true);
    }

    public void endDialogue(){
        dialogueHolder.SetActive(false);
    }

    public void presentDeathScreen(){
        deathScreen.SetActive(true);
        if(gameManager.difficulty == 0){
            deathScreenText.text = "You've lost half your loot.";
            manager.exitEasy();
        }
        else if(gameManager.difficulty == 1){
            deathScreenText.text = "You've lost all your loot.";
            manager.exitNormal();
        }
        else{
            deathScreenText.text = "Your journey ends after " + gameManager.day + " days.";
            manager.exitHard();
        }
    }

    public void exitDeathSceen(){
        deathScreen.SetActive(false);
        manager.resumeGame();
        if(gameManager.difficulty == 0){
            SceneManager.LoadScene("Sidescroll");
        }
        else if(gameManager.difficulty == 1){
            SceneManager.LoadScene("Sidescroll");
        }
        else{
            SceneManager.LoadScene("Menu");
        }
    }

}
