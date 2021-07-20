using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Sprite[] sprites;
    Vector2 movement;
    public float moveSpeed = 5f;
    Rigidbody2D rb;
    public Camera cam;
    Vector2 mouse;
    public Transform gun;
    public GameObject activeGun;
    public Weapon gunscript;
    public float bulletForce;
    float angle;
    float facing;
    SpriteRenderer sR;
    public float dashForce;
    public float dashDuration;
    bool dashing;
    float dashTimer;
    public LayerMask noMove;

    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        gunscript = activeGun.GetComponent<Weapon>();
    }

    // Update is called once per frame
    void Update()
    {
        //Input
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetButton("Fire1")) activeGun.GetComponent<Weapon>().shoot(gun.position,gun.up,gun.rotation, Camera.main.ScreenToWorldPoint(Input.mousePosition));
        if(Input.GetKeyDown(KeyCode.Space)) dash();
    
        //timers
        dashTimer -= Time.deltaTime;
        if(dashTimer <= 0){
            dashing = false;
            rb.velocity = Vector2.zero;
        } 
    }

    void FixedUpdate()
    {
        if(!dashing){
            rb.MovePosition(rb.position+movement*moveSpeed*Time.fixedDeltaTime);
        }

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

        gun.rotation = Quaternion.Euler(0,0,angle);
    }
    /*
    void shoot(){

        GameObject bullet = Instantiate(bulletPrefab, gun.position, gun.rotation);
        Rigidbody2D rbBull = bullet.GetComponent<Rigidbody2D>();
        Bullet bScript = bullet.GetComponent<Bullet>();
        bScript.dmg = 10f;
        bScript.bulletforce = gun.up*bulletForce*0.05f;
        rbBull.AddForce(gun.up*bulletForce,ForceMode2D.Impulse);
    }

    */

    void dash(){
        if(movement != new Vector2(0,0)){
            dashTimer = dashDuration;
            dashing = true;
            rb.velocity = movement*dashForce;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        //Debug.Log(other.gameObject);
    }
}
