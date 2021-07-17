using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterController : MonoBehaviour
{
    public float hp;
    public float dmg;
    public float moveSpeed;
    public float atkSpeed;
    public float aggroRange;
    public bool aggro;
    public GameObject player;
    public float distanceToEnemy;
    Pathfinding.AIPath pathing;
    public Transform dmgPopUp;
       

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        pathing = GetComponent<Pathfinding.AIPath>();
    }

    
    void Update()
    {
        distanceToEnemy = Vector3.Distance(player.transform.position, this.transform.position);
        if(distanceToEnemy < aggroRange) aggro =  true;
        if(aggro){
            pathing.canSearch = true;
        }
    }

    public void takeDamage(float dmgTaken){
        spawnDmgText(dmgTaken);
        hp -= dmgTaken;
        if(hp <= 0){
            die();
        }
    }

    void die(){
        Destroy(this.gameObject);
    }

    void spawnDmgText(float dmgTaken){
        Vector3 spawnPoint = new Vector3(transform.position.x, transform.position.y+1,0);
        Transform damagePopUpTrans = Instantiate(dmgPopUp, transform.position, Quaternion.identity);
        DmgPopUp dmgPopUpScript = damagePopUpTrans.GetComponent<DmgPopUp>();;
        dmgPopUpScript.Setup(dmgTaken);
    }

    
}
