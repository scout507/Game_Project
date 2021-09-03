using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Hud : MonoBehaviour
{
    public GameObject rightWarning;
    public GameObject leftWarning;
    public WallSidescroll wallRight;
    public WallSidescroll wallLeft;
    public GameObject looseText;
    public AiManager aiManager;

    void Update()
    {
        if (wallLeft.life < wallLeft.maxlife / 2) leftWarning.SetActive(true);
        else leftWarning.SetActive(false);

        if (wallRight.life < wallRight.maxlife / 2) rightWarning.SetActive(true);
        else rightWarning.SetActive(false);

        if (wallLeft.life <= 0 || wallRight.life <= 0)
        {
            leftWarning.SetActive(false);
            rightWarning.SetActive(false);
        }

        if (aiManager.gameEndEnemy)
            looseText.SetActive(true);
    }

    public void backButton()
    {
        aiManager.gameManager.deleteGame();
        SceneManager.LoadScene("Menu");
    }
}
