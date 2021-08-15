using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shotgun : Weapon
{
    public int projectiles;
    public int centering;

    override public void shoot(Vector3 pos, Vector3 oldDir, Quaternion rot, Vector3 target){
        if(!overheated && lastShotTimer>(1/fireRate)){
            float multi = 1;

            //create bullet
            for(int i = 0; i<projectiles; i++){
                Vector3 dir;
                float offset;
                if(Random.Range(0,10)<centering) offset = (float) Random.Range(0,25)/1000;
                else offset = (float) Random.Range(0,250)/1000;
                if(oldDir.x+oldDir.y > 1 || oldDir.x+oldDir.y < -1){
                    dir = new Vector3(Mathf.Clamp((oldDir.x+multi*offset),-1,1),Mathf.Clamp((oldDir.y-multi*offset),-1,1),0);
                }
                else dir = new Vector3(Mathf.Clamp((oldDir.x+multi*offset),-1,1),Mathf.Clamp((oldDir.y+multi*offset),-1,1),0);
                multi = -multi;
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
            muzzleFire.enabled = true;
			FindObjectOfType<SoundManager>().Play("shootShotgun");
        }
    }
}
