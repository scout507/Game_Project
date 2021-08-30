using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SleepAndDungeonSidescroll : BuildingOverClassSidescroll
{
    //public variables
    public GameObject canvas;
    public GameObject menu;

    //private variables
    PlayerMovementSidescroll playerMovementSidescroll;
    Canvas highlightCanvas;
    Canvas menuCanvas;

    void Start()
    {
        highlightCanvas = canvas.GetComponent<Canvas>();
        menuCanvas = menu.GetComponent<Canvas>();
        playerMovementSidescroll = player.GetComponent<PlayerMovementSidescroll>();
    }

    public override void activateMenu()
    {
        playerMovementSidescroll.inEvent = true;
        highlightCanvas.enabled = false;
        menuCanvas.enabled = true;
    }

    public override void deactivateMenu()
    {
        playerMovementSidescroll.inEvent = false;
        highlightCanvas.enabled = true;
        menuCanvas.enabled = false;
    }

    public void intoDungeon()
    {
        SceneManager.LoadScene("MapGeneration");
    }

    public void intoDefense()
    {
        SceneManager.LoadScene("Defense");
    }
}
