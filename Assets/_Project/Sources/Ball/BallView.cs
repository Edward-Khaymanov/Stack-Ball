using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class BallView : MonoBehaviour
{
    private MeshFilter _meshFilter;
    private MeshRenderer _meshRenderer;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshRenderer = GetComponent<MeshRenderer>();
    }

    public void Show()
    {
        _meshRenderer.enabled = true;
    }

    public void Hide()
    {
        _meshRenderer.enabled = false;
    }

    public void Render(BallSkin skin, Color32 color)
    {
        _meshFilter.mesh = skin.Skin;
        _meshRenderer.material = skin.Material;

        if (skin.UseMaterialColor)
        {
            _meshRenderer.material.color = skin.Material.color;
            return;
        }

        _meshRenderer.material.color = color;
    }
}
