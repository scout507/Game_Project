using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
    public int day;
    public GameObject tutorialHolder;

    void Start()
    {
        if(GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().day == day) tutorialHolder.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.H)) activate();
    }

    void activate(){
        Time.timeScale = 0;
        tutorialHolder.SetActive(true);
    }

    public void Deactivate(){
        Time.timeScale = 1;
        tutorialHolder.SetActive(false);
    }
}
