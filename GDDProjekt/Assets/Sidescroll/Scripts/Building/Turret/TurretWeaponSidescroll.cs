using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretWeaponSidescroll : MonoBehaviour
{
    //public variables    
    public GameObject bulletPrefab;
    public Transform firepoint;
    public Camera cam;
    public bool entered = false;
    public bool isRightFacing = false;
    public float speed = 10f;
    public float cooldown = 0.3f;
    public GameManager gameManager;
    public int damage;
    public AiManager aiManager;

    //private variables
    Vector3 mouse;
    Vector2 dirct;
    float facing;
    float angle;
    float timer = 0;

    void Start()
    {
        damage = Mathf.CeilToInt((float)damage * Mathf.Pow(1.1f, gameManager.turret));
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (entered && !aiManager.gameEndEnemy)
        {
            mouse = cam.ScreenToWorldPoint(Input.mousePosition);
            dirct = mouse - firepoint.position;
            facing = Mathf.Atan2(dirct.y, dirct.x) * Mathf.Rad2Deg;

            if (!isRightFacing) facing -= 180f;

            if (!isRightFacing && facing >= -60) transform.rotation = Quaternion.Euler(0, 0, facing);
            if (isRightFacing && facing <= 60 && facing >= 0) transform.rotation = Quaternion.Euler(0, 0, facing);

            if (Input.GetButtonDown("Fire1") && timer >= cooldown) shoot();
        }
        else transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void shoot()
    {
        timer = 0;

        angle = Mathf.Atan2(dirct.y, dirct.x) * Mathf.Rad2Deg - 90;
        firepoint.rotation = Quaternion.Euler(0, 0, angle);
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        bullet.GetComponent<BulletTurretSidescroll>().damage = damage;
        bullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * speed, ForceMode2D.Impulse);
    }
}
