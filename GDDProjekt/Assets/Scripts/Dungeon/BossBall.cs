using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBall : MonoBehaviour
{
    public float baseDamage;
    public float increase;
    public float expansionSpeed;
    float initialDamage = 10f;
    public GameObject target;
    float deathTimer;
    bool playerContact;
    float damage;

    // Start is called before the first frame update
    void Start()
    {
        target = GameObject.FindGameObjectWithTag("Player");
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
        Vector3 oldScale = new Vector3(1,1,1);
        this.transform.localScale += new Vector3(oldScale.x*expansionSpeed*Time.deltaTime,oldScale.y*expansionSpeed*Time.deltaTime,0); 
    }

    void FixedUpdate()
    {
        //Vector2 direction = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y-transform.position.y);
        //rb.velocity = direction.normalized*speed*Time.fixedDeltaTime;
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
