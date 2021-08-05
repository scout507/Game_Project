using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Manager : MonoBehaviour
{
    MapGenerator mapGenerator;
    AstarPath pathing;
    public GameObject hero;
    int level = 0;
    public int monsterAmount = 10;
    public GameObject[] monsters;
    public List<GameObject> monstersInLevel;
    //UI
    public TextMeshProUGUI lvlTxt;

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
        monstersInLevel.ForEach( monster =>{
            Destroy(monster);
        });
        monstersInLevel.Clear();
        mapGenerator.spawnMap();
        level++;
        lvlTxt.text = "Level: " + level;
        Invoke("Scan",0.5f);
    }

    void Scan(){
        pathing.Scan();
    }
}
