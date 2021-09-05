using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyNPCSidescroll : MonoBehaviour
{
    //public variables
    public int damage;

    //private variables
    float timer = 0;
    bool isTriggered = false;

    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 5)
            Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (hitInfo.tag == "npc" && !isTriggered)
        {
            isTriggered = true;
            hitInfo.GetComponent<NpcAttackSidescroll>().takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
