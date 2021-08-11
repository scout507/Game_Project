using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableProp : MonoBehaviour
{
    LootTable lootTable;
    public int lootWeight;

    private void Start() {
        lootTable = GameObject.FindGameObjectWithTag("manager").GetComponent<LootTable>();
    }

    public void die(){
        Instantiate(lootTable.loot[lootTable.roll(lootWeight)], this.transform.position, Quaternion.identity);
        Destroy(this.gameObject);        
    }
}
