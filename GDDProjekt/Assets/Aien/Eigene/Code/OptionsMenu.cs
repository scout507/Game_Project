using UnityEngine.UI;
using UnityEngine;

public class OptionsMenu : MonoBehaviour
{

    GameManager gm;
    GameManager.Settings settings;

    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider musicSlider;

    private void Start()
    {
        gm = GetComponent<GameManager>();
        settings = gm.loadSettings();

        masterSlider.value = settings.masterVolume;
        sfxSlider.value = settings.sfxVolume;
        musicSlider.value = settings.musicVolume;
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
