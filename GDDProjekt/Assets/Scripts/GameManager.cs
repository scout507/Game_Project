using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    //Player related stats
    public static GameManager instance = null; 
    public int[] resources;
    public int[] selectedWeapons;
    public Weaponstats rifle;
    public Weaponstats shotgun;
    public Weaponstats grenadeLauncher;
    public int day;

    public int normalNpcLife = 100;
    public int normalNpcDamage = 100;
    public int sniperNpcLife = 100;
    public int sniperNpcDamage = 100;
    public int mortarNpcLife = 100;
    public int mortarNpcDamage = 100;
    
    public int dungeonLevel;

    //Settings
    public int hardCoreMode;

    //SaveGame




    private void Awake()
    {
        loadSettings();

        rifle = new Weaponstats();
        shotgun = new Weaponstats();
        grenadeLauncher = new Weaponstats();
        initWeapons();

        if (instance == null) instance = this;
        else if (instance != this) Destroy(gameObject);  
        DontDestroyOnLoad(this.gameObject);

        
        saveGame();
        loadGame();

        //get the settings

    }

    void startNewGame(){
        initWeapons();
        saveGame();
    }

    void initWeapons(){
        rifle.init(10,5,0.5f,2,0,0,0);
        shotgun.init(25,1,1,1,6,4,0);
        grenadeLauncher.init(50,1,11,2.5f,0,0,6);
    }


    void saveGame(){
        //TODO add filename
        SaveGame save = new SaveGame(resources,rifle,shotgun,grenadeLauncher,day);
        string json = JsonUtility.ToJson(save);
        File.WriteAllText(Application.dataPath + "/save.txt", json);
    }

    void loadGame(){
        //TODO add filename
        if(File.Exists(Application.dataPath + "/save.txt")){
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            SaveGame loadedSave = JsonUtility.FromJson<SaveGame>(saveString);
            this.resources = loadedSave.resources;
            this.rifle = loadedSave.rifle;
            this.shotgun = loadedSave.shotgun;
            this.grenadeLauncher = loadedSave.grenadeLauncher;
            this.day = loadedSave.day;
        }      
    }

    void saveSettings(float master, float sfxVolume, float musicVolume){
        Settings settings = new Settings(master,sfxVolume,musicVolume);
        string json = JsonUtility.ToJson(settings);
        
        File.WriteAllText(Application.dataPath + "/settings.txt", json);
    }

    void loadSettings(){

        if(File.Exists(Application.dataPath + "/settings.txt")){
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            Settings loadedSettings = JsonUtility.FromJson<Settings>(saveString);
            SoundManager soundManager = GetComponentInChildren<SoundManager>();
            soundManager.globalVolume = loadedSettings.masterVolume;
            soundManager.sfxVolume = loadedSettings.sfxVolume;
            soundManager.musicVolume = loadedSettings.musicVolume;
        }
    }


    public class SaveGame{

        public int[] resources;
        public int[] selectedWeapons;
        public Weaponstats rifle;
        public Weaponstats shotgun;
        public Weaponstats grenadeLauncher;
        public int day;

        public SaveGame(int[] res, Weaponstats rifle, Weaponstats shotgun, Weaponstats grenadeLauncher, int day){
            this.resources = res;
            this.rifle = rifle;
            this.shotgun = shotgun;
            this.grenadeLauncher = grenadeLauncher;
            this.day = day;
        } 
    }

    public class Settings{
        public float masterVolume;
        public float sfxVolume;
        public float musicVolume;

        public Settings(float master, float sfx, float music){
            this.masterVolume = master;
            this.sfxVolume = sfx;
            this.musicVolume = music;
        }
    }


}
