using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class DungeonSidescroll : BuildingOverClassSidescroll
{
    //public variables
    public GameObject canvas;
    public GameObject menu;
    public GameManager gameManager;
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
            if (gameManager.wasInDungeon)
            {
                textMeshProTitle.text = "Du musst erst schlafen";
                textMeshProTitleInteract.text = "";
            }
            else
            {
                textMeshProTitle.text = "Dungeon";
                textMeshProTitleInteract.text = "[E]";
            }
        }
    }

    public override void activateMenu()
    {
        if (!gameManager.wasInDungeon)
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

    public void intoDungeon()
    {
        gameManager.wasInDungeon = true;
        gameManager.hasSlept = false;
        gameManager.saveGame();
        SceneManager.LoadScene("MapGeneration");
    }
}
