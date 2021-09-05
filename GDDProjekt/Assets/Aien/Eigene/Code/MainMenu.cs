using UnityEngine;

public class MainMenu : MonoBehaviour
{

    GameManager gm;

    private void Start()
    {
        gm = FindObjectOfType<GameManager>();
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
