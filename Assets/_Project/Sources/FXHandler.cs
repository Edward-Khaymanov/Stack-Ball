using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class FXHandler : MonoBehaviour
{
    [SerializeField] private AudioClip _platformBreak;
    [SerializeField] private AudioClip _victory;

    private AudioSource _audioSource;
    private Settings _settings;
    private Timer _resetPitchTimer;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
        _settings = Settings.Current;
        _resetPitchTimer = new Timer(this);
        ResetHandler();
    }

    private void OnEnable()
    {
        EventsHolder.PlatformDestroyed += OnDestroyPlatform;
        EventsHolder.LevelPassed += PlayWin;
        EventsHolder.LevelBuilding += ResetHandler;
    }

    private void OnDisable()
    {
        EventsHolder.PlatformDestroyed -= OnDestroyPlatform;
        EventsHolder.LevelPassed -= PlayWin;
        EventsHolder.Losed -= ResetHandler;

        _resetPitchTimer.Elapsed -= ResetPitch;
    }

    private void OnDestroyPlatform()
    {
        _resetPitchTimer.Stop();
        PlayDestroyPlatform();
        _resetPitchTimer.Start(TimeSpan.FromSeconds(3));
    }

    private void PlayWin()
    {
        if (_settings.AudioIsEnabled)
        {
            _audioSource.pitch = 0.8f;
            _audioSource.PlayOneShot(_victory);
        }

        if (_settings.VibrationIsEnabled)
            Vibration.Vibrate(50);

    }

    private void PlayDestroyPlatform()
    {
        if (_settings.AudioIsEnabled)
        {
            _audioSource.pitch += 0.01f;
            if (_audioSource.pitch >= 3f)
                ResetPitch();

            _audioSource.PlayOneShot(_platformBreak);
        }

        if (_settings.VibrationIsEnabled)
            Vibration.Vibrate(8);
    }

    private void ResetPitch()
    {
        _audioSource.pitch = 1;
    }

    private void ResetHandler()
    {
        ResetPitch();
        _resetPitchTimer.Stop();
        _resetPitchTimer = new Timer(this);
        _resetPitchTimer.Elapsed += ResetPitch;
    }
}
