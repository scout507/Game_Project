using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon: MonoBehaviour 
{
    public string weaponName;
    public Sprite icon;
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
    }

    private void Update()
    {
        lastShotTimer += Time.deltaTime;
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

    public void changeSprite(int direction, int layer){
        if(active){
            sr.enabled = true;
            sr.sprite = sprites[direction];
            sr.sortingOrder = layer;
        }
    }

    abstract public void shoot(Vector3 pos, Vector3 dir, Quaternion rot, Vector3 target);
}
