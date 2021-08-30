using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : MonoBehaviour
{
    public float dmg;
    public float bulletforce;
    public Vector2 target;
    public float exRadius;
    public GameObject explo;
    bool exploded;   
    GameObject cam;

    private void Start()
    {
        cam = GameObject.FindGameObjectWithTag("MainCamera");
    } 

    private void FixedUpdate()
    {
        if(Vector2.Distance(transform.position, target) <= 0.5f) explode();
    }

    void explode(){
        if(!exploded){
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, exRadius);

            foreach (Collider2D obj in colliders)
            {
                if(obj.tag == "monster" && obj.isTrigger){
                    float distPercent = (exRadius/Vector2.Distance(this.transform.position,obj.transform.position))/exRadius;
                    if(distPercent > 0.25f) obj.GetComponent<MonsterController>().takeDamage(dmg, (obj.transform.position-transform.position)*bulletforce);
                    else if(distPercent > 0.125f) obj.GetComponent<MonsterController>().takeDamage(dmg*0.5f, (obj.transform.position-transform.position)*bulletforce);
                    else obj.GetComponent<MonsterController>().takeDamage(dmg*0.25f, (obj.transform.position-transform.position)*bulletforce);
                }
                else if(obj.tag == "destructable"){
                    obj.GetComponent<DestructableProp>().die();
                }
                else if(obj.tag == "boss"){
                    float distPercent = (exRadius/Vector2.Distance(this.transform.position,obj.transform.position))/exRadius;
                    if(distPercent > 0.25f) obj.GetComponent<BossController>().takeDamage(dmg);
                    else if(distPercent > 0.125f) obj.GetComponent<BossController>().takeDamage(dmg*0.5f);
                    else obj.GetComponent<BossController>().takeDamage(dmg*0.25f);
                }
            }
            explo = Instantiate(explo, transform.position, Quaternion.identity);
            Invoke("destroy", 0.5f);
            exploded = true;
            FindObjectOfType<SoundManager>().Play("explosion");
            GetComponent<SpriteRenderer>().enabled = false;
            cam.GetComponent<CamController>().shake();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag != "Player" && other.tag != "allowBullets" && other.tag != "collectible" && other.tag != "portal"){
            explode();
        }
    }

    void destroy(){
        Destroy(explo);
        Destroy(this.gameObject);
    }
}
