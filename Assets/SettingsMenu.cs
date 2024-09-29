using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public AudioMixer masterMixer;
    public Slider masterSlider;
    public Slider musicSlider;
    public Slider sfxSlider;

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

        dropDown.AddOptions(options);
        dropDown.value = currentResIndex;
        dropDown.RefreshShownValue();
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
    }

    public void SetResolution()
    {
        Resolution resolution = resolutions[dropDown.value];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
}
