using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    float timer;
    float dmg = 25f;
    public GameObject explo;
    bool exploded;

    void Awake()
    {
        timer = Random.Range(1f, 2.5f);
    }

    
    void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0) explode();
    }

    void explode(){
        if(!exploded){
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1.5f);
            foreach (Collider2D obj in colliders){
                if(obj.tag == "Player"){
                    obj.GetComponent<PlayerController>().getStunned(2f);
                    obj.GetComponent<PlayerStats>().takeDamage(dmg);
                } 
            }
            explo = Instantiate(explo, transform.position, Quaternion.identity);
            Invoke("destroy", 0.5f);
            exploded = true; 
        } 
    }

    void destroy(){
        Destroy(explo);
        Destroy(this.gameObject);
    }
}
