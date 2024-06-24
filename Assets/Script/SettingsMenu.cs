using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public TMP_Dropdown resolutionDropdown;
    public TMP_Dropdown qualityDropdown;
    public Slider sensitivitySlider;
    Resolution[] resolutions;

    private void Start()
    {
        LoadQualityLevel();
        LoadSensitivity();

        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height + " " + resolutions[i].refreshRateRatio.numerator / resolutions[i].refreshRateRatio.denominator + "Hz";
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width
                && resolutions[i].height == Screen.currentResolution.height
                && resolutions[i].refreshRateRatio.Equals(Screen.currentResolution.refreshRateRatio))
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        SetQualityDropdownOptions();
    }

    private void SetQualityDropdownOptions()
    {
        List<string> options = new List<string>();
        string[] qualityNames = QualitySettings.names;
        int currentQualityLevel = QualitySettings.GetQualityLevel();

        foreach (string name in qualityNames)
        {
            options.Add(name);
        }

        qualityDropdown.ClearOptions();
        qualityDropdown.AddOptions(options);
        qualityDropdown.value = currentQualityLevel;
        qualityDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRateRatio);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("Volume", volume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        SaveQualityLevel(qualityIndex);
    }

    public void SetFullscreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void SetSensitivity(float sensitivity)
    {
        PlayerPrefs.SetFloat("Sensitivity", sensitivity);
        PlayerPrefs.Save();
    }

    private void SaveQualityLevel(int qualityIndex)
    {
        PlayerPrefs.SetInt("QualityLevel", qualityIndex);
        PlayerPrefs.Save();
    }

    private void LoadQualityLevel()
    {
        if (PlayerPrefs.HasKey("QualityLevel"))
        {
            int qualityIndex = PlayerPrefs.GetInt("QualityLevel");
            QualitySettings.SetQualityLevel(qualityIndex);
        }
        else
        {
            QualitySettings.SetQualityLevel(2);
        }
    }

    private void LoadSensitivity()
    {
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            float sensitivity = PlayerPrefs.GetFloat("Sensitivity");
            sensitivitySlider.value = sensitivity;
        }
        else
        {
            sensitivitySlider.value = 1f;
        }
        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
    }
}