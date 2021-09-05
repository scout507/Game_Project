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
        gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        settings = gm.loadSettings();

        masterSlider.value = settings.masterVolume;
        sfxSlider.value = settings.sfxVolume;
        musicSlider.value = settings.musicVolume;
    }

    public void SetMasterVolume()
    {
        gm.currentSettings.masterVolume = masterSlider.value;
    }

    public void SetSfxVolume()
    {
        gm.currentSettings.sfxVolume = sfxSlider.value;
    }

    public void SetMusicVolume()
    {
        gm.currentSettings.musicVolume = musicSlider.value;
    }

    public void SaveSettings()
    {
        gm.saveSettings(gm.currentSettings);
    }
}
