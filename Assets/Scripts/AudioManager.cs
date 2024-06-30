using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [SerializeField] private AudioMixerGroup _sfxMixerGroup;
    [SerializeField] private AudioMixerGroup _musicMixerGroup;
    [SerializeField] private int _numAudioSources = 5;

    private Queue<AudioSource> _availableSfxSources = new Queue<AudioSource>();
    private AudioSource _musicSource;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeAudioSources();
        }
        else
        {
            Destroy(this);
        }
    }

    private void InitializeAudioSources()
    {
        for (int i = 0; i < _numAudioSources; i++)
        {
            GameObject audioSourceObj = new GameObject($"SFX_Source_{i}");
            audioSourceObj.transform.SetParent(this.transform);
            AudioSource sfxSource = audioSourceObj.AddComponent<AudioSource>();
            sfxSource.outputAudioMixerGroup = _sfxMixerGroup;
            sfxSource.playOnAwake = false;
            _availableSfxSources.Enqueue(sfxSource);
        }

        GameObject musicSourceObj = new GameObject("Music_Source");
        musicSourceObj.transform.SetParent(this.transform);
        _musicSource = musicSourceObj.AddComponent<AudioSource>();
        _musicSource.outputAudioMixerGroup = _musicMixerGroup;
        _musicSource.loop = true;
    }

    public void PlaySound(AudioClip clip, Vector3 position)
    {
        if (_availableSfxSources.Count == 0)
        {
            Debug.LogWarning("No available AudioSources!");
            return;
        }

        AudioSource audioSource = _availableSfxSources.Dequeue();
        audioSource.transform.position = position;
        audioSource.clip = clip;
        audioSource.Play();

        StartCoroutine(ReturnAudioSourceWhenFinished(audioSource));
    }

    public void PlaySound(AudioClip clip)
    {
        PlaySound(clip, Vector3.zero);
    }

    public void PlayMusic(AudioClip musicClip)
    {
        _musicSource.clip = musicClip;
        _musicSource.Play();
    }

    public void SetVolume(string parameterName, float volumePercent)
    {
        float volumeValue = Mathf.Log10(volumePercent) * 20;
        _sfxMixerGroup.audioMixer.SetFloat(parameterName, volumeValue);
    }

    private IEnumerator ReturnAudioSourceWhenFinished(AudioSource audioSource)
    {
        yield return new WaitForSeconds(audioSource.clip.length);
        audioSource.transform.position = Vector3.zero;
        _availableSfxSources.Enqueue(audioSource);
    }
}