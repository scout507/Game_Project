using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletTurretSidescroll : MonoBehaviour
{
    //public variables
    public int damage = 0;
    public GameObject parent;

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

        if (hitInfo.tag == "enemy" && !isTriggered)
        {
            isTriggered = true;
            hitInfo.gameObject.GetComponent<EnemyAttackSidescroll>().takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
