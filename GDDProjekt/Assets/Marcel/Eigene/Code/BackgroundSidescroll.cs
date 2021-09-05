using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSidescroll : MonoBehaviour
{
    public GameObject day;
    public GameObject night;
    GameManager gameManager;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (gameManager.wasInDungeon)
        {
            day.SetActive(false);
            night.SetActive(true);
        }
        else
        {
            day.SetActive(true);
            night.SetActive(false);
        }
    }
}
