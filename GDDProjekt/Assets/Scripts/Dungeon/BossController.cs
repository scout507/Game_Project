using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    [Tooltip("Sprites: 0: down, 1: down-right, 2: right, 3: top-right, 4: top, 5: top-left, 6: left, 7: down-left")]
    public Sprite[] sprites;
    public Transform dmgPopUp;
    public DmgPopUp lastPopUp;

    public int bosslevel;
    public float hp;
    public float maxhp;
    public float dmg;
    public float moveSpeed;
    public float leapSpeed;
    public float atkSpeed;
    public float meleeAtkRange;
    public float slamRadius;
    public float slamDamage;

    public float coolDown;    
    public int lootWeight; 

    public float skill1Cd; 
    public float skill2Cd; 
    public float skill3Cd; 
    public float skill4Cd; 

    float atkTimer;    // Auto-Atk
    float skill1Timer; // Slam
    float skill2Timer; // Projectiles   
    float skill3Timer; // Minion
    float skill4Timer; // WIP

    public float stunDuration;
        
    public GameObject projectile;
    public GameObject minion;
    public Vector3[] positions;

    bool dead;
    bool moveblock;
    float moveblockDuration;
    float dmgPopTimer;
    Vector2 lookDir;
    SpriteRenderer sR;
    GameObject player;
    Rigidbody2D rb;
    LootTable lootTable;
    Vector2 target;

    bool skill1Active;
    bool skill2Active;
    bool skill3Active;
    bool skill4Active;


    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player");
        hp = maxhp;
        lootTable = GameObject.FindGameObjectWithTag("manager").GetComponent<LootTable>();

        skill1Timer = skill1Cd;
        skill2Timer = skill2Cd;
        skill3Timer = skill3Cd;
        skill4Timer = skill4Cd;
    }

    // Update is called once per frame
    void Update()
    {
        atkTimer -= Time.deltaTime;
        skill1Timer -= Time.deltaTime;
        skill2Timer -= Time.deltaTime;
        skill3Timer -= Time.deltaTime;
        skill4Timer -= Time.deltaTime;
        coolDown -= Time.deltaTime;
        moveblockDuration -= Time.deltaTime;

        if(moveblockDuration > 0) moveblock = true;
        else moveblock = false;

        dmgPopTimer += Time.deltaTime;
        if(dmgPopTimer >= 0.1f){
            lastPopUp = null;
        }

        //Skill 1
        if(skill1Active && Vector2.Distance(transform.position, target) <= 1f) slam();


        if(hp <= 0) die();
        decider();
    }

    private void FixedUpdate()
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


        //constant moving
        if(target != new Vector2(0,0) && Vector2.Distance(target, transform.position) >= meleeAtkRange && !moveblock){
            // move
            Vector2 direction = new Vector2(target.x - transform.position.x, target.y-transform.position.y);
            rb.velocity = direction.normalized*moveSpeed*Time.fixedDeltaTime;
        }
    }

    void decider(){
        if(Vector2.Distance(transform.position, player.transform.position) <= meleeAtkRange && atkTimer <= 0){
            autoAtk();
            coolDown = 1.5f;
        } 
        else if(coolDown <= 0){
            target = new Vector2(0,0);
            if(skill4Timer <= 0 && bosslevel >= 4){
                skill4();
            }
            else if(skill3Timer <= 0 && bosslevel >= 3){
                skill3();
            }
            else if(skill2Timer <= 0 && bosslevel >= 2){
                skill2();
            }
            else if(skill1Timer <= 0){
                skill1();
            }
            coolDown = 1.5f;
        }
        if(!moveblock) move();
        //do nothing;
    }

    void autoAtk(){
        atkTimer = atkSpeed;
        rb.velocity = Vector2.zero;
        moveblockDuration = 1f;
        player.GetComponent<PlayerStats>().takeDamage(dmg);
    }

    void move(){
        target = new Vector2(player.transform.position.x,player.transform.position.y);
    }

    void leap(){
        Debug.Log("leaping");
        target = new Vector2(player.transform.position.x,player.transform.position.y);
        Vector2 direction = new Vector2(target.x - transform.position.x, target.y-transform.position.y);
        rb.AddForce(direction*leapSpeed, ForceMode2D.Impulse);
        moveblockDuration = 2f;
    }

    void skill1(){
        skill1Timer = skill1Cd;
        leap();
        skill1Active = true;
    }

    void skill2(){
        moveblockDuration = 1f;
        skill2Timer = skill2Cd;
        List<int> randomList = new List<int>();

        for(int i = 0; i < 20; i++){
            randomList.Add(i);
        }

        int projectileNumber = Random.Range(3,7);
        for(int j = 0; j < projectileNumber; j++){
            int r = randomList[Random.Range(0,randomList.Count)];
            randomList.Remove(r);
            Vector3 spawnSpot = positions[r];
            Instantiate(projectile, spawnSpot, Quaternion.identity);
        }
    }

    void skill3(){

        moveblockDuration = 1f;
        skill3Timer = skill3Cd;
        List<int> randomList = new List<int>();
        for(int i = 0; i < 20; i++){
            randomList.Add(i);
        }
        int minionNumber = Random.Range(5,11);
        for(int j = 0; j < minionNumber; j++){
            int r = randomList[Random.Range(0,randomList.Count)];
            randomList.Remove(r);
            Vector3 spawnSpot = positions[r];
            GameObject min = Instantiate(minion, spawnSpot, Quaternion.identity);
        }

    }

    void skill4(){
        skill4Timer = skill4Cd;
    }
    
    void die(){
        if(!dead){
            GameObject.FindGameObjectWithTag("manager").GetComponent<Manager>().spawnPortal();
            dead = true;
            Destroy(this.gameObject);
        } 
        GameObject loot = Instantiate(lootTable.loot[lootTable.roll(lootWeight)], this.transform.position, Quaternion.identity);
    }

    void slam(){
        skill1Active = false;
        rb.velocity = Vector2.zero;
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, slamRadius);

         foreach (Collider2D obj in colliders){
                if(obj.tag == "Player"){
                    obj.GetComponent<PlayerController>().getStunned(stunDuration);
                    obj.GetComponent<PlayerStats>().takeDamage(slamDamage);
                    moveblockDuration = 0;
                } 
        }    
    }
    
    public void takeDamage(float dmgTaken){
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
    }

    DmgPopUp spawnDmgText(float dmgTaken){
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y+1.5f,0);
        Transform damagePopUpTrans = Instantiate(dmgPopUp, transform.position, Quaternion.identity);
        DmgPopUp dmgPopUpScript = damagePopUpTrans.GetComponent<DmgPopUp>();;
        dmgPopUpScript.Setup(dmgTaken);
        return dmgPopUpScript;
    }
}

