using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMortarNpcToEnemy : MonoBehaviour
{
    //public variables
    public int damage;
    public int exRadius;

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
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, exRadius);
            foreach (var item in colliders)
            {
                if (item.tag == "enemy")
                    item.GetComponent<EnemyAttackSidescroll>().takeDamage(damage);
            }
            Destroy(gameObject);
        }
    }
}
