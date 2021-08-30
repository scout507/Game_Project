using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudSidescroll : MonoBehaviour
{
    public TextMeshProUGUI[] ressourcesText;
    public GameManager gameManager;
    public TextMeshProUGUI dayText;

    void Update()
    {
        for (int i = 0; i < ressourcesText.Length; i++)
        {
            ressourcesText[i].text = gameManager.resources[i].ToString();
        }

        dayText.text = "Day " + gameManager.day;
    }
}
