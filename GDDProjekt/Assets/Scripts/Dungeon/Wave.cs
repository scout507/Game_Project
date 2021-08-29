using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wave : MonoBehaviour
{
    public float direction;
    public float expansionSpeed;
    public float damage;
    public float moveSpeed;
    public Vector3 target;
    float timer;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        Vector3 oldScale = new Vector3(1,1,1);
        this.transform.localScale += new Vector3(oldScale.x*expansionSpeed*Time.deltaTime,oldScale.y*expansionSpeed*Time.deltaTime,0); 

        if(timer >= 1.5f) Destroy(this.gameObject);
    }

    void FixedUpdate()
    {
        Vector3 dir = new Vector3(target.x - transform.position.x, target.y - transform.position.y, 0f);
        this.transform.rotation = Quaternion.Euler(dir.x,dir.y,0);
        Vector2 direction = new Vector2(target.x - transform.position.x, target.y-transform.position.y);
        rb.velocity = direction.normalized*moveSpeed*Time.fixedDeltaTime;
    }
}
