using System;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider masterSlider;
    public TMP_Text masterText;
    public Slider musicSlider;
    public TMP_Text musicText;
    public Slider sfxSlider;
    public TMP_Text sfxText;

    public Slider mouseSlider;
    public TMP_Text mouseText;
    public Slider fovSlider;
    public TMP_Text fovText;

    public Toggle toggle;

    Resolution[] resolutions;
    public TMP_Dropdown dropDown;


    private void Start()
    {
        resolutions = Screen.resolutions;
        dropDown.ClearOptions();

        List<string> options = new List<string>();
        int currentResIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            Resolution resolution = resolutions[i];
            string option = resolution.width + "x" + resolution.height;
            options.Add(option);

            if (resolution.width == Screen.currentResolution.width && resolution.height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }
        Settings.instance.resolutionIndex = currentResIndex;
        dropDown.AddOptions(options);
        dropDown.value = currentResIndex;
        dropDown.RefreshShownValue();

        UpdateValues();
    }

    public void GetVolume(string param)
    {
        if (param.Contains("Master"))
        {
            float volume = masterSlider.value;
            masterMixer.SetFloat(param, volume);
        }
        else if (param.Contains("Music"))
        {
            float volume = musicSlider.value;
            masterMixer.SetFloat(param, volume);
        }
        else
        {
            float volume = sfxSlider.value;
            masterMixer.SetFloat(param, volume);
        }
    }

    public void SetFullScreen()
    {
        Screen.fullScreen = toggle.isOn;
        Settings.instance.toggleOn = toggle.isOn;
    }

    public void SetResolution()
    {
        Resolution resolution = resolutions[dropDown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
        Settings.instance.resolutionIndex = dropDown.value;
    }

    public void MouseSensitivity()
    {
        Settings.instance.mouseSense = mouseSlider.value;
        if(GameManager.instance != null)
        {
            GameManager.instance.player.GetComponentInChildren<MouseLook>().mouseSens = mouseSlider.value;
        }
    }

    public void FOV()
    {
        Settings.instance.fov = fovSlider.value;
        if (GameManager.instance != null)
        {
            GameManager.instance.player.GetComponentInChildren<Camera>().fieldOfView = fovSlider.value;
        }
    }

    public void UpdateValues()
    {
        float value;
        fovSlider.value = Settings.instance.fov;
        mouseSlider.value = Settings.instance.mouseSense;
        dropDown.value = Settings.instance.resolutionIndex;
        toggle.isOn = Settings.instance.toggleOn;
        dropDown.RefreshShownValue();
        masterMixer.GetFloat("MasterVolume", out value);  
        masterSlider.value = value;
        masterMixer.GetFloat("MusicVolume", out value);  
        musicSlider.value = value;
        masterMixer.GetFloat("SFXVolume", out value);  
        sfxSlider.value = value;
    }

    private void Update()
    {
        masterText.text = masterSlider.value.ToString("0.00");
        musicText.text = musicSlider.value.ToString("0.00");
        sfxText.text = sfxSlider.value.ToString("0.00");
        mouseText.text = mouseSlider.value.ToString("0.00");
        fovText.text = fovSlider.value.ToString("0.00");

    }
}
