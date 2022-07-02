using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance = null;
    public static SoundManager Instance { get => _instance; }

    [SerializeField] private AudioSource _effectAudioSource = null;
    [SerializeField] private AudioSource _musicAudioSource = null;

    private void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
        }
        else if (_instance != this)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayEffectClip(AudioClip clip, float startTime = 0f)
    {
        if (clip != null && _effectAudioSource != null)
        {
            _effectAudioSource.clip = clip;
            _effectAudioSource.time = startTime;
            _effectAudioSource.Play();
        }
    }

    public void PlayMusicClip(AudioClip clip, float startTime = 0f)
    {
        if (clip != null && _musicAudioSource != null)
        {
            _musicAudioSource.clip = clip;
            _effectAudioSource.time = startTime;
            _musicAudioSource.Play();
        }
    }
}
