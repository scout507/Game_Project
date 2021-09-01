using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    [Tooltip("Sprites: 0: down, 1: down-right, 2: right, 3: top-right, 4: top, 5: top-left, 6: left, 7: down-left")]
    public Sprite[] sprites;
    public float hp;
    public float dmg;
    
    public float moveSpeed;
    public float atkSpeed;
    public float meleeAtkRange;
    public float rangeDmg;
    public bool hasRangedAtk;
    public int posionChance;
    public int slowChance;
    
    public string meleeAtkSound;
    public string rangeAtkSound;
    public string dmgSound;
    public string deathSound;

    public int lootWeight;
    public GameObject bullet;
    
    Pathfinding.AIPath pathing;
    Pathfinding.AIDestinationSetter destSetter;
    private SpriteRenderer sR;
    private GameObject player;
    bool isDmging;
    float atkDelay = 0.5f;
    float rangeAtkRange = 10;
    private bool aggro;
    float aggroRange = 20;
    private bool dead;
    bool takingDmg;
    private bool hasAtkd;
    private float aktTimer;
    private float shotTimer;
    private float dmgPopTimer;
    private float distanceToEnemy;
    public Transform dmgPopUp;
    Rigidbody2D rb;
    PlayerStats playerStats;
    PlayerController playerController;
    Vector2 lookDir;
    LootTable lootTable;
    Manager manager;
    SoundManager soundManager;
    public DmgPopUp lastPopUp;

    Texture2D cursor;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerStats = player.GetComponent<PlayerStats>();
        playerController = player.GetComponent<PlayerController>();
        pathing = GetComponent<Pathfinding.AIPath>();
        destSetter = GetComponent<Pathfinding.AIDestinationSetter>();
        destSetter.target = player.transform;
        sR = GetComponentInChildren<SpriteRenderer>();
        lootTable = GameObject.FindGameObjectWithTag("manager").GetComponent<LootTable>();
        manager = GameObject.FindGameObjectWithTag("manager").GetComponent<Manager>();
        pathing.maxSpeed = moveSpeed;
        soundManager = FindObjectOfType<SoundManager>();
        cursor = (Texture2D)Resources.Load("Cursors/Cursor_Move2");
    }



    void Update()
    {   
        dmgPopTimer += Time.deltaTime;
        if(dmgPopTimer >= 0.1f){
            lastPopUp = null;
        }

        distanceToEnemy = Vector3.Distance(player.transform.position, this.transform.position);
        if(distanceToEnemy < aggroRange) aggro =  true;
        if(aggro && manager.rdy){
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
            Invoke("startMoving", atkDelay);
        }
        if(distanceToEnemy <= rangeAtkRange && !hasAtkd && hasRangedAtk){
            pathing.canMove = false;
            hasAtkd = true;
            rangeAtk();
            rb.velocity = Vector3.zero;
            Invoke("startMoving", atkDelay);
        }
    }

    void FixedUpdate()
    {
        lookDir = new Vector2(player.transform.position.x, player.transform.position.y) - rb.position;
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
        isDmging = true;
        playerStats.takeDamage(dmg);
        //if(Random.Range(1,101) <= slowChance) playerController.getSlowed(2.5f);
        //if(Random.Range(1,101) <= posionChance) playerController.getPoisoned(Random.Range(1,4));
        //soundManager.PlayOnToggle(meleeAtkSound, isDmging);
        isDmging = false;
    }

    void rangeAtk(){
        isDmging = true;
        GameObject shot = Instantiate(bullet, transform.position, Quaternion.identity);
        MonsterBullet bulletScript = shot.GetComponent<MonsterBullet>();
        bulletScript.dmg = rangeDmg;
        bulletScript.target = new Vector3(player.transform.position.x,player.transform.position.y,player.transform.position.z);
        bulletScript.poisionChance = posionChance;
        bulletScript.slowChance = slowChance;
        shot.GetComponent<Rigidbody2D>().AddForce(lookDir*3f, ForceMode2D.Impulse);
        //soundManager.PlayOnToggle(rangeAtkSound, isDmging);
        isDmging = false;
    }

    public void takeDamage(float dmgTaken, Vector3 force){
        takingDmg = true;
        pathing.canMove = false;
        Invoke("startMoving", 0.1f);
        pathing.canMove = false;
        rb.AddForce(force,ForceMode2D.Impulse);
        //soundManager.PlayOnToggle(dmgSound, takingDmg);

        if(lastPopUp == null){
            lastPopUp = spawnDmgText(dmgTaken);
            dmgPopTimer = 0f;
        }else{
            lastPopUp.updateText(dmgTaken);
        }

        hp -= dmgTaken;
        if(hp <= 0){
            die();
        }
        takingDmg = false;
    }

    void die(){
        //FindObjectOfType<SoundManager>().Play(deathSound);
        if(!dead){
            dead = true;
            if(Random.Range(0,100)<50) dropLoot();
            Destroy(this.gameObject);
        }  
    }

    void dropLoot(){
       GameObject loot = Instantiate(lootTable.loot[lootTable.roll(lootWeight)], this.transform.position, Quaternion.identity);
    }

    DmgPopUp spawnDmgText(float dmgTaken){
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y+1.5f,0);
        Transform damagePopUpTrans = Instantiate(dmgPopUp, transform.position, Quaternion.identity);
        DmgPopUp dmgPopUpScript = damagePopUpTrans.GetComponent<DmgPopUp>();;
        dmgPopUpScript.Setup(dmgTaken);
        return dmgPopUpScript;
    }

    void startMoving(){
        pathing.canMove = true;
    }

    //vvv Aien vvv
    void OnMouseEnter()
    {
        Cursor.SetCursor(cursor, new Vector2(32, 32), CursorMode.Auto);
    }

    void OnMouseExit()
    {
        Cursor.SetCursor(null, Vector2.zero, CursorMode.Auto);
    }

    
}
