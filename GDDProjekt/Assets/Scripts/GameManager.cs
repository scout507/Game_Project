using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; 
    public int[] resources;
    public int[] selectedWeapons;
    public Weaponstats rifle;
    public Weaponstats shotgun;
    public Weaponstats grenadeLauncher;
    



    private void Awake()
    {
        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);
        //rifle.init(10,5,0.5f,2,0,0,0);   
        DontDestroyOnLoad(this.gameObject);
    }

    void startNewGame(){
        rifle.init(10,5,0.5f,2,0,0,0);
    }

    private void Update()
    {
        
    }



}
