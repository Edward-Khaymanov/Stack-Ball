using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class Input : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public event Action Pressed;
    public event Action Unpressed;

    public bool IsPressed { get; private set; }

    private void OnDisable()
    {
        IsPressed = false;
        Unpressed?.Invoke();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;
        Pressed?.Invoke();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
        Unpressed?.Invoke();
    }
}
