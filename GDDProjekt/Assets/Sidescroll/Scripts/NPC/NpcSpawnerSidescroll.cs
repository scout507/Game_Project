using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NpcSpawnerSidescroll : MonoBehaviour
{
    //public variables
    public GameObject normalNpcPrefab;
    public GameObject sniperNpcPrefab;
    public GameObject mortarNpcPrefab;

    public int amountNormalNpcPrefabPerSide;
    public int amountSniperNpcPrefabPerSide;
    public int amountMortarNpcPrefabPerSide;

    public GameObject spawner;
    public int waves;
    public float startDelay;
    public float nextWaveDelay;

    public Transform cityHall;
    public Transform positionSniperLeft;
    public Transform positionMortarLeft;
    public Transform positionSniperRight;
    public Transform positionMortarRight;
    
    //private variables
    float wavesTimer;
    float waitTime;
    int wavesCounter;
    AiManager aiManager;

    void Start()
    {
        waitTime = startDelay;
        aiManager = GetComponent<AiManager>();
        normalNpcPrefab.GetComponent<NpcAttackSidescroll>().damage = aiManager.gameManager.normalNpcLevel;
        normalNpcPrefab.GetComponent<NpcAttackSidescroll>().maxLife = aiManager.gameManager.normalNpcLevel;
        sniperNpcPrefab.GetComponent<NpcAttackSidescroll>().damage = aiManager.gameManager.sniperNpcLevel;
        sniperNpcPrefab.GetComponent<NpcAttackSidescroll>().maxLife = aiManager.gameManager.sniperNpcLevel;
        mortarNpcPrefab.GetComponent<NpcAttackSidescroll>().damage = aiManager.gameManager.mortarNpcLevel;
        mortarNpcPrefab.GetComponent<NpcAttackSidescroll>().maxLife = aiManager.gameManager.mortarNpcLevel;
    }

    void Update()
    {
        wavesTimer += Time.deltaTime;

        if (wavesTimer >= waitTime && wavesCounter < waves)
        {
            waitTime = nextWaveDelay;
            wavesCounter++;

            for (int i = 0; i < amountNormalNpcPrefabPerSide; i++)
            {
                setValuesLeft(Instantiate(normalNpcPrefab, spawner.transform.position, spawner.transform.rotation), aiManager.wallLeft.transform);
                setValuesRight(Instantiate(normalNpcPrefab, spawner.transform.position, spawner.transform.rotation), aiManager.wallRight.transform);
            }

            for (int i = 0; i < amountMortarNpcPrefabPerSide; i++)
            {
                setValuesLeft(Instantiate(mortarNpcPrefab, spawner.transform.position, spawner.transform.rotation), positionMortarLeft);
                setValuesRight(Instantiate(mortarNpcPrefab, spawner.transform.position, spawner.transform.rotation), positionMortarRight);
            }

            for (int i = 0; i < amountSniperNpcPrefabPerSide; i++)
            {
                setValuesLeft(Instantiate(sniperNpcPrefab, spawner.transform.position, spawner.transform.rotation), positionSniperLeft);
                setValuesRight(Instantiate(sniperNpcPrefab, spawner.transform.position, spawner.transform.rotation), positionSniperRight);
            }
        }
    }

    void setValuesRight(GameObject gb, Transform position)
    {
        aiManager.npcsRight.Add(gb);
        NpcMovementSidescroll npcMovementSidescroll = gb.GetComponent<NpcMovementSidescroll>();
        NpcAttackSidescroll npcAttackSidescroll = gb.GetComponent<NpcAttackSidescroll>();
        npcAttackSidescroll.enemysAbove = aiManager.enemysAboveRight;
        npcAttackSidescroll.enemysBelow = aiManager.enemysBelowRight;
        npcAttackSidescroll.aiManager = aiManager;
        npcMovementSidescroll.targetToMove = position;
        npcMovementSidescroll.speed += Random.Range(1, 4);
        npcMovementSidescroll.cityHall = cityHall;
        npcMovementSidescroll.aiManager = aiManager;
    }

    void setValuesLeft(GameObject gb, Transform position)
    {
        aiManager.npcsLeft.Add(gb);
        NpcMovementSidescroll npcMovementSidescroll = gb.GetComponent<NpcMovementSidescroll>();
        NpcAttackSidescroll npcAttackSidescroll = gb.GetComponent<NpcAttackSidescroll>();
        npcAttackSidescroll.enemysAbove = aiManager.enemysAboveLeft;
        npcAttackSidescroll.enemysBelow = aiManager.enemysBelowLeft;
        npcAttackSidescroll.aiManager = aiManager;
        npcMovementSidescroll.targetToMove = position;
        npcMovementSidescroll.speed += Random.Range(1, 4);
        npcMovementSidescroll.cityHall = cityHall;
        npcMovementSidescroll.aiManager = aiManager;
    }
}
