using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class NpcAttackSidescroll : MonoBehaviour
{
    //public
    public int life;
    public int maxLife;
    public int damage;
    public int agro;    //1 = Npc below, 2 = Npc below and above
    public float cooldownHit;
    public float bulletSpeed;
    public Transform firepoint;
    public GameObject bulletNpcPrefab;
    public List<Transform> enemysBelow;
    public List<Transform> enemysAbove;

    //private
    float timer;
    NpcMovementSidescroll npcMovementSidescroll;
    EnemyAttackSidescroll enemyAttackSidescroll;

    void Start()
    {
        timer = cooldownHit;
        npcMovementSidescroll = GetComponent<NpcMovementSidescroll>();
        enemyAttackSidescroll = GetComponent<EnemyAttackSidescroll>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!npcMovementSidescroll.move && !npcMovementSidescroll.gameEnd && timer >= cooldownHit)
        {
            switch (agro)
            {
                case 1:
                    hitEnemyBelow();
                    break;
                case 2:
                    if (enemysAbove.Count != 0) hitEnemyAbove();
                    else hitEnemyBelow();
                    break;
            }
        }

        if (life <= 0) Destroy(gameObject);
    }

    void hitEnemyBelow()
    {
        timer = 0;

        if (!enemysBelow.Any())
        {
            if (enemysBelow[0] == null) enemysBelow.RemoveAt(0);
            Vector2 direction = (enemysBelow[0].position - firepoint.position).normalized;
            GameObject bullet = Instantiate(bulletNpcPrefab, firepoint.position, firepoint.rotation);
            bullet.GetComponent<BulletNpcToEnemey>().damage = damage;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
    }

    void hitEnemyAbove()
    {
        timer = 0;

        if (!enemysAbove.Any())
        {
            if (enemysAbove[0] == null) enemysAbove.RemoveAt(0);
            Vector2 direction = (enemysAbove[0].position - firepoint.position).normalized;
            GameObject bullet = Instantiate(bulletNpcPrefab, firepoint.position, firepoint.rotation);            
            bullet.GetComponent<BulletNpcToEnemey>().damage = damage;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
        else hitEnemyBelow();
    }

    public void takeDamage(int damage)
    {
        life -= damage;
        if (life <= 0) Destroy(gameObject);
    }
}