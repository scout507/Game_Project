using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float hp;
    public float dmg;
    public float moveSpeed;
    public float atkSpeed;
    public float meleeAtkRange;
    public float aggroRange;
    public bool aggro;

    public GameObject player;
    public float distanceToEnemy;
    Pathfinding.AIPath pathing;
    Pathfinding.AIDestinationSetter destSetter;
    public Transform dmgPopUp;
    
    private bool hasAtkd;
    private float aktTimer;
    private float shotTimer;

    Rigidbody2D rb;
    PlayerStats playerStats;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        pathing = GetComponent<Pathfinding.AIPath>();
        destSetter = GetComponent<Pathfinding.AIDestinationSetter>();
        destSetter.target = player.transform;
    }

    
    void Update()
    {
        distanceToEnemy = Vector3.Distance(player.transform.position, this.transform.position);
        if(distanceToEnemy < aggroRange) aggro =  true;
        if(aggro){
            pathing.canSearch = true;
        }
        if(hasAtkd) aktTimer += Time.deltaTime;
        if(aktTimer >= atkSpeed){
            aktTimer = 0;
            hasAtkd = false;
        }
        if(distanceToEnemy <= meleeAtkRange && !hasAtkd){
            pathing.canMove = false;
            hasAtkd = true;
            meleeAtk();
            rb.velocity = Vector3.zero;
            Invoke("startMoving", 0.5f);
        }
    }

    void meleeAtk(){
        playerStats.takeDamage(dmg);
    }

    public void takeDamage(float dmgTaken, Vector3 force){
        pathing.canMove = false;
        Invoke("startMoving", 0.1f);
        pathing.canMove = false;
        rb.AddForce(force,ForceMode2D.Impulse);
        spawnDmgText(dmgTaken);
        hp -= dmgTaken;
        if(hp <= 0){
            die();
        }
    }

    void die(){
        Destroy(this.gameObject);
    }

    void spawnDmgText(float dmgTaken){
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y+1,0);
        Transform damagePopUpTrans = Instantiate(dmgPopUp, transform.position, Quaternion.identity);
        DmgPopUp dmgPopUpScript = damagePopUpTrans.GetComponent<DmgPopUp>();;
        dmgPopUpScript.Setup(dmgTaken);
    }

    void startMoving(){
        pathing.canMove = true;
    }

    
}
