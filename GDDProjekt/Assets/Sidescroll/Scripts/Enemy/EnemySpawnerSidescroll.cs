using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnerSidescroll : MonoBehaviour
{
    //public variables
    public GameObject leftSideSpwaner;
    public GameObject rightSideSpawner;
    public GameObject leftSideSpwanerAbove;
    public GameObject rightSideSpawnerAbove;
    public GameObject normalEnemyPrefab;
    public GameObject flyEnemyPrefab;
    public GameObject bothEnemyPrefab;
    public int amountEnemyNormalPerSide;
    public int amountEnemyFlyPerSide;
    public int amountEnemyBothPerSide;
    public int waves;
    public int wavesCounter;
    public float startDelay;
    public float nextWaveDelay;
    public Transform cityHall;
    public Transform turretRight;
    public Transform turretLeft;

    //private variables
    float wavesTimer;
    float waitTime;
    AiManager aiManager;

    void Start()
    {
        waitTime = startDelay;
        aiManager = GetComponent<AiManager>();
        amountEnemyNormalPerSide = aiManager.gameManager.day + 3;
        amountEnemyFlyPerSide = Mathf.FloorToInt(aiManager.gameManager.day * 1f / 8f);
        amountEnemyBothPerSide = Mathf.FloorToInt(aiManager.gameManager.day * 1f / 4f);
        waves = aiManager.gameManager.day;
    }

    void FixedUpdate()
    {
        wavesTimer += Time.deltaTime;

        if (wavesTimer >= waitTime && wavesCounter < waves)
        {
            waitTime = nextWaveDelay;
            wavesCounter++;

            for (int i = 0; i < amountEnemyNormalPerSide; i++)
            {
                setValuesLeft(Instantiate(normalEnemyPrefab, leftSideSpwaner.transform.position, leftSideSpwaner.transform.rotation), false);
                setValuesRight(Instantiate(normalEnemyPrefab, rightSideSpawner.transform.position, rightSideSpawner.transform.rotation), false);
            }

            for (int i = 0; i < amountEnemyFlyPerSide; i++)
            {
                setValuesLeft(Instantiate(flyEnemyPrefab, leftSideSpwanerAbove.transform.position, leftSideSpwanerAbove.transform.rotation), true);
                setValuesRight(Instantiate(flyEnemyPrefab, rightSideSpawnerAbove.transform.position, rightSideSpawnerAbove.transform.rotation), true);
            }

            for (int i = 0; i < amountEnemyBothPerSide; i++)
            {
                setValuesLeft(Instantiate(bothEnemyPrefab, leftSideSpwaner.transform.position, leftSideSpwaner.transform.rotation), false);
                setValuesRight(Instantiate(bothEnemyPrefab, rightSideSpawner.transform.position, rightSideSpawner.transform.rotation), false);
            }
        }
    }

    void setValuesRight(GameObject gb, bool isAbove)
    {
        if (isAbove) aiManager.enemysAboveRight.Add(gb);
        else aiManager.enemysBelowRight.Add(gb);
        EnemyMovementSidescroll enemyMovementSidescroll = gb.GetComponent<EnemyMovementSidescroll>();
        EnemyAttackSidescroll enemyAttackSidescroll = gb.GetComponent<EnemyAttackSidescroll>();
        setValues(gb, enemyMovementSidescroll, enemyAttackSidescroll);
        enemyAttackSidescroll.npcs = aiManager.npcsRight;
        enemyMovementSidescroll.comeFromLeft = false;
        enemyMovementSidescroll.wallTarget = aiManager.wallRight.transform;
        enemyAttackSidescroll.wall = aiManager.wallRight;
    }

    void setValuesLeft(GameObject gb, bool isAbove)
    {
        if (isAbove) aiManager.enemysAboveLeft.Add(gb);
        else aiManager.enemysBelowLeft.Add(gb);
        EnemyMovementSidescroll enemyMovementSidescroll = gb.GetComponent<EnemyMovementSidescroll>();
        EnemyAttackSidescroll enemyAttackSidescroll = gb.GetComponent<EnemyAttackSidescroll>();
        setValues(gb, enemyMovementSidescroll, enemyAttackSidescroll);
        enemyAttackSidescroll.npcs = aiManager.npcsLeft;
        enemyMovementSidescroll.comeFromLeft = true;
        enemyMovementSidescroll.wallTarget = aiManager.wallLeft.transform;
        enemyAttackSidescroll.wall = aiManager.wallLeft;
    }

    void setValues(GameObject gb, EnemyMovementSidescroll enemyMovementSidescroll, EnemyAttackSidescroll enemyAttackSidescroll)
    {
        enemyAttackSidescroll.aiManager = aiManager;
        enemyMovementSidescroll.aiManager = aiManager;
        enemyMovementSidescroll.speed += Random.Range(1, 4);
        enemyMovementSidescroll.cityHall = cityHall;
    }
}
