using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour
{

    public Animator transition;

    public void LoadNextLevel(string nextLevel)
    {
        StartCoroutine(LoadLevel(nextLevel));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator LoadLevel(string lvl)
    {
        transition.SetTrigger("Start");

        yield return new WaitForSeconds(1);

        SceneManager.LoadScene(lvl);
    }
}
