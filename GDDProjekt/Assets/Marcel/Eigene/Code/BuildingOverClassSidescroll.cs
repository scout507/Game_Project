using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class BuildingOverClassSidescroll : MonoBehaviour
{
    //public variables
    public GameObject player;
    abstract public void activateMenu();
    abstract public void deactivateMenu();

    public void highlightBuilding()
    {
        GetComponentInChildren<Canvas>().enabled = true;
    }

    public void dehighlightBuilding()
    {
        GetComponentInChildren<Canvas>().enabled = false;
    }
}