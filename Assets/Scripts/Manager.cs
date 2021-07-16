using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    MapGenerator mapGenerator;
    AstarPath pathing;
    public GameObject hero;
    int level = 0;

    void Start()
    {
        mapGenerator = GetComponent<MapGenerator>();
        newMap();
    }

    // Update is called once per frame
    void Update()
    {
        pathing = GetComponent<AstarPath>();
        if(Vector3.Distance(hero.transform.position, mapGenerator.end) < 1f){
            newMap();
        }
        
    }

    void newMap(){
        mapGenerator.spawnMap();
        level++;
        Invoke("Scan",0.5f);
    }

    void Scan(){
        pathing.Scan();
    }
}
