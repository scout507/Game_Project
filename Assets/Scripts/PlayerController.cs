using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Sprite[] sprites;
    Vector2 movement;
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Camera cam;
    Vector2 mouse;
    public Transform gun;
    public GameObject bulletPrefab;
    public float bulletForce;
    float angle;
    SpriteRenderer sR;

    void Start()
    {
        sR = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        if(Input.GetButtonDown("Fire1")) Shoot();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position+movement*moveSpeed*Time.fixedDeltaTime);
        Vector2 lookDir = mouse - rb.position;
        angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg-90;
        Debug.Log(angle);
        if(angle <= -130 && angle > -240) sR.sprite = sprites[0];
        else if(angle <= -40 && angle > -130) sR.sprite = sprites[1];
        else if(angle <= 50 && angle > -40) sR.sprite = sprites[2]; // looking top
        else if(angle <= -240 || angle > 50) sR.sprite = sprites[2];
        gun.rotation = Quaternion.Euler(0,0,angle);
    }

    void Shoot(){
        GameObject bullet = Instantiate(bulletPrefab, gun.position, gun.rotation);
        Rigidbody2D rbBull = bullet.GetComponent<Rigidbody2D>();
        rbBull.AddForce(gun.up*bulletForce,ForceMode2D.Impulse);
    }
}
