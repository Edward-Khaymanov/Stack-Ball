using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public abstract class Screen : MonoBehaviour
{
    protected Canvas _canvas;

    protected virtual void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    protected void Show()
    {
        _canvas.enabled = true;
    }

    protected void Hide()
    {
        _canvas.enabled = false;

    }
}
