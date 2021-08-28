using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    GameManager gm;

    private void Start()
    {
        gm = GetComponent<GameManager>();
    }

    public void PlayGame()
    {
        gm.startNewGame();
    }

    public void PlaySavedGame()
    {
        gm.startLoadedGame();
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
