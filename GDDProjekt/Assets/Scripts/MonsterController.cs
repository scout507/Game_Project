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
    public Sprite[] sprites;
    private SpriteRenderer sR;
    
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
        sR = GetComponentInChildren<SpriteRenderer>();
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

    void FixedUpdate()
    {
        Vector2 lookDir = new Vector2(player.transform.position.x, player.transform.position.y) - rb.position;
        float facing = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //down = -112,5 - -67,5 / down-right -67,5 - -22,5 / right -22,5 - 22,5 / top right = 22,5 - 67,5 / top = 67,5 - 112,5 / top-left = 157,5 / left = < 157,5 | > -157,5 / down-left = -112,5 - -157,5
        if(facing >= -112.5f && facing < -67.5f) sR.sprite = sprites[0]; //down
        else if(facing >= -67.5f && facing < -22.5f) sR.sprite = sprites[1]; //donw-right
        else if(facing >= -22.5f && facing < 22.5f) sR.sprite = sprites[2]; //right
        else if(facing >= 22.5f && facing < 67.5f) sR.sprite = sprites[3]; //top-right
        else if(facing >= 67.5f && facing < 112.5f) sR.sprite = sprites[4]; //top
        else if(facing >= 112.5f && facing < 157.5f) sR.sprite = sprites[5]; //top-left
        else if(facing >= -157.5f && facing < -112.5f) sR.sprite = sprites[7]; //down-left
        else sR.sprite = sprites[6]; //left
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
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y+1.5f,0);
        Transform damagePopUpTrans = Instantiate(dmgPopUp, transform.position, Quaternion.identity);
        DmgPopUp dmgPopUpScript = damagePopUpTrans.GetComponent<DmgPopUp>();;
        dmgPopUpScript.Setup(dmgTaken);
    }

    void startMoving(){
        pathing.canMove = true;
    }

    
}
