using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rifle : Weapon
{
    override public void shoot(Vector3 pos, Vector3 dir, Quaternion rot, Vector3 target){
        if(!overheated && lastShotTimer>(1/fireRate)){
            //create bullet
            GameObject bullet = Instantiate(bulletPrefab, pos, rot);
            Rigidbody2D rbBull = bullet.GetComponent<Rigidbody2D>();
            Bullet bScript = bullet.GetComponent<Bullet>();
            bScript.dmg = damage;
            bScript.bulletforce = dir*knockback;
            rbBull.AddForce(dir*force,ForceMode2D.Impulse);
            
            //Cooldown Stuff
            lastShotTimer = 0f;
            overheat += overheatPerShot;
        }
    }
    
    
}
