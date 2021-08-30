using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public float dmg;
    public Vector3 target;
    public Vector3 bulletforce;
    public float splashRadius = 5f;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "monster" && other.tag != "allowBullets" && other.tag != "collectible" && other.tag != "portal"){
            if(other.tag == "Player"){
                splash();
            }
        }
    }

    void Update(){
        if(Vector3.Distance(this.transform.position, target) <= 0.5f) splash();
    }

    void splash(){
        
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, splashRadius);
        foreach (Collider2D obj in colliders)
        {
            if(obj.tag == "Player"){
                obj.GetComponent<PlayerStats>().takeDamage(dmg);
            }
        }
        Destroy(this.gameObject);
    }
}
