using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public float hp;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }

    public void takeDamage(float dmg){
        hp -= dmg;
        if(hp <= 0) die();
    }

    void die(){

    }
}
