using System.Collections;
using System.IO;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null; 

    //Save Game related stats
    public int[] resources;
    public int[] selectedWeapons = new int[2];
    public Weaponstats rifle;
    public Weaponstats shotgun;
    public Weaponstats grenadeLauncher;
    public int day;
    // 0=easy, 1=normal, 2=hardcore; 
    public int difficulty = 1;

    public int normalNpcLife = 100;
    public int normalNpcDamage = 100;
    public int sniperNpcLife = 100;
    public int sniperNpcDamage = 100;
    public int mortarNpcLife = 100;
    public int mortarNpcDamage = 100;
    
    public int dungeonLevel = 0;

    //Settings
    public Settings currentSettings;


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
        //loadGame();

        //get the settings
        currentSettings = loadSettings();
        //if there are no saved settings init new ones
        if(currentSettings == null){
            currentSettings = new Settings(1, 1, 1);
            saveSettings(currentSettings);
        } 
    }

    void startNewGame(){
        initWeapons();
        saveGame();
        SceneManager.LoadScene("Sidescroll");
    }

    void initWeapons(){
        rifle.init(10,5,0.5f,2,0,0,0);
        shotgun.init(25,1,1,1,6,4,0);
        grenadeLauncher.init(50,1,11,2.5f,0,0,6);
    }

    public void saveGame(){
        //TODO add filename
        SaveGame save = new SaveGame(resources,rifle,shotgun,grenadeLauncher,day,difficulty);
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
            this.difficulty = loadedSave.difficulty;
            SceneManager.LoadScene("Sidescroll");
        }      
    }

    void saveSettings(Settings settings){
        string json = JsonUtility.ToJson(settings);
        File.WriteAllText(Application.dataPath + "/settings.txt", json);
    }

    Settings loadSettings(){

        if(File.Exists(Application.dataPath + "/settings.txt")){
            string saveString = File.ReadAllText(Application.dataPath + "/save.txt");
            Settings loadedSettings = JsonUtility.FromJson<Settings>(saveString);
            SoundManager soundManager = GetComponentInChildren<SoundManager>();
            soundManager.globalVolume = loadedSettings.masterVolume;
            soundManager.sfxVolume = loadedSettings.sfxVolume;
            soundManager.musicVolume = loadedSettings.musicVolume;
            return loadedSettings;
        }
        return null;
    }

    public class SaveGame{

        public int[] resources;
        public int[] selectedWeapons;
        public Weaponstats rifle;
        public Weaponstats shotgun;
        public Weaponstats grenadeLauncher;
        public int day;
        public int difficulty;

        public SaveGame(int[] res, Weaponstats rifle, Weaponstats shotgun, Weaponstats grenadeLauncher, int day, int difficulty){
            this.resources = res;
            this.rifle = rifle;
            this.shotgun = shotgun;
            this.grenadeLauncher = grenadeLauncher;
            this.day = day;
            this.difficulty = difficulty;
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
