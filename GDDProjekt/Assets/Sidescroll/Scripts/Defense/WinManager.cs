using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Cinemachine;
using System.Linq;

public class WinManager : MonoBehaviour
{
    public float delayDespawnTimerNpc;
    public float delayBackToVillage;
    public GameObject player;
    public CinemachineVirtualCamera cam;

    AiManager aiManager;
    float timer;
    bool entered = false;
    bool entered2 = false;
    EnemySpawnerSidescroll enemySpawnerSidescroll;

    void Start()
    {
        aiManager = GetComponent<AiManager>();
        enemySpawnerSidescroll = GetComponent<EnemySpawnerSidescroll>();
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= enemySpawnerSidescroll.startDelay + 3 && enemysDown() && !entered && enemySpawnerSidescroll.waves == enemySpawnerSidescroll.wavesCounter)
        {
            entered = true;
            aiManager.gameEndNpc = true;
        }

        if ((aiManager.wallLeft.GetComponent<WallSidescroll>().life <= 0 || aiManager.wallRight.GetComponent<WallSidescroll>().life <= 0))
        {
            aiManager.wallLeft.GetComponent<WallSidescroll>().life = 0;
            aiManager.wallRight.GetComponent<WallSidescroll>().life = 0;
            aiManager.gameEndEnemy = true;
            aiManager.stopNpcs = true;
            player.GetComponent<PlayerMovementSidescroll>().inEvent = true;
            if (aiManager.enemysBelowLeft.Count != 0) cam.Follow = aiManager.enemysBelowLeft.First().transform;
            else if (aiManager.enemysBelowRight.Count != 0) cam.Follow = aiManager.enemysBelowRight.First().transform;
            else cam.Follow = aiManager.cityHall.transform;
        }

        if (aiManager.gameEndNpc && aiManager.npcsLeft.Count == 0 && aiManager.npcsRight.Count == 0 && !entered2)
        {
            timer = 0;
            entered2 = true;
        }

        if (entered2 && timer >= delayBackToVillage)
        {
            SceneManager.LoadScene("Sidescroll");
        }
    }

    bool enemysDown()
    {
        if (aiManager.enemysAboveLeft.Count == 0 && aiManager.enemysBelowLeft.Count == 0 && aiManager.enemysAboveRight.Count == 0 && aiManager.enemysBelowRight.Count == 0)
            return true;
        return false;
    }
}