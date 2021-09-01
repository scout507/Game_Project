using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HudSidescroll : MonoBehaviour
{
    public TextMeshProUGUI[] ressourcesText;
    public TextMeshProUGUI dayText;
    GameManager gameManager;

    private void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    void Update()
    {
        for (int i = 0; i < ressourcesText.Length; i++)
        {
            ressourcesText[i].text = gameManager.resources[i].ToString();
        }

        dayText.text = "Day " + gameManager.day;
    }
}
