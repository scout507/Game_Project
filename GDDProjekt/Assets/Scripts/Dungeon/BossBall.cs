using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBall : MonoBehaviour
{
    public float baseDamage;
    public float increase;
    public float speed;
    float initialDamage = 10f;
    public GameObject target;
    float deathTimer;
    bool playerContact;
    float damage;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        deathTimer += Time.deltaTime;
        if(deathTimer >= 5f) Destroy(this.gameObject);
        if(playerContact){
            target.GetComponent<PlayerStats>().takeDamage(damage*Time.deltaTime);
            target.GetComponent<PlayerController>().getSlowed(1f);
            damage += increase*Time.deltaTime;
        } 
    }

    void FixedUpdate()
    {
        Vector2 direction = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y-transform.position.y);
        rb.velocity = direction.normalized*speed*Time.fixedDeltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player"){
            playerContact = true;
            other.GetComponent<PlayerStats>().takeDamage(initialDamage);
        } 
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag == "Player") playerContact = false;
        damage = baseDamage;
    }

}
