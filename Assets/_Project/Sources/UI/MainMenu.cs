using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviour
{
    private BallInput _ballInput;
    private Canvas _canvas;
    private bool _isFirstTouch = true;

    [Inject]
    private void Constructor(BallInput ballInput)
    {
        _ballInput = ballInput;
    }

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        EventsHolder.LevelStarted += Show;
        _ballInput.Pressed += TryHide;
    }

    private void OnDisable()
    {
        EventsHolder.LevelStarted -= Show;
        _ballInput.Pressed -= TryHide;
    }

    private void Show()
    {
        _canvas.enabled = true;
        _isFirstTouch = true;
    }

    private void TryHide()
    {
        if (_isFirstTouch)
        {
            _canvas.enabled = false;
            _isFirstTouch = false;
        }
    }
}
