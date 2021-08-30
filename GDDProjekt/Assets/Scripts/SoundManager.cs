using UnityEngine.Audio;
using System;
using UnityEngine;

// How to use?
// 1. using `FindObjectOfType<SoundManager>().Play(...)` (recommended)
// 2. Passing the sound manager as a property to a game object

public class SoundManager : MonoBehaviour
{

    public Sound[] sounds;
    public Sound themeSound;
    public static SoundManager instance;

    [Range(0f, 1f)]
    public float globalVolume = 1f;
    [Range(0f, 1f)]
    public float sfxVolume = 1f;
    [Range(0f, 1f)]
    public float musicVolume = 1f;

    GameManager gameManager;
    AudioSource themeSource;

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
            s.source.pitch = 1;
            s.source.loop = s.shouldLoop;
        }

        StartThemeSound(); 
    }

    public void StartThemeSound()
    {
        if (themeSound != null && themeSound.clip != null && themeSource == null)
        {
            gameManager = GetComponentInParent<GameManager>();
            themeSource = gameObject.AddComponent<AudioSource>();
            themeSource.clip = themeSound.clip;
            themeSource.loop = themeSound.shouldLoop;
            themeSource.volume = themeSound.volume * gameManager.currentSettings.musicVolume * gameManager.currentSettings.masterVolume;
            themeSource.pitch = themeSound.pitch;
            themeSource.Play();
        } else if (themeSource != null)
        {
            themeSource.Play();
        }
    }

    public void StopThemeSound()
    {
        if (themeSource != null)
        {
            themeSource.Stop();
        }
    }

    /**
     * @param {string} name the name of the sound
     */
    public void Play(string name)
    {
        Sound s = GetSound(name);

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

    public void Play(string name, float delay)
    {
        Sound s = GetSound(name);

        if (s == null)
        {
            Debug.LogError("cannot find the sound '" + name + "'!");
            return;
        }

        s.source.PlayDelayed(delay);
    }

    public AudioSource SourceOf(string name)
    {
        return GetSound(name).source;
    }

    public Sound GetSound(string name)
    {
        return Array.Find(sounds, sound => sound.name.ToLower() == name.ToLower());
    }

    public void PlayOnToggle(string name, bool toggle)
    {
        Sound s = GetSound(name);

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
