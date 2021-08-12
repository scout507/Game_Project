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
    public float startDelay;
    public float nextWaveDelay;
    public Transform cityHall;
    public Transform turretRight;
    public Transform turretLeft;
    public Transform wallRight;
    public Transform wallLeft;
    public List<Transform> npcsLeft;
    public List<Transform> npcsRight;

    //private variables
    float wavesTimer;
    float waitTime;
    int wavesCounter;

    void Start()
    {
        waitTime = startDelay;
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
                setValuesLeft(Instantiate(normalEnemyPrefab, leftSideSpwaner.transform.position, leftSideSpwaner.transform.rotation));
                setValuesRight(Instantiate(normalEnemyPrefab, rightSideSpawner.transform.position, rightSideSpawner.transform.rotation));
            }

            for (int i = 0; i < amountEnemyFlyPerSide; i++)
            {
                setValuesLeft(Instantiate(flyEnemyPrefab, leftSideSpwanerAbove.transform.position, leftSideSpwanerAbove.transform.rotation));
                setValuesRight(Instantiate(flyEnemyPrefab, rightSideSpawnerAbove.transform.position, rightSideSpawnerAbove.transform.rotation));
            }

            for (int i = 0; i < amountEnemyBothPerSide; i++)
            {
                setValuesLeft(Instantiate(bothEnemyPrefab, leftSideSpwaner.transform.position, leftSideSpwaner.transform.rotation));
                setValuesRight(Instantiate(bothEnemyPrefab, rightSideSpawner.transform.position, rightSideSpawner.transform.rotation));
            }
        }
    }

    void setValuesRight(GameObject gb)
    {
        EnemyMovementSidescroll enemyMovementSidescroll = gb.GetComponent<EnemyMovementSidescroll>();
        EnemyAttackSidescroll enemyAttackSidescroll = gb.GetComponent<EnemyAttackSidescroll>();
        enemyMovementSidescroll.cityHall = cityHall;
        enemyAttackSidescroll.wall = wallRight.gameObject;
        enemyAttackSidescroll.npcs = npcsRight;
        enemyMovementSidescroll.wallTarget = wallRight;
        enemyMovementSidescroll.comeFromLeft = false;
        enemyMovementSidescroll.speed += Random.Range(1,4);
    }

    void setValuesLeft(GameObject gb)
    {
        EnemyMovementSidescroll enemyMovementSidescroll = gb.GetComponent<EnemyMovementSidescroll>();
        EnemyAttackSidescroll enemyAttackSidescroll = gb.GetComponent<EnemyAttackSidescroll>();
        enemyMovementSidescroll.cityHall = cityHall;
        enemyAttackSidescroll.wall = wallLeft.gameObject;
        enemyAttackSidescroll.npcs = npcsLeft;
        enemyMovementSidescroll.wallTarget = wallLeft;
        enemyMovementSidescroll.comeFromLeft = true;
        enemyMovementSidescroll.speed += Random.Range(1,4);
    }
}
