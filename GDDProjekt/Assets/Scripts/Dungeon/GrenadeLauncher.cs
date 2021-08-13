using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrenadeLauncher : Weapon
{
    public float explosionRadius;

    override public void shoot(Vector3 pos, Vector3 dir, Quaternion rot, Vector3 target){
        //Cooldown Stuff
        if(!overheated && lastShotTimer>(1/fireRate)){
            lastShotTimer = 0f;
            overheat += overheatPerShot;

            GameObject bullet = Instantiate(bulletPrefab, pos, rot);
            Rigidbody2D rbBull = bullet.GetComponent<Rigidbody2D>();
            Granade rocketSkript = bullet.GetComponent<Granade>();
            
            rocketSkript.dmg = damage;
            rocketSkript.bulletforce = knockback;
            rocketSkript.exRadius = explosionRadius;
            rocketSkript.target = target;
            rbBull.AddForce(dir*force,ForceMode2D.Impulse);
            muzzleFire.enabled = true;
        }          
   }
}
