using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Weaponstats : ScriptableObject
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

    //shotgun
    public int projectiles;
    public int centering;

    //GrenadeLauncher
    public float explosionRadius;
}
