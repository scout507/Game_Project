using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiManager : MonoBehaviour
{
    public List<GameObject> npcsLeft;
    public List<GameObject> enemysAboveLeft;
    public List<GameObject> enemysBelowLeft;
    public List<GameObject> npcsRight;
    public List<GameObject> enemysAboveRight;
    public List<GameObject> enemysBelowRight;
    public bool gameEndEnemy = false;
    public bool gameEndNpc = false;
    public GameObject wallLeft;
    public GameObject wallRight;
    public GameObject cityHall;
    public bool stopNpcs = false;
    public GameManager gameManager;

    void Awake(){
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }
}
