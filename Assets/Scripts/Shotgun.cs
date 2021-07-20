using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public int projectiles;

    override public void shoot(Vector3 pos, Vector3 oldDir, Quaternion rot, Vector3 target){
        if(!overheated && lastShotTimer>(1/fireRate)){
            float multi = 1;

            //create bullet
            for(int i = 0; i<projectiles; i++){
                Debug.Log(oldDir);
                Vector3 dir = new Vector3(oldDir.x+ oldDir.x*((float) Random.Range(2,15)/10 * multi), oldDir.y+oldDir.y*((float) Random.Range(2,15)/10*multi),0);
                //multi = -multi;
                Debug.Log(dir);
                GameObject bullet = Instantiate(bulletPrefab, pos, rot);
                Rigidbody2D rbBull = bullet.GetComponent<Rigidbody2D>();
                Bullet bScript = bullet.GetComponent<Bullet>();
                bScript.dmg = damage;
                bScript.bulletforce = dir*knockback;
                rbBull.AddForce(dir*force,ForceMode2D.Impulse);
            }
            //Cooldown Stuff
            lastShotTimer = 0f;
            overheat += overheatPerShot;
        }
    }
}
