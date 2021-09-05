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
    GameManager gameManager;
    public int damage;
    public AiManager aiManager;
    public Transform pipe;

    //private variables
    Vector3 mouse;
    Vector2 dirct;
    float facing;
    float angle;
    float timer = 0;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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

            if (!isRightFacing && facing >= -60) pipe.rotation = Quaternion.Euler(0, 0, facing);
            if (isRightFacing && facing <= 60 && facing >= 0) pipe.rotation = Quaternion.Euler(0, 0, facing);

            if (Input.GetButton("Fire1") && timer >= cooldown) shoot();
        }
        else pipe.rotation = Quaternion.Euler(0, 0, 0);
    }

    void shoot()
    {
        timer = 0;
        FindObjectOfType<SoundManager>().Play("turret_shoot");
        angle = Mathf.Atan2(dirct.y, dirct.x) * Mathf.Rad2Deg - 90;
        float angle2 = Mathf.Atan2(dirct.y, dirct.x) * Mathf.Rad2Deg;
        firepoint.rotation = Quaternion.Euler(0, 0, angle);
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, Quaternion.Euler(0, 0, angle2));
        bullet.GetComponent<BulletTurretSidescroll>().damage = damage;
        bullet.GetComponent<Rigidbody2D>().AddForce(firepoint.up * speed, ForceMode2D.Impulse);
    }
}
