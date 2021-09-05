using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float dmg;
    public Vector3 bulletforce;
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Player" && other.tag != "allowBullets" && other.tag != "collectible" && other.tag != "portal"){
            Destroy(this.gameObject);
            if(other.tag == "monster"){
                other.GetComponent<MonsterController>().takeDamage(dmg, bulletforce);
            }
            else if(other.tag == "destructable"){
                other.GetComponent<DestructableProp>().die();
            }
            else if(other.tag == "boss"){
                other.GetComponent<BossController>().takeDamage(dmg);
            }
        }
    }
}
