using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{

    public GameObject pauseMenuUI;
    GameManager gm;

    private void Start()
    {
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
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

    public void SaveGame()
    {
        gm.saveGame();
        Resume();
    }
}
