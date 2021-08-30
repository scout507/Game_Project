using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWeaponsSidescroll : BuildingOverClassSidescroll
{
    //private variables
    PlayerMovementSidescroll playerMovementSidescroll;

    void Start()
    {
        playerMovementSidescroll = player.GetComponent<PlayerMovementSidescroll>();
    }

    public override void activateMenu()
    {
        playerMovementSidescroll.inEvent = true;
    }

    public override void deactivateMenu()
    {
        playerMovementSidescroll.inEvent = false;
    }
}
