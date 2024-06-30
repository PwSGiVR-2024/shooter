using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static SettingsManager Instance { get; private set; }

    private const string MASTER_VOLUME_KEY = "MasterVolume";
    private const string SFX_VOLUME_KEY = "SFXVolume";
    private const string MUSIC_VOLUME_KEY = "MusicVolume";
    private const string QUALITY_LEVEL_KEY = "QualityLevel";

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            LoadSettings();
        }
        else
        {
            Destroy(this);
        }
    }

    public void SetMasterVolume(float volume)
    {
        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.SetVolume("MasterVolume", volume);
            PlayerPrefs.SetFloat(MASTER_VOLUME_KEY, volume);
        }
        else
        {
            Debug.LogWarning("AudioManager.Instance is null. Cannot set Master Volume.");
        }
}

    public void SetSFXVolume(float volume)
    {
        AudioManager.Instance.SetVolume("SFXVolume", volume);
        PlayerPrefs.SetFloat(SFX_VOLUME_KEY, volume);
    }

    public void SetMusicVolume(float volume)
    {
        AudioManager.Instance.SetVolume("MusicVolume", volume);
        PlayerPrefs.SetFloat(MUSIC_VOLUME_KEY, volume);
    }

    public void SetQualityLevel(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        PlayerPrefs.SetInt(QUALITY_LEVEL_KEY, qualityIndex);
    }

    private void LoadSettings()
    {
        SetMasterVolume(PlayerPrefs.GetFloat(MASTER_VOLUME_KEY, 1f));
        SetSFXVolume(PlayerPrefs.GetFloat(SFX_VOLUME_KEY, 1f));
        SetMusicVolume(PlayerPrefs.GetFloat(MUSIC_VOLUME_KEY, 1f));
        SetQualityLevel(PlayerPrefs.GetInt(QUALITY_LEVEL_KEY, 2));
    }

    public void SaveSettings()
    {
        PlayerPrefs.Save();
    }


}