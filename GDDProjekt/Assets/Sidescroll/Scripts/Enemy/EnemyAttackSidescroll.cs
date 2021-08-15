using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EnemyAttackSidescroll : MonoBehaviour
{
    //public
    public int life;
    public int maxLife;
    public int damageWall;
    public int damageNpc;
    public int agro;    //1 = Wall, 2 = Npc, 3 = Wall and Npc
    public int cooldownHitWall;
    public int cooldownHitNpc;
    public float bulletSpeed;
    public Transform firepoint;
    public GameObject bulletNpcPrefab;
    public GameObject bulletWallPrefab;
    public List<Transform> npcs;
    public GameObject wall;
    public bool isFly;

    //private
    float timer;
    float npcTimer;
    EnemyMovementSidescroll enemyMovementSidescroll;
    WallSidescroll wallSidescroll;

    void Start()
    {
        enemyMovementSidescroll = GetComponent<EnemyMovementSidescroll>();
        wallSidescroll = wall.GetComponent<WallSidescroll>();
    }

    void Update()
    {
        timer += Time.deltaTime;
        npcTimer += Time.deltaTime;

        if ((!enemyMovementSidescroll.move || isFly) && !enemyMovementSidescroll.gameEnd)
        {
            switch (agro)
            {
                case 1:
                    if (timer >= cooldownHitWall) hitWall();
                    break;
                case 2:
                    if (npcTimer >= cooldownHitNpc) hitNpc();
                    break;
                case 3:
                    if (npcTimer >= cooldownHitNpc && npcs.Count != 0) hitNpc();
                    if (timer >= cooldownHitWall) hitWall();
                    break;
            }
        }

        if (life <= 0) Destroy(gameObject);
    }

    void hitWall()
    {
        timer = 0;

        wallSidescroll.life -= damageWall;
    }

    void hitNpc()
    {
        npcTimer = 0;

        if (npcs.Count > 0)
        {
            if (npcs.First() == null)
            {
                npcs.Remove(npcs.First());
                hitNpc();
            }
            else
            {
                Vector2 direction = (npcs.First().position - firepoint.position).normalized;
                GameObject bullet = Instantiate(bulletNpcPrefab, firepoint.position, firepoint.rotation);
                bullet.GetComponent<BulletEnemyNPCSidescroll>().damage = damageNpc;
                bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
            }
        }
        else
        {
            Vector2 direction = (wall.transform.position - firepoint.position).normalized;
            GameObject bullet = Instantiate(bulletWallPrefab, firepoint.position, firepoint.rotation);
            bullet.GetComponent<BulletEnemyWallSidescroll>().damage = damageWall;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
    }

    public void takeDamage(int damage)
    {
        life -= damage;
    }
}
