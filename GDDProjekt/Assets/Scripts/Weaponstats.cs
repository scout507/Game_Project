using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Weaponstats
{
    public void init(float damage, float fireRate, float overheatPerShot, float overheatLossRate, int projectiles, int centering, float explosionRadius){

        this.damage = damage;
        this.fireRate = fireRate;
        this.overheatPerShot = overheatPerShot;
        this.overheatLossRate = overheatLossRate;
        this.projectiles = projectiles;
        this.centering = centering;
        this.explosionRadius = explosionRadius;
    }

    public float damage;
    public float fireRate;
    public float overheatPerShot;
    public float overheatLossRate;

    public int fireRateLevel = 0;
    public int damageLevel = 0;
    public int cooldownLevel = 0;
    public int explosionRadiusLevel = 0;

    //shotgun
    public int projectiles;
    public int centering;

    //GrenadeLauncher
    public float explosionRadius;
}
