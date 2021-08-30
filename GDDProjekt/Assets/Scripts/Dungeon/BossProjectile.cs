using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    float timer;
    float dmg = 25f;

    void Awake()
    {
        timer = Random.Range(2f, 3.5f);
    }

    
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0) explode();
    }

    void explode(){
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);
        foreach (Collider2D obj in colliders){
            if(obj.tag == "Player"){
                obj.GetComponent<PlayerController>().getStunned(0.8f);
                obj.GetComponent<PlayerStats>().takeDamage(dmg);
            } 
        }
        Destroy(this.gameObject);    
    }
}
