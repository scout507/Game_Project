using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ressource : MonoBehaviour
{
    public string displayName;
    public int maxAmount;
    int amount;
    public Sprite image;
    
    GameObject player;
    bool active;

    SpriteRenderer sr;
    Rigidbody2D rb;
    Vector3 direction;
    Manager manager;

    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.sprite = image;
        amount = Random.Range(1,maxAmount);
        rb = GetComponent<Rigidbody2D>();
        manager = GameObject.FindGameObjectWithTag("manager").GetComponent<Manager>();
        manager.loot.Add(this.gameObject);
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
        Destroy(this.gameObject);
    }

}
