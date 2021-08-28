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
        gm = GetComponentInParent<GameManager>();
    }

    public void Resume()
    {
        gm.GamePaused = false;
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
    }

    public void Pause()
    {
        gm.GamePaused = true;
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
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
