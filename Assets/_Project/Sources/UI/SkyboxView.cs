using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class SkyboxView : MonoBehaviour
{
    private Material _material;
    private const string BOTTOM_COLOR = "_BottomColor";
    private const string TOP_COLOR = "_TopColor";

    private void Awake()
    {
        var mat = Instantiate(RenderSettings.skybox);
        _material = mat;
        RenderSettings.skybox = _material;
    }

    public void Render(Gradient gradient)
    {
        _material.SetColor(TOP_COLOR, gradient.colorKeys[0].color);
        _material.SetColor(BOTTOM_COLOR, gradient.colorKeys[1].color);
    }
}
