using UnityEngine;
using UnityEngine.Audio;
using System.Linq;
using UnityEngine.UI;
using System.Collections.Generic;
using System;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
    #region Variables
    //Variable for instance of the settings menu manager (singleton)
    public static SettingsMenu SharedInstance;

    //Variables for audio
    [SerializeField] AudioMixer mixer;

    //Variables for resolution
    private Resolution[] resolutions;
    [SerializeField] private Dropdown resolutionDropdown;
    private int currentResolutionIndex;
    private Resolution chosenResolution;

    //Variables for fullscreen mode
    private bool isFullscreen;
    [SerializeField] private Toggle fullScreenToggle;

    //Variables for volume
    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider soundEffectsVolumeSlider;
    private float volume;
    private float masterVolume;
    private float musicVolume;
    private float soundEffectsVolume;
    #endregion

    #region UnityMethods
    private void Awake()
    {
        //Get the instance for settings menu
        SharedInstance = this;

        //Get all available screen resolutions
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();

        //Load settings
        LoadSettings();

        //Display resolution options in dropdown
        SetResolutionDropdown(resolutions);

        //Set the toggle fullscreen value based on current fullscreen aspect
        fullScreenToggle.isOn = Screen.fullScreen;

        //Set the sliders value based on current audio mixer value
        mixer.GetFloat("MasterVolume", out volume);
        masterVolumeSlider.value = LogValueToSlider(volume);
        mixer.GetFloat("MusicVolume", out volume);
        musicVolumeSlider.value = LogValueToSlider(volume);
        mixer.GetFloat("SoundEffectsVolume", out volume);
        soundEffectsVolumeSlider.value = LogValueToSlider(volume);
    }
    #endregion

    #region Methods
    public void ApplySettingsButton()
    {
        //Set volumes
        mixer.SetFloat("MasterVolume", masterVolume);
        mixer.SetFloat("MusicVolume", musicVolume);
        mixer.SetFloat("SoundEffectsVolume", soundEffectsVolume);


        //Set resolution
        Screen.SetResolution(chosenResolution.width, chosenResolution.height, isFullscreen);

        //Save settings
        SaveSettings();
    }

    public void SetResolutionDropdown(Resolution[] resolutions)
    {
        //Variables for adding resolutions in dropdown
        string resolutionString;
        List<string> options = new List<string>();
        var count = 0;

        //Get every resolutions available and add them in a list
        foreach(var element in resolutions)
        {
            resolutionString = element.width + "x" + element.height;
            options.Add(resolutionString);

            //Get the current resolution
            if (element.width == Screen.width && element.height == Screen.height)
            {
                currentResolutionIndex = count;
            }

            count++;
        }

        //Add every resolution in dropdown
        resolutionDropdown.ClearOptions();
        resolutionDropdown.AddOptions(options);
        //Mark the current resolution as selected
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SaveSettings()
    {
        //Save volumes
        PlayerPrefs.SetFloat("masterVolume", masterVolume);
        PlayerPrefs.SetFloat("musicVolume", musicVolume);
        PlayerPrefs.SetFloat("soundEffectsVolume", soundEffectsVolume);

        //Save resolution
        PlayerPrefs.SetInt("chosenResolutionWidth", chosenResolution.width);
        PlayerPrefs.SetInt("chosenResolutionHeight", chosenResolution.height);

        //Save screen mode
        PlayerPrefs.SetInt("isFullScreen", Convert.ToInt32(isFullscreen));
    }

    public void LoadSettings()
    {
        //Load volumes
        masterVolume = PlayerPrefs.GetFloat("masterVolume", 0);
        musicVolume = PlayerPrefs.GetFloat("musicVolume", 0);
        soundEffectsVolume = PlayerPrefs.GetFloat("soundEffectsVolume", 0);
        mixer.SetFloat("MasterVolume", masterVolume);
        mixer.SetFloat("MusicVolume", musicVolume);
        mixer.SetFloat("SoundEffectsVolume", soundEffectsVolume);

        //Load fullscreen mode
        if (PlayerPrefs.GetInt("isFullScreen", 0) == 0)
        {
            isFullscreen = false;
        }
        else
        {
            isFullscreen = true;
        }

        //Load resolution
        chosenResolution.width = PlayerPrefs.GetInt("chosenResolutionWidth", 1920);
        chosenResolution.height = PlayerPrefs.GetInt("chosenResolutionHeight", 1080);
        Screen.SetResolution(chosenResolution.width, chosenResolution.height, isFullscreen);
    }

    public void ResolutionChanged(int resolutionIndex)
    {
        //Get the resolution choosed by player
        chosenResolution = resolutions[resolutionIndex];
    }

    public void FullscreenChanged(bool toggle)
    {
        //Get the toggle value
        isFullscreen = toggle;
    }

    public void MasterVolumeChanged(float volume)
    {
        //Get slider value for master volume
        masterVolume = SliderToLogValue(volume);
    }

    public void MusicVolumeChanged(float volume)
    {
        //Get slider value for music volume
        musicVolume = SliderToLogValue(volume);
    }

    public void SoundEffectsVolumeChanged(float volume)
    {
        //Get slider value for sound effects volume
        soundEffectsVolume = SliderToLogValue(volume);
    }

    private float SliderToLogValue(float volume)
    {
        return Mathf.Log10(volume) * 20;
    }

    private float LogValueToSlider(float volume)
    {
        return Mathf.Pow(10, volume / 20);
    }

    public void EventSystemSelectedElement(GameObject go)
    {
        //Clear event system
        EventSystem.current.SetSelectedGameObject(null);
        //Set selected gameobject
        EventSystem.current.SetSelectedGameObject(go);
    }
    #endregion
}
