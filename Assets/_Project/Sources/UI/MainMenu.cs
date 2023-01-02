using UnityEngine;
using Zenject;

public class MainMenu : MonoBehaviour
{
    private Canvas _canvas;
    private Input _input;
    private bool _isFirstTouch = true;

    [Inject]
    private void Constructor(Input input)
    {
        _input = input;
    }

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    private void OnEnable()
    {
        EventsHolder.LevelStarted += Show;
        _input.Pressed += TryHide;
    }

    private void OnDisable()
    {
        EventsHolder.LevelStarted -= Show;
        _input.Pressed -= TryHide;
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
