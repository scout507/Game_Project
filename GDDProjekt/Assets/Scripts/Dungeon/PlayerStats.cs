using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float hp;
    public float maxhp;
    public int[] loot;
    public int essence;
    public bool dead = false;

    public float energyShield;
    public float maxEnergyShield;
    public float esRechargeRate;
    public float esTimer;

    float lastHitTimer;
    CamController camScript;
   

    void Start()
    {
        hp = 100f;
        maxhp = 100f;
        energyShield = maxEnergyShield;
        camScript = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CamController>();
    }

    
    void Update()
    {
        essence = 0;
        for(int i = 4; i<loot.Length; i++){
            essence += loot[i];
        }
        
        lastHitTimer += Time.deltaTime;
        if(lastHitTimer >= esTimer) energyShield += esRechargeRate * Time.deltaTime;
        if(energyShield > maxEnergyShield) energyShield = maxEnergyShield;

    }

    public void takeDamage(float dmg){
        if(dmg >= 30) camScript.shake(0.05f,0.1f);
        else if(dmg >= 10) camScript.shake(0.05f,0.05f);
        lastHitTimer = 0f;
        if(energyShield > dmg) energyShield -= dmg;
        else if(energyShield > 0){
            dmg -= energyShield;
            energyShield = 0;
            hp -= dmg;
        }
        else hp -= dmg; 
        if(hp <= 0) die();
    }

    void die(){
        if(!dead){
            dead = true;
            GameObject.FindGameObjectWithTag("manager").GetComponent<UIController>().presentDeathScreen();
        }
    }
}
