using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
   
    [Tooltip("Sprites: 0: down, 1: down-right, 2: right, 3: top-right, 4: top, 5: top-left, 6: left, 7: down-left")]
    public Sprite[] sprites;
    public LayerMask noMove;
    public float moveSpeed = 5f;
    public float dashForce;
    public float dashDuration;
    public Camera cam;
    
    
    public Transform gun;
    public Transform gunHolder;
    public GameObject[] guns;
    [HideInInspector]
    public bool weaponOneActive = true;
    GameObject activeGun;


    float angle;
    float facing;
    bool moveBlock;
    float moveBlockTimer;

    public Weapon gunscript;
    Vector2 mouse;
    SpriteRenderer sR;
    Rigidbody2D rb;
    Vector2 movement;
    UIController uIController;

    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        activeGun = guns[0];
        gunscript = activeGun.GetComponent<Weapon>();
        gunscript.active = true;
        uIController = GameObject.FindGameObjectWithTag("manager").GetComponent<UIController>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetButton("Fire1")) activeGun.GetComponent<Weapon>().shoot(gun.position,gun.up, Quaternion.Euler(0,0,facing) , Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if(Input.GetKeyDown(KeyCode.Space)) dash();

        if(Input.GetKeyDown(KeyCode.Q)) switchWeapon();

        //timers
        moveBlockTimer -= Time.deltaTime;
        if(moveBlockTimer <= 0){
            moveBlock = false;
            rb.velocity = Vector2.zero;
        } 
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
    
    void rotationHandler(){
        
    }

    void dash(){
        if(movement != new Vector2(0,0)){
            moveBlockTimer = dashDuration;
            moveBlock = true;
            rb.velocity = movement*dashForce;
        }
    }

    void gunHandler(){
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

    void switchWeapon(){
        uIController.spinanim();
        gunscript.disableSprite();
        weaponOneActive = !weaponOneActive;
        if(weaponOneActive) activeGun = guns[0];
        else activeGun = guns[1];
        gunscript = activeGun.GetComponent<Weapon>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "collectible"){
            other.GetComponent<Ressource>().collect(this.gameObject);
        }
    }
}
