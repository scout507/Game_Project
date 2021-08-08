using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgPopUp : MonoBehaviour
{
    private TextMeshPro txt;
    private float timer = 0.35f;
    private float damage;

    private void Awake()
    {
        txt = GetComponentInChildren<TextMeshPro>();
    }

    public void Setup(float dmg){
        txt.SetText(Mathf.RoundToInt(dmg).ToString());
        damage += dmg;
    }

    public void updateText(float dmg){
        txt.SetText(Mathf.RoundToInt(damage).ToString());
        damage += dmg;
    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if(timer <= 0){
            Destroy(this.gameObject);
        }
    }
}
