using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float hp;
    public float maxhp;
    public int[] loot;
    public int essence;

    void Start()
    {
        hp = 100f;
        maxhp = 100f;
    }

    
    void Update()
    {
        essence = 0;
        for(int i = 4; i<loot.Length; i++){
            essence += loot[i];
        }
        
    }

    public void takeDamage(float dmg){
        hp -= dmg;
        if(hp <= 0) die();
    }

    void die(){

    }
}
