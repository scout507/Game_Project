using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SleepSidescroll : BuildingOverClassSidescroll
{
    //public variables
    public GameObject canvas;
    public GameObject menu;
    GameManager gameManager;
    public GameObject title;
    public GameObject interact;

    //private variables
    PlayerMovementSidescroll playerMovementSidescroll;
    Canvas highlightCanvas;
    Canvas menuCanvas;
    TextMeshProUGUI textMeshProTitle;
    TextMeshProUGUI textMeshProTitleInteract;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        highlightCanvas = canvas.GetComponent<Canvas>();
        menuCanvas = menu.GetComponent<Canvas>();
        playerMovementSidescroll = player.GetComponent<PlayerMovementSidescroll>();
        textMeshProTitle = title.GetComponent<TextMeshProUGUI>();
        textMeshProTitleInteract = interact.GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (highlightCanvas.enabled)
        {
            if (gameManager.hasSlept)
            {
                textMeshProTitle.text = "Du musst erst in den Dungeon";
                textMeshProTitleInteract.text = "";
            }
            else
            {
                textMeshProTitle.text = "Bed";
                textMeshProTitleInteract.text = "[E]";
            }
        }
    }

    public override void activateMenu()
    {
        if (!gameManager.hasSlept)
        {
            playerMovementSidescroll.inEvent = true;
            highlightCanvas.enabled = false;
            menuCanvas.enabled = true;
        }
    }

    public override void deactivateMenu()
    {

        playerMovementSidescroll.inEvent = false;
        highlightCanvas.enabled = true;
        menuCanvas.enabled = false;
    }

    public void intoDefense()
    {
        gameManager.day++;
        gameManager.saveGame();
        gameManager.hasSlept = true;
        gameManager.wasInDungeon = false;

        if (gameManager.day % 3 == 0)
        {
            SceneManager.LoadScene("Defense");
        }
        else
        {
            deactivateMenu();
        }
    }
}
