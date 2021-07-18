using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float dmg;
    public Vector3 bulletforce;
    // Start is called before the first frame update
    void Start()
    {
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Player" && other.tag != "allowBullets"){
            Destroy(this.gameObject);
            if(other.tag == "monster"){
                other.GetComponent<MonsterController>().takeDamage(dmg, bulletforce);
            }
        }
    }
}
