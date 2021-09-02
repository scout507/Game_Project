using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootTable : MonoBehaviour
{
    public GameObject[] loot;
    public int total;
    int[] lootTable = {
        800,
        400,
        200,
        100,
        25,
        12,
        6,
        3
    };
    
    private void Start()
    {
        foreach (int chance in lootTable)
        {
            total += chance;
        }
    }
    

    public int roll(int weight){
        int r = Random.Range(0,total)+weight;
        
        for(int i = 0; i<lootTable.Length; i++){
            if(r < lootTable[i]) return i;
            else r -= lootTable[i];
        }
        return lootTable.Length-1;
    }

    public int rollTresure(){

        int r = Random.Range(800,1500);
            
            for(int i = 0; i<lootTable.Length; i++){
                if(r < lootTable[i]) return i;
                else r -= lootTable[i];
            }
            return lootTable.Length-1;
    }    
}
