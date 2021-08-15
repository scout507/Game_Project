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
    public float globalVolume = 0f;

    void Awake()
    {
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

        // to make the sound manager persistent between
        // scenes
        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            if (globalVolume != 0)
            {
                s.source.volume = globalVolume;
            }
            else
            {
                s.source.volume = s.volume;
            }

            s.source.pitch = s.pitch;
            s.source.loop = s.shouldLoop;
        }
    }

    private void Start()
    {
        if (themeSound != null && themeSound.clip != null)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = themeSound.clip;
            source.loop = themeSound.shouldLoop;
            source.volume = themeSound.volume;
            source.pitch = themeSound.pitch;
            source.Play();
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
