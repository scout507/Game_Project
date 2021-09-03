using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInputter : MonoBehaviour
{
    SoundManager soundManager;
    public AudioClip theme;
    
    void Start()
    {
        soundManager = FindObjectOfType<SoundManager>();
        switchSong(theme);
    }

    public void switchSong(AudioClip song){
        //soundManager.StopThemeSound();
        soundManager.themeSource.clip = song;
        soundManager.StartThemeSound();
    }
}
