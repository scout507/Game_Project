using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemyWallSidescroll : MonoBehaviour
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
        if (hitInfo.tag == "wall" && !isTriggered)
        {
            isTriggered = true;
            hitInfo.GetComponentInParent<WallSidescroll>().takeDamage(damage);
            Destroy(gameObject);
        }
    }
}
