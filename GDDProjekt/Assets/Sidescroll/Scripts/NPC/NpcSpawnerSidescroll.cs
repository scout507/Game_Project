using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class NpcSpawnerSidescroll : MonoBehaviour
{
    //public variables
    public GameObject noramlNpcPrefab;
    public GameObject sniperNpcPrefab;
    public GameObject mortarNpcPrefab;

    public int amountNoramlNpcPrefabPerSide;
    public int amountSniperNpcPrefabPerSide;
    public int amountMortarNpcPrefabPerSide;

    public GameObject spawner;
    public int waves;
    public float startDelay;
    public float nextWaveDelay;

    public Transform cityHall;
    public Transform wallRight;
    public Transform wallLeft;
    public Transform positionSniper;
    public Transform positionMortar;

    public List<Transform> enemysAboveLeft;
    public List<Transform> enemysBelowLeft;
    public List<Transform> enemysAboveRight;
    public List<Transform> enemysBelowRight;

    //private variables
    float wavesTimer;
    float waitTime;
    int wavesCounter;

    void Start()
    {
        waitTime = startDelay;
    }

    void Update()
    {
        wavesTimer += Time.deltaTime;

        if (wavesTimer >= waitTime && wavesCounter < waves)
        {
            waitTime = nextWaveDelay;
            wavesCounter++;

            for (int i = 0; i < amountNoramlNpcPrefabPerSide; i++)
            {
                setValuesLeft(Instantiate(noramlNpcPrefab, spawner.transform.position, spawner.transform.rotation));
                setValuesRight(Instantiate(noramlNpcPrefab, spawner.transform.position, spawner.transform.rotation));
            }

            for (int i = 0; i < amountMortarNpcPrefabPerSide; i++)
            {
                setValuesLeft(Instantiate(mortarNpcPrefab, spawner.transform.position, spawner.transform.rotation));
                setValuesRight(Instantiate(mortarNpcPrefab, spawner.transform.position, spawner.transform.rotation));
            }

            for (int i = 0; i < amountSniperNpcPrefabPerSide; i++)
            {
                setValuesLeft(Instantiate(sniperNpcPrefab, spawner.transform.position, spawner.transform.rotation));
                setValuesRight(Instantiate(sniperNpcPrefab, spawner.transform.position, spawner.transform.rotation));
            }
        }
    }

    void setValuesRight(GameObject gb)
    {
        NpcMovementSidescroll npcMovementSidescroll = gb.GetComponent<NpcMovementSidescroll>();
        NpcAttackSidescroll npcAttackSidescroll = gb.GetComponent<NpcAttackSidescroll>();
        npcAttackSidescroll.enemysAbove = enemysAboveRight;
        npcAttackSidescroll.enemysBelow = enemysBelowRight;
        Debug.Log(PrefabUtility.GetPrefabAssetType(gb));
        npcMovementSidescroll.targetToMove = wallRight;
        npcMovementSidescroll.speed += Random.Range(1, 4);
        npcMovementSidescroll.cityHall = cityHall;
    }

    void setValuesLeft(GameObject gb)
    {
        NpcMovementSidescroll npcMovementSidescroll = gb.GetComponent<NpcMovementSidescroll>();
        NpcAttackSidescroll npcAttackSidescroll = gb.GetComponent<NpcAttackSidescroll>();
        npcAttackSidescroll.enemysAbove = enemysAboveLeft;
        npcAttackSidescroll.enemysBelow = enemysBelowLeft;
        npcMovementSidescroll.targetToMove = wallLeft;
        npcMovementSidescroll.speed += Random.Range(1, 4);
        npcMovementSidescroll.cityHall = cityHall;
    }
}
