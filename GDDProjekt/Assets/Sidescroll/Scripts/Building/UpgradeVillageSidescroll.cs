using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UpgradeVillageSidescroll : BuildingOverClassSidescroll
{
    //public variables
    public GameObject menu;

    //private variables
    PlayerMovementSidescroll playerMovementSidescroll;
    Canvas menuCanvas;
    

    void Start()
    {
        playerMovementSidescroll = player.GetComponent<PlayerMovementSidescroll>();
        menuCanvas = menu.GetComponent<Canvas>();
    }

    public override void activateMenu()
    {
        playerMovementSidescroll.inEvent = true;
        menuCanvas.enabled = true;
    }

    public override void deactivateMenu()
    {
        playerMovementSidescroll.inEvent = false;
        menuCanvas.enabled = false;
    }
}
