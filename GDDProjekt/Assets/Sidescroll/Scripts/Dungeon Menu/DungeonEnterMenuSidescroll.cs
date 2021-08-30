using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class DungeonEnterMenuSidescroll : MonoBehaviour
{
    public Toggle rifle;
    public Toggle grenadelauncher;
    public Toggle shotgun;
    public GameManager gameManager;

    void Update()
    {
        if (rifle.isOn && grenadelauncher.isOn && shotgun.isOn) rifle.isOn = false;
    }

    public void back()
    {
        gameObject.SetActive(false);
    }

    public void loadDungeon()
    {
        List<int> tmp = new List<int>();

        if (rifle.isOn) tmp.Add(0);
        if (shotgun.isOn) tmp.Add(1);
        if (grenadelauncher.isOn) tmp.Add(2);

        if (tmp.Count == 2)
        {
            gameManager.wasInDungeon = true;
            gameManager.hasSlept = false;
            gameManager.saveGame();
            gameManager.selectedWeapons[0] = tmp[0];
            gameManager.selectedWeapons[1] = tmp[1];
            SceneManager.LoadScene("MapGeneration");
        }
    }
}
