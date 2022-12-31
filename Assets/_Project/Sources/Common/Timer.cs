using System;
using System.Collections;
using UnityEngine;

public class Timer
{
    private MonoBehaviour _invoker;
    private IEnumerator _timer;
    private float _remainingSeconds;

    public event Action Elapsed;
    public event Action Tick;

    public Timer(MonoBehaviour invoker)
    {
        _invoker = invoker;
    }

    public float RemainingSeconds => _remainingSeconds;
    public TimeSpan RemainingTime => TimeSpan.FromSeconds(_remainingSeconds);

    public void Start(TimeSpan time)
    {
        Stop();
        _remainingSeconds = (float)time.TotalSeconds;
        _timer = Countdown();
        _invoker.StartCoroutine(_timer);
    }

    public void Stop()
    {
        if (_timer == null)
            return;

        _invoker.StopCoroutine(_timer);
    }

    public void AddTime(TimeSpan time)
    {
        _remainingSeconds += (float)time.TotalSeconds;
    }

    private IEnumerator Countdown()
    {
        while (_remainingSeconds > 0)
        {
            yield return null;
            _remainingSeconds -= Time.deltaTime;
            Tick?.Invoke();
        }

        _remainingSeconds = 0;
        Elapsed?.Invoke();
    }
}