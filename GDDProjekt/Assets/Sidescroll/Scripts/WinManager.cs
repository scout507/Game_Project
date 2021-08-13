using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WinManager : MonoBehaviour
{
    public float delayDespawnTimerNpc;
    public float delayBackToVillage;
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

        if ((aiManager.wallLeft == null || aiManager.wallRight == null))
        {
            aiManager.gameEndEnemy = true;
            aiManager.stopNpcs = true;
        }

        if (aiManager.wallLeft == null) Destroy(aiManager.wallRight);
        if (aiManager.wallRight == null) Destroy(aiManager.wallLeft);

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