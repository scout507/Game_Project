using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    [Tooltip("Sprites: 0: down, 1: down-right, 2: right, 3: top-right, 4: top, 5: top-left, 6: left, 7: down-left")]
    public Sprite[] sprites;
    public LayerMask noMove;
    public float moveSpeed;
    public float regularMoveSpeed =5f;
    public float dashForce;
    public float dashDuration;
    public float dashTimer = 0f;
    public Camera cam;
    
    
    public Transform gun;
    public Transform gunHolder;
    public GameObject[] guns;
    [HideInInspector]
    public bool weaponOneActive = true;
    public GameObject activeGun;


    float angle;
    float facing;
    public bool moveBlock;
    float slowTimer;
    public float moveBlockTimer;

    public Weapon gunscript;
    Vector2 mouse;
    SpriteRenderer sR;
    Rigidbody2D rb;
    Vector2 movement;
    UIController uIController;
    Manager manager;

    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        uIController = GameObject.FindGameObjectWithTag("manager").GetComponent<UIController>();
        manager = GameObject.FindGameObjectWithTag("manager").GetComponent<Manager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetButton("Fire1") && !manager.paused) activeGun.GetComponent<Weapon>().shoot(gun.position,gun.up, Quaternion.Euler(0,0,facing) , Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if(Input.GetKeyDown(KeyCode.Space) && !manager.paused && dashTimer <= 0 && moveBlockTimer <= 0) dash();
        if(Input.GetKeyDown(KeyCode.Q) && !manager.paused) switchWeapon();
        //timers
        moveBlockTimer -= Time.deltaTime;
        slowTimer -= Time.deltaTime;
        if(slowTimer > 0) moveSpeed = regularMoveSpeed*0.7f;
        else moveSpeed = regularMoveSpeed;
        
        if(moveBlockTimer <= 0){
            moveBlock = false;
            rb.velocity = Vector2.zero;
        }
        dashTimer -= Time.deltaTime; 

        FindObjectOfType<SoundManager>().PlayOnToggle("walkOnRock", isMoving());
    }

    void FixedUpdate()
    {
        Vector2 lookDir = mouse - rb.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg-90;
        facing = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
        //down = -112,5 - -67,5 / down-right -67,5 - -22,5 / right -22,5 - 22,5 / top right = 22,5 - 67,5 / top = 67,5 - 112,5 / top-left = 157,5 / left = < 157,5 | > -157,5 / down-left = -112,5 - -157,5
        if(facing >= -112.5f && facing < -67.5f) sR.sprite = sprites[0]; //down
        else if(facing >= -67.5f && facing < -22.5f) sR.sprite = sprites[1]; //donw-right
        else if(facing >= -22.5f && facing < 22.5f) sR.sprite = sprites[2]; //right
        else if(facing >= 22.5f && facing < 67.5f) sR.sprite = sprites[3]; //top-right
        else if(facing >= 67.5f && facing < 112.5f) sR.sprite = sprites[4]; //top
        else if(facing >= 112.5f && facing < 157.5f) sR.sprite = sprites[5]; //top-left
        else if(facing >= -157.5f && facing < -112.5f) sR.sprite = sprites[7]; //down-left
        else sR.sprite = sprites[6]; //left

        float tempMoveSpeed = moveSpeed;
        if((lookDir.x >= 0 && movement.x < 0) || (lookDir.y >= 0 && movement.y < 0 ) || (lookDir.x <= 0 && movement.x > 0) || (lookDir.y <= 0 && movement.y > 0)){
            //tempMoveSpeed = moveSpeed*0.5f;
            //this needs more refinement
        }
        if(!moveBlock){
            rb.MovePosition(rb.position+movement*tempMoveSpeed*Time.fixedDeltaTime);
        }
        gunHandler();
        gun.rotation = Quaternion.Euler(0,0,angle);
    }
    

    void dash(){
        if(movement != new Vector2(0,0)){
            dashTimer = 2.5f;
            moveBlockTimer = dashDuration;
            moveBlock = true;
            rb.velocity = movement*dashForce;
            FindObjectOfType<SoundManager>().Play("jumpOnRock");
        }
    }

    void gunHandler(){
        if(gunscript){
            gunHolder.rotation = Quaternion.Euler(0,0,facing);
            if(facing>90 || facing <-90){
                //gun to the left
                if(facing<0){
                    //gun in front
                    gunscript.changeSprite(0,3);
                }
                else gunscript.changeSprite(0,2);
            }
            else{
                if(facing<0){
                    //gun in front
                    gunscript.changeSprite(1,3);
                }
                else gunscript.changeSprite(1,2);
            }
        }
    }

    void switchWeapon(){
        uIController.spinanim();
        gunscript.disableSprite();
        weaponOneActive = !weaponOneActive;
        if(weaponOneActive) activeGun = guns[0];
        else activeGun = guns[1];
        gunscript = activeGun.GetComponent<Weapon>();
        FindObjectOfType<SoundManager>().Play("switchWeapon");
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "collectible"){
            other.GetComponent<Ressource>().collect(this.gameObject);
        }
        if(other.tag == "portal"){
            if(other.GetComponent<Portal>().isExit){
                manager.newMap();
            }else manager.presentExit();
        }
    }

    public void getStunned(float duration){
        moveBlockTimer = duration;
        moveBlock = true;
    }

    public void getSlowed(float duration){
        slowTimer = duration;
    }

    private bool isMoving()
    {
        return Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0;
    }
}
