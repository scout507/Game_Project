using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderQuerySidescroll : MonoBehaviour
{
    //private variables
    BuildingOverClassSidescroll buildingSidescroll;

    void Update()
    {
        if (buildingSidescroll && Input.GetKeyDown(KeyCode.E) && !(GetComponent<PlayerMovementSidescroll>().inEvent)){
            buildingSidescroll.activateMenu();
        } else if (buildingSidescroll && Input.GetKeyDown(KeyCode.E) && GetComponent<PlayerMovementSidescroll>().inEvent){
            buildingSidescroll.deactivateMenu();
        }
    }

    void OnTriggerEnter2D(Collider2D other){
        if (other.tag == "building"){
            buildingSidescroll = other.GetComponent<BuildingOverClassSidescroll>();
            buildingSidescroll.highlightBuilding();
        }
    }

    void OnTriggerExit2D(Collider2D other){
        if (other.tag == "building"){
            buildingSidescroll.dehighlightBuilding();
            buildingSidescroll = null;
        }
    }
}
