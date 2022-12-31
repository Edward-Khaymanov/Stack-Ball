using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishPlatform : MonoBehaviour
{
    [SerializeField] private ParticleSystem _confetti;

    private bool _isFinished;

    private void OnEnable()
    {
        EventsHolder.LevelStarted += ResetFinish;
    }

    private void OnDisable()
    {
        EventsHolder.LevelStarted += ResetFinish;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (_isFinished == false)
        {
            _isFinished = true;
            EventsHolder.PassLevel();
            _confetti.Play();
        }
    }

    private void ResetFinish()
    {
        _isFinished = false;
    }
}
