using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Manager : MonoBehaviour
{
    public GameObject bossarena;
    GameObject hero;
    public GameObject cam;
    public int level = 0;
    public int monsterAmount = 10;
    public GameObject[] monsters;
    public List<GameObject> monstersInLevel;
    public List<GameObject> props; 
    public List<GameObject> loot;

    string[] maps = {"15,2,2,2","15,2,2,3","15,2,2,4","15,2,2,5","15,2,1,3","6,1,2,3", "6,2,1,5", "6,2,1,10"};

    
    public Tilemap wall;
    public Tilemap floor;
    public Tilemap innerObs;

    //UI
    
    public GameObject lighting;
    MapGenerator mapGenerator;
    AstarPath pathing;

    void Start()
    {
        mapGenerator = GetComponent<MapGenerator>();
        hero = GameObject.FindGameObjectWithTag("Player");
        newMap();
    }

    void Update()
    {
        pathing = GetComponent<AstarPath>();
        if(Vector3.Distance(hero.transform.position, mapGenerator.end) < 1f){
            newMap();
        }   
    }

    void newMap(){
        level++;
        monstersInLevel.ForEach( monster =>{
            Destroy(monster);
        });
        props.ForEach(prop =>{
            Destroy(prop);
        });
        loot.ForEach(loot =>{
            Destroy(loot);
        });
        monstersInLevel.Clear();
        props.Clear();
        

        if(level % 10 != 0){
            string mapCode = maps[Random.Range(0,maps.Length)];
            string[] settings = mapCode.Split(',');
            monsterAmount += Mathf.RoundToInt(level*(4f/5f));
            if(monsterAmount >= 50) monsterAmount = 50;
            lighting.transform.position = new Vector3(Random.Range(-300,300), Random.Range(-300,300), 0);
            if(level == 10) floor.color = new Color(130f/255f, 184f/255f, 224f/255f,1);
            if(level == 20) floor.color = new Color(130f/255f, 224f/255f, 170f/255f,1);
            if(level == 30) floor.color = new Color(224f/255f, 130f/255f, 141f/255f,1);
            if(level == 40) floor.color = new Color(213f/255f, 130f/255f, 224f/255f,1);
            mapGenerator.spawnMap(settings); 
            Invoke("Scan",0.5f);
        }
        else{
            
            hero.transform.position = bossarena.transform.position;
        }
        cam.transform.position = new Vector3 (hero.transform.position.x, hero.transform.position.y, cam.transform.position.z);
    }

    void Scan(){
        pathing.Scan();
    }
}
