using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer AudioMixer1;
    public AudioMixer AudioMixer2;
    public TMP_Dropdown ResolutionDropdown;
    public TMP_Dropdown QualityDropdown;
    public Slider SensitivitySlider;
    public AudioSource ButtonClickSource;
    public AudioClip ButtonClickClip;
    public AudioSource BackgroundMusicSource;
    public AudioClip BackgroundMusicClip;
    Resolution[] _resolutions;

    private void Start()
    {
        LoadQualityLevel();
        LoadSensitivity();

        _resolutions = Screen.resolutions;
        ResolutionDropdown.ClearOptions();

        List<string> options = new List<string>();
        int currentResolutionIndex = 0;

        for (int i = 0; i < _resolutions.Length; i++)
        {
            string option = _resolutions[i].width + "x" + _resolutions[i].height + " " + _resolutions[i].refreshRateRatio.numerator / _resolutions[i].refreshRateRatio.denominator + "Hz";
            options.Add(option);

            if (_resolutions[i].width == Screen.currentResolution.width
                && _resolutions[i].height == Screen.currentResolution.height
                && _resolutions[i].refreshRateRatio.Equals(Screen.currentResolution.refreshRateRatio))
            {
                currentResolutionIndex = i;
            }
        }

        ResolutionDropdown.AddOptions(options);
        ResolutionDropdown.value = currentResolutionIndex;
        ResolutionDropdown.RefreshShownValue();

        SetQualityDropdownOptions();

        PlayBackgroundMusic();

        AddButtonClickSoundEventsToAllCanvases();
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

        QualityDropdown.ClearOptions();
        QualityDropdown.AddOptions(options);
        QualityDropdown.value = currentQualityLevel;
        QualityDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = _resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreenMode, resolution.refreshRateRatio);
    }

    public void SetVolume(float volume)
    {
        AudioMixer1.SetFloat("Volume", volume);
        AudioMixer2.SetFloat("SideVolume", volume);
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
            SensitivitySlider.value = sensitivity;
        }
        else
        {
            SensitivitySlider.value = 1f;
        }
        SensitivitySlider.onValueChanged.AddListener(SetSensitivity);
    }

    private void PlayBackgroundMusic()
    {
        if (BackgroundMusicSource != null && BackgroundMusicClip != null)
        {
            BackgroundMusicSource.clip = BackgroundMusicClip;
            BackgroundMusicSource.loop = true;
            BackgroundMusicSource.Play();
        }
    }

    private void AddButtonClickSoundEventsToAllCanvases()
    {
        Canvas[] canvases = FindObjectsOfType<Canvas>(true);
        foreach (Canvas canvas in canvases)
        {
            AddButtonClickSoundEvents(canvas.gameObject);
        }
    }

    public void AddButtonClickSoundEvents(GameObject canvas)
    {
        Button[] buttons = canvas.GetComponentsInChildren<Button>(true);
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(PlayButtonClickSound);
        }
    }

    public void PlayButtonClickSound()
    {
        if (ButtonClickSource != null && ButtonClickClip != null)
        {
            ButtonClickSource.PlayOneShot(ButtonClickClip);
        }
    }

    private void OnEnable()
    {
        AddButtonClickSoundEvents(gameObject);
    }
}