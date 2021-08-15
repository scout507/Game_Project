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
        playerMovementSidescroll = player.GetComponent<PlayerMovementSidescroll>();
        textMeshProTitle = title.GetComponent<TextMeshProUGUI>();
        textMeshProTitleInteract = interact.GetComponent<TextMeshProUGUI>();
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

        if (life <= 0) Destroy(gameObject);
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
