using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Tilemaps;

public class Manager : MonoBehaviour
{
    MapGenerator mapGenerator;
    AstarPath pathing;
    public GameObject hero;
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
    public TextMeshProUGUI lvlTxt;
    public GameObject lighting;

    void Start()
    {
        mapGenerator = GetComponent<MapGenerator>();
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
        string mapCode = maps[Random.Range(0,maps.Length)];
        string[] settings = mapCode.Split(',');
        monsterAmount += Mathf.RoundToInt(level*(4f/5f));
        if(monsterAmount >= 50) monsterAmount = 50;
        lighting.transform.position = new Vector3(Random.Range(-300,300), Random.Range(-300,300), 0);
        if(level == 10) floor.color = new Color(130f/255f, 184f/255f, 224f/255f,1);
        if(level == 20) floor.color = new Color(130f/255f, 224f/255f, 170f/255f,1);
        if(level == 30) floor.color = new Color(224f/255f, 130f/255f, 141f/255f,1);
        if(level == 40) floor.color = new Color(213f/255f, 130f/255f, 224f/255f,1);
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
        mapGenerator.spawnMap(settings);
        level++;
        lvlTxt.text = "Level: " + level;
        Invoke("Scan",0.5f);
    }

    void Scan(){
        pathing.Scan();
    }
}
