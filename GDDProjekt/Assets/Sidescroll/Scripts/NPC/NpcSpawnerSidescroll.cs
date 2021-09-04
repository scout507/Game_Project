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

    public GameObject spawnerRight;
    public GameObject spawnerLeft;
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
                setValuesLeft(Instantiate(normalNpcPrefab, spawnerLeft.transform.position, spawnerLeft.transform.rotation), aiManager.wallLeft.transform, aiManager.gameManager.normalNpcLevel);
                setValuesRight(Instantiate(normalNpcPrefab, spawnerRight.transform.position, spawnerRight.transform.rotation), aiManager.wallRight.transform, aiManager.gameManager.normalNpcLevel);
            }

            for (int i = 0; i < amountMortarNpcPrefabPerSide; i++)
            {
                setValuesLeft(Instantiate(mortarNpcPrefab, spawnerLeft.transform.position, spawnerLeft.transform.rotation), positionMortarLeft, aiManager.gameManager.mortarNpcLevel);
                setValuesRight(Instantiate(mortarNpcPrefab, spawnerRight.transform.position, spawnerRight.transform.rotation), positionMortarRight, aiManager.gameManager.mortarNpcLevel);
            }

            for (int i = 0; i < amountSniperNpcPrefabPerSide; i++)
            {
                setValuesLeft(Instantiate(sniperNpcPrefab, spawnerLeft.transform.position, spawnerLeft.transform.rotation), positionSniperLeft, aiManager.gameManager.sniperNpcLevel);
                setValuesRight(Instantiate(sniperNpcPrefab, spawnerRight.transform.position, spawnerRight.transform.rotation), positionSniperRight, aiManager.gameManager.sniperNpcLevel);
            }
        }
    }

    void setValuesRight(GameObject gb, Transform position, int level)
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
        npcMovementSidescroll.toLeft = false;
        npcAttackSidescroll.damage = (int)(npcAttackSidescroll.damage * Mathf.Pow(1.1f, level));
        npcAttackSidescroll.maxLife = (int)(npcAttackSidescroll.maxLife * Mathf.Pow(1.1f, level));
    }

    void setValuesLeft(GameObject gb, Transform position, int level)
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
        npcMovementSidescroll.toLeft = true;
        npcAttackSidescroll.damage = (int)(npcAttackSidescroll.damage * Mathf.Pow(1.1f, level));
        npcAttackSidescroll.maxLife = (int)(npcAttackSidescroll.maxLife * Mathf.Pow(1.1f, level));
    }
}
