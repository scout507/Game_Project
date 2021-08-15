using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressource : MonoBehaviour
{
    public string displayName;
    public int maxAmount;
    public int type;
    int amount;
    public Sprite image;
    
    GameObject player;
    bool active;

    SpriteRenderer sr;
    Rigidbody2D rb;
    Vector3 direction;
    GameObject manager;
    PlayerStats playerStats;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = image;
        amount = Random.Range(1,maxAmount);
        rb = GetComponent<Rigidbody2D>();
        manager = GameObject.FindGameObjectWithTag("manager");
        manager.GetComponent<Manager>().loot.Add(this.gameObject);
        playerStats = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerStats>();
    }


    
    void FixedUpdate()
    {
        if(active){
            direction = player.transform.position - this.transform.position;
            rb.velocity = direction*3f;
            if(Vector3.Distance(player.transform.position, this.transform.position)< 2.5f) getCollected();
        }
    }

    public void collect(GameObject target){
        active = true;
        player = target;
    }

    void getCollected(){
        playerStats.loot[type] += amount;
        Destroy(this.gameObject);
        FindObjectOfType<SoundManager>().Play("itemPickup");
    }

}
