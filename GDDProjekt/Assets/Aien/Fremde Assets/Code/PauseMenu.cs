using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    SoundManager soundManager;
    GameManager gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        soundManager = GameObject.FindGameObjectWithTag("GameManager").GetComponentInChildren<SoundManager>();
    }

    public void Resume()
    {
        gm.GamePaused = false;
        Time.timeScale = 1f;
        pauseMenuUI.SetActive(false);
        
    }

    public void Pause()
    {
        gm.GamePaused = true;
        Time.timeScale = 0f;
        pauseMenuUI.SetActive(true);
        
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Leave()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }

    public void SaveSettings(){
        gm.saveSettings(gm.currentSettings);
        soundManager.ResetAllSounds();
    }

    public void SaveGame()
    {
        gm.saveGame();
        Resume();
    }
}
