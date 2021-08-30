using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    GameManager gm;
    GameManager.Settings settings;

    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider musicSlider;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        settings = gm.loadSettings();

        masterSlider.value = settings.masterVolume;
        sfxSlider.value = settings.sfxVolume;
        musicSlider.value = settings.musicVolume;
    }

    public void SetInGameMenu(bool inGameMenu)
    {
        gm.InGameMenu = inGameMenu;
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

    public void ReturnToVillage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Sidescroll");
    }

    public void SaveGame()
    {
        gm.saveGame();
        Resume();
    }

    public void SetMasterVolume(float vol)
    {
        gm.currentSettings.masterVolume = vol;
    }

    public void SetSfxVolume(float vol)
    {
        gm.currentSettings.sfxVolume = vol;
    }

    public void SetMusicVolume(float vol)
    {
        gm.currentSettings.musicVolume = vol;
    }

    public void SaveSettings()
    {
        gm.saveSettings(gm.currentSettings);
    }
}
