using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class BallInvincibility : MonoBehaviour
{
    [SerializeField] private float _secondsPerPlatform = 0.15f;
    [SerializeField] private int _invincibleSeconds = 3;
    [SerializeField] private int _platformsToEnableIndicator = 10;
    [SerializeField] private int _secondsToEnableInvincible = 4;

    private BallInput _input;
    private BallInvincibilityIndicator _indicator;
    private Timer _timer;
    private int _currentDestroyedPlatform;

    public event Action Enabled;
    public event Action Disabled;

    public bool IsInvincible { get; private set; }

    [Inject]
    private void Constructor(BallInput input, BallInvincibilityIndicator indicator)
    {
        _input = input;
        _indicator = indicator;
    }

    private void Start()
    {
        _timer = new Timer(this);
        ResetInvincibility();
    }

    private void OnValidate()
    {
        if (_secondsPerPlatform <= 0)
            _secondsPerPlatform = 0.15f;

        if (_invincibleSeconds <= 0)
            _invincibleSeconds = 3;

        if (_platformsToEnableIndicator <= 0)
            _platformsToEnableIndicator = 10;

        if (_secondsToEnableInvincible <= 0)
            _secondsToEnableInvincible = 4;
    }

    private void OnEnable()
    {
        EventsHolder.PlatformDestroyed += OnPlatformDestroyed;
        EventsHolder.LevelPassed += ResetInvincibility;
        EventsHolder.Losed += ResetInvincibility;

        _input.Unpressed += TryResumeCountdown;
    }

    private void OnDisable()
    {
        EventsHolder.PlatformDestroyed -= OnPlatformDestroyed;
        EventsHolder.LevelPassed -= ResetInvincibility;
        EventsHolder.Losed -= ResetInvincibility;

        _timer.Tick -= UpdateIndicator;
        _timer.Elapsed -= OnTimerElapsed;

        _input.Unpressed -= TryResumeCountdown;
    }

    private void EnableInvincible()
    {
        IsInvincible = true;
        _indicator.ChangeFillerColor(Color.red);
        _timer.Start(TimeSpan.FromSeconds(_invincibleSeconds));
        Enabled?.Invoke();
    }

    private void TryDisableInvicible()
    {
        if (IsInvincible)
        {
            IsInvincible = false;
            _indicator.ChangeFillerColor(Color.white);
            Disabled?.Invoke();
        }
    }

    private void TryResumeCountdown()
    {
        if (_currentDestroyedPlatform < _platformsToEnableIndicator || _timer.RemainingSeconds <= 0 || IsInvincible)
            return;

        _timer.Start(_timer.RemainingTime);
    }

    private void ResetInvincibility()
    {
        IsInvincible = false;
        _currentDestroyedPlatform = 0;
        _timer.Stop();
        _timer = new Timer(this);
        _timer.Elapsed += OnTimerElapsed;
        _timer.Tick += UpdateIndicator;
        _indicator.ChangeFillerColor(Color.white);
        _indicator.Fill(0);
        _indicator.Hide();
        Disabled?.Invoke();
    }

    private void OnPlatformDestroyed()
    {
        if (IsInvincible)
            return;

        _currentDestroyedPlatform++;
        if (_currentDestroyedPlatform < _platformsToEnableIndicator)
            return;

        _timer.Stop();

        var nextTimerSeconds = _timer.RemainingSeconds + _secondsPerPlatform;
        if (nextTimerSeconds >= _secondsToEnableInvincible)
        {
            EnableInvincible();
            return;
        }

        var fillAmount = _timer.RemainingSeconds / _secondsToEnableInvincible;
        _indicator.Fill(fillAmount);
        _indicator.Show();
        _timer.AddTime(TimeSpan.FromSeconds(_secondsPerPlatform));
    }

    private void UpdateIndicator()
    {
        if (_timer.RemainingSeconds < 0)
            return;

        var fillAmount = _timer.RemainingSeconds / _secondsToEnableInvincible;

        if (IsInvincible)
            fillAmount = _timer.RemainingSeconds / _invincibleSeconds;

        _indicator.Fill(fillAmount);
        _indicator.Show();
    }

    private void OnTimerElapsed()
    {
        _currentDestroyedPlatform = 0;
        _indicator.Hide();
        _indicator.Fill(0);
        TryDisableInvicible();
    }
}
