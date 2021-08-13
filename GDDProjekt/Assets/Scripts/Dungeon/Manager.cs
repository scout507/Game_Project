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
    public bool paused;

    public GameObject[] guns;

    string[] maps = {"15,2,2,2","15,2,2,3","15,2,2,4","15,2,2,5","15,2,1,3","6,1,2,3", "6,2,1,5", "6,2,1,10"};

    
    public Tilemap wall;
    public Tilemap floor;
    public Tilemap innerObs;

    //UI
    
    public GameObject lighting;
    MapGenerator mapGenerator;
    AstarPath pathing;
    GameManager gameManager;
    PlayerStats playerStats;
    PlayerController playerController;
    UIController uIController;
    

    void Start()
    {
        mapGenerator = GetComponent<MapGenerator>();
        hero = GameObject.FindGameObjectWithTag("Player");
        playerStats = hero.GetComponent<PlayerStats>();
        playerController = hero.GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        uIController = GetComponent<UIController>();
        loadFromManager();
        newMap();
    }

    void Update()
    {
        pathing = GetComponent<AstarPath>();
    }

    public void newMap(){
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
            monsterAmount = 10 + Mathf.RoundToInt(level*(4f/5f));
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


    public void presentExit(){
        pauseGame();
        uIController.exitMenu();
    }

    void pauseGame(){
        Time.timeScale = 0;
    }

    public void resumeGame(){
        Time.timeScale = 1;
    }

    void saveToManager(){
        for(int i = 0; i<playerStats.loot.Length; i++){
            gameManager.resources[i] = playerStats.loot[i];
        }
    }

    void loadFromManager(){
        GameObject gunHolder = GameObject.FindGameObjectWithTag("gunHolder");
        GameObject gun;
        bool activeSelected = false;
        Vector3 spawnPoint = new Vector3(gunHolder.transform.position.x+0.5f, gunHolder.transform.position.y, gunHolder.transform.position.z);
        //Set up Weapons
        
        for(int i = 0; i < 2; i++){
            Weapon weaponScript;
            Weaponstats copyStats;
            if(gameManager.selectedWeapons[i] == 0){
                //shotgun
                
                copyStats = gameManager.shotgun;
                gun = Instantiate(guns[0], spawnPoint, Quaternion.identity);
                gun.transform.parent = gunHolder.transform;
                weaponScript = gun.GetComponent<Weapon>();
                Shotgun shotgun = gun.GetComponent<Shotgun>();
                shotgun.projectiles = copyStats.projectiles;
                shotgun.centering = copyStats.centering;
                playerController.guns[i] = gun;
            }
            else if(gameManager.selectedWeapons[i] == 1){
                //Rifle
                
                copyStats = gameManager.rifle;
                gun = Instantiate(guns[1], spawnPoint, Quaternion.identity);
                gun.transform.parent = gunHolder.transform;
                weaponScript = gun.GetComponent<Weapon>();
                playerController.guns[i] = gun;
            }
            else{
                //grenade
                
                copyStats = gameManager.grenadeLauncher;
                gun = Instantiate(guns[2], spawnPoint, Quaternion.identity);
                gun.transform.parent = gunHolder.transform;
                weaponScript = gun.GetComponent<Weapon>();
                GrenadeLauncher grenade =  gun.GetComponent<GrenadeLauncher>();
                grenade.explosionRadius = copyStats.explosionRadius;

                playerController.guns[i] = gun;
                
            }
            if(!activeSelected){
                playerController.activeGun = gun;
                playerController.gunscript = gun.GetComponent<Weapon>();
                activeSelected = true;
            } 
            weaponScript.damage = copyStats.damage;
            weaponScript.fireRate = copyStats.fireRate;
            weaponScript.overheatPerShot = copyStats.overheatPerShot;
            weaponScript.overheatLossRate = copyStats.overheatLossRate;
        }
        

    }
}
