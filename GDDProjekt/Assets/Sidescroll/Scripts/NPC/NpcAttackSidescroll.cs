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
    public int agro;    //1 = Npc normal, 2 = Npc sniper, 3 = Npc Mortar
    public float cooldownHit;
    public float bulletSpeed;
    public Transform firepoint;
    public GameObject bulletNpcPrefab;
    public GameObject bulletMortarNpcPrefab;
    public List<GameObject> enemysBelow;
    public List<GameObject> enemysAbove;
    public AiManager aiManager;

    //private
    float timer;
    NpcMovementSidescroll npcMovementSidescroll;

    void Start()
    {
        timer = cooldownHit;
        npcMovementSidescroll = GetComponent<NpcMovementSidescroll>();
        life = maxLife;
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (!npcMovementSidescroll.move && !aiManager.gameEndNpc && timer >= cooldownHit && !aiManager.stopNpcs)
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
                case 3:
                    hitEnemyBelowMassiv();
                    break;
            }
        }

        die();
    }

    void hitEnemyBelow()
    {
        timer = 0;

        if (enemysBelow.Count > 0)
        {
            Vector2 direction = (enemysBelow.First().transform.position - firepoint.position).normalized;
            GameObject bullet = Instantiate(bulletNpcPrefab, firepoint.position, firepoint.rotation);
            bullet.GetComponent<BulletNpcToEnemey>().damage = damage;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;
        }
    }

    void hitEnemyAbove()
    {
        timer = 0;

        if (enemysAbove.Count > 0)
        {
            Vector2 direction = (enemysAbove.First().transform.position - firepoint.position).normalized;
            GameObject bullet = Instantiate(bulletNpcPrefab, firepoint.position, firepoint.rotation);
            bullet.GetComponent<BulletNpcToEnemey>().damage = damage;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        }
        else hitEnemyBelow();
    }

    void hitEnemyBelowMassiv()
    {
        timer = 0;

        if (enemysBelow.Count > 0)
        {
            Vector2 direction = (enemysBelow.First().transform.position - firepoint.position).normalized;
            GameObject bullet = Instantiate(bulletMortarNpcPrefab, firepoint.position, firepoint.rotation);
            bullet.GetComponent<BulletMortarNpcToEnemy>().damage = damage;
            bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        }
    }

    public void takeDamage(int damage)
    {
        life -= damage;
        if (life <= 0) Destroy(gameObject);
    }

    void die()
    {
        if (life <= 0)
        {
            aiManager.npcsLeft.Remove(gameObject);
            aiManager.npcsRight.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}