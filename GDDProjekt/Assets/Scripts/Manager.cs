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
    public List<GameObject> props; 
    //UI
    public TextMeshProUGUI lvlTxt;
    public GameObject lighting;

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
        lighting.transform.position = new Vector3(Random.Range(-300,300), Random.Range(-300,300), 0);

        monstersInLevel.ForEach( monster =>{
            Destroy(monster);
        });
        props.ForEach(prop =>{
            Destroy(prop);
        });
        monstersInLevel.Clear();
        props.Clear();
        mapGenerator.spawnMap();
        level++;
        lvlTxt.text = "Level: " + level;
        Invoke("Scan",0.5f);
    }

    void Scan(){
        pathing.Scan();
    }
}
