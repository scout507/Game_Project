using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class WallSidescroll : BuildingOverClassSidescroll
{
    //public
    public GameObject title;
    public GameObject interact;
    public GameObject collosionCollider;
    public int life;
    public int maxlife;
    public float maxtimer = 10;
    GameManager gameManager;

    //private
    PlayerMovementSidescroll playerMovementSidescroll;
    TextMeshProUGUI textMeshProTitle;
    TextMeshProUGUI textMeshProTitleInteract;
    string textMeshProTitleOld;
    string textMeshProTitleInteractOld;
    float timer;
    bool isentered;

    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        maxlife = Mathf.CeilToInt((float)maxlife * Mathf.Pow(1.1f, gameManager.wall));
        playerMovementSidescroll = player.GetComponent<PlayerMovementSidescroll>();
        textMeshProTitle = title.GetComponent<TextMeshProUGUI>();
        textMeshProTitleInteract = interact.GetComponent<TextMeshProUGUI>();
        life = maxlife;
    }

    void Update()
    {
        if (isentered)
        {
            if (life < maxlife)
            {
                timer -= Time.deltaTime;
                textMeshProTitleInteract.text = Mathf.RoundToInt(timer).ToString();
                if (timer <= 0) life = maxlife;
            }
            else
            {
                textMeshProTitle.text = "Vollständig repariert";
                textMeshProTitleInteract.text = "zurück [E]";
            }
        }

        if (life <= 0)
        {
            collosionCollider.GetComponent<SpriteRenderer>().enabled = false;
        }
    }

    public override void activateMenu()
    {
        timer = maxtimer;
        isentered = true;
        playerMovementSidescroll.inEvent = true;
        textMeshProTitleOld = textMeshProTitle.text;
        textMeshProTitleInteractOld = textMeshProTitleInteract.text;
        textMeshProTitle.text = "wird repariert...";

    }

    public override void deactivateMenu()
    {
        isentered = false;
        playerMovementSidescroll.inEvent = false;
        textMeshProTitle.text = textMeshProTitleOld;
        textMeshProTitleInteract.text = textMeshProTitleInteractOld;
    }

    public void takeDamage(int damage)
    {
        life -= damage;
    }
}
