using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public float dmg;
    public Vector3 target;
    public Vector3 bulletforce;
    public float splashRadius = 5f;
    public int slowChance;
    public int poisionChance;


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
                if(Random.Range(1,101) >= slowChance) obj.GetComponent<PlayerController>().getSlowed(2.5f);
                if(Random.Range(1,101) >= poisionChance) obj.GetComponent<PlayerController>().getPoisoned(Random.Range(1,4));
            }
        }
        Destroy(this.gameObject);
    }
}
