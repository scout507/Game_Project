using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSidescroll : MonoBehaviour
{
    //public variables
    public Transform firepoint;
    public GameObject bulletPrefab;
    public Camera cam;
    public bool entered = false;
    public float speed = 10f;
    public float cooldown = 0.5f;

    //private variables
    Vector3 mouse;
    Vector2 dirct;
    float timer = 0;
    float angle;
    
    void Update()
    {
        timer += Time.deltaTime;

        mouse = cam.ScreenToWorldPoint(Input.mousePosition);
        dirct = mouse - firepoint.position;
        angle = Mathf.Atan2(dirct.y, dirct.x) * Mathf.Rad2Deg - 90;
        firepoint.rotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetButtonDown("Fire1") && !GetComponent<PlayerMovementSidescroll>().inEvent && timer >= cooldown) shoot();

    }

    void shoot()
    {
        timer = 0;
        
        if ((transform.position.x - firepoint.position.x < 0 && dirct.x > 0) || (transform.position.x - firepoint.position.x > 0 && dirct.x < 0))
        {
            GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
            bullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * speed, ForceMode2D.Impulse);
        }
    }
}
