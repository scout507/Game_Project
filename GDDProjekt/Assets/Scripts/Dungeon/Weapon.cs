using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public abstract class Weapon: MonoBehaviour 
{
    public string weaponName;
    public Sprite icon;
    public Sprite iconActive;
    public GameObject bulletPrefab;
    public float damage;
    public float fireRate;
    public float overheat;
    public float overheatPerShot;
    public float overheatLossRate;
    public float force;
    public float knockback;
    private float timer;
    private SpriteRenderer sr;
    public Light2D muzzleFire;
    private float muzzleFireTime = 0.04f;

    [Tooltip("Sprites: 0: right, 1: left")]
    public Sprite[] sprites;

    [HideInInspector]
    public float lastShotTimer;
    public bool overheated;
    public float coolDown;
    public bool active = false;
    
    private void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        if(!active) sr.enabled = false;
        muzzleFire = GetComponentInChildren<Light2D>();
    }

    private void Update()
    {
        lastShotTimer += Time.deltaTime;
        if(lastShotTimer >= muzzleFireTime) muzzleFire.enabled = false;
        if(overheat >= 10f){
            overheated = true;
            timer = 10/overheatLossRate;
        } 
        if(lastShotTimer >= 1/fireRate && overheat > 0) overheat -= overheatLossRate * Time.deltaTime;
        if(overheated){
            timer -= Time.deltaTime;
            if(timer <= 0) overheated = false;
        }
    }

    void FixedUpdate(){
        
    }

    public void changeSprite(int direction, int layer){
        sr = GetComponent<SpriteRenderer>();
        sr.enabled = true;
        sr.sprite = sprites[direction];
        sr.sortingOrder = layer;
    }

    public void disableSprite(){
        sr.enabled = false;
    }

    abstract public void shoot(Vector3 pos, Vector3 dir, Quaternion rot, Vector3 target);
}
