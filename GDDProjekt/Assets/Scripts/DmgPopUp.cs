using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DmgPopUp : MonoBehaviour
{
    public TextMeshPro txt;
    public float ySpeed;
    public float xSpeed;
    private float timer = 0.35f;
    float damage;

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
