using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBall : MonoBehaviour
{
    public float damage;
    public float speed;
    public GameObject target;
    float deathTimer;
    bool playerContact;

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
    }

    void FixedUpdate()
    {
        Vector2 direction = new Vector2(target.transform.position.x - transform.position.x, target.transform.position.y-transform.position.y);
        rb.velocity = direction.normalized*speed*Time.fixedDeltaTime;
    }
}
