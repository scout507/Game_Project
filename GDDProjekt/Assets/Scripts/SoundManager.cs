using UnityEngine.Audio;
using System;
using UnityEngine;

// How to use?
// 1. using `FindObjectOfType<SoundManager>().Play(...)` (recommended)
// 2. Passing the sound manager as a property to a game object

public class SoundManager : MonoBehaviour
{

    public Sound[] sounds;
    public Sound[] themeSounds;
    public static SoundManager instance;

    [Range(0f, 1f)]
    public float globalVolume = 1f;
    [Range(0f, 1f)]
    public float sfxVolume = 1f;
    [Range(0f, 1f)]
    public float musicVolume = 1f;

    GameManager gameManager;

    void Awake()
    {
        gameManager = GetComponentInParent<GameManager>();
        // a singleton pattern to make sure that the
        // sound manager won't be destroyed when we
        // change between scenes
        if (instance == null)
        {
            instance = this;
        } else
        {
            Destroy(gameObject);
            return;
        }
        
    }

    private void Start()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume*gameManager.currentSettings.sfxVolume*gameManager.currentSettings.masterVolume;
            s.source.pitch = s.pitch;
            s.source.loop = s.shouldLoop;
        }

        foreach (Sound s in themeSounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume * gameManager.currentSettings.sfxVolume * gameManager.currentSettings.masterVolume;
            s.source.pitch = s.pitch;
            s.source.loop = s.shouldLoop;
        }

        // start the first menu sound on start
        themeSounds[0].source.Play();
    }

    public void ResetAllSounds()
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = s.volume * gameManager.currentSettings.sfxVolume * gameManager.currentSettings.masterVolume;
        }

        foreach (Sound s in themeSounds)
        {
            s.source.volume = s.volume * gameManager.currentSettings.sfxVolume * gameManager.currentSettings.masterVolume;
        }
    }

    /**
     * play a sound
     * @param {string} name the name of the sound
     * @param {bool} searchInTheme if the sound belongs to theme
     */
    public void Play(string name, bool searchInTheme = false)
    {
        Sound s = GetSound(name, searchInTheme);

        if (s == null)
        {
            Debug.LogError("cannot find the sound '" + name + "'!");
            return;
        }

        if (s.source == null)
        {
            Debug.LogError("'" + s.name + "' has no source");
            return;
        }

        s.source.Play();
    }

    /**
     * stop a sound
     * @param {string} name the name of the sound
     * @param {bool} searchInTheme if the sound belongs to theme
     */
    public void Stop(string name, bool searchInTheme = false)
    {
        Sound s = GetSound(name, searchInTheme);

        if (s == null)
        {
            Debug.LogError("cannot find the sound '" + name + "'!");
            return;
        }

        if (s.source == null)
        {
            Debug.LogError("'" + s.name + "' has no source");
            return;
        }

        s.source.Stop();
    }

    /**
     * play a sound with delay
     * @param {string} name the name of the sound
     * @param {float} time to delay the play
     * @param {bool} searchInTheme if the sound belongs to theme
     */
    public void Play(string name, float delay, bool searchInTheme = false)
    {
        Sound s = GetSound(name, searchInTheme);

        if (s == null)
        {
            Debug.LogError("cannot find the sound '" + name + "'!");
            return;
        }

        s.source.PlayDelayed(delay);
    }

    /*
     * get the source of a sound
     * @param {string} name name of the sound
     * @param {bool} searchInTheme if the sound belongs to theme
     */
    public AudioSource SourceOf(string name, bool searchInTheme = false)
    {
        return GetSound(name, searchInTheme).source;
    }

    /*
     * get/find a sound in a list
     * @param {string} name name of the sound
     * @param {bool} searchInTheme if the sound belongs to theme
     */
    public Sound GetSound(string name, bool searchInTheme = false)
    {

        if (searchInTheme)
        {
            return Array.Find(themeSounds, sound => sound.name.ToLower() == name.ToLower());
        }
        return Array.Find(sounds, sound => sound.name.ToLower() == name.ToLower());
    }

    /*
     * toggle play/stop a sound
     * @param {string} name name of the sound
     * @param {bool} searchInTheme if the sound belongs to theme
     */
    public void Toggle(string name, bool toggle, bool searchInTheme = false)
    {
        Sound s = GetSound(name, searchInTheme);

        if (s == null)
        {
            Debug.LogError("cannot find the sound '" + name + "'!");
            return;
        }

        if (toggle && !s.source.isPlaying)
        {
            Play(name);
        }
        if (!toggle)
        {
            s.source.Stop();
        }
    }
}
