using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallVFX : MonoBehaviour
{
    [SerializeField] private ParticleSystem _fire;
    [SerializeField] private ParticleSystem _invincibilityActivation;
    [SerializeField] private TrailRenderer _trail;
    [SerializeField] private Footprint _footprint;

    public void RenderTrail(BallSkin skin, Color32 color)
    {
        if (skin.UseMaterialColor)
        {
            var materialColor = skin.Material.color;
            materialColor.a = _trail.startColor.a;
            _trail.startColor = materialColor;
        }
        else
        {
            var trailAlpha = (byte)(_trail.startColor.a * 255.0);
            _trail.startColor = new Color32(color.r, color.g, color.b, trailAlpha);
        }
    }

    public void SetFootprintColor(Color32 color)
    {
        _footprint.SetColor(color);
    }

    public void LeaveFootprint(Vector3 surfacePoint, Transform container)
    {
        _footprint.LeaveFootprint(surfacePoint, container);
    }

    public void PlayFire()
    {
        _fire.Play();
    }

    public void StopFire()
    {
        _fire.Stop();
    }

    public void PlayInvincibilityActivation()
    {
        _invincibilityActivation.Play();
    }

    public void StopInvincibilityActivation()
    {
        _invincibilityActivation.Stop();
    }

    public void ShowTrail()
    {
        _trail.enabled = true;
    }

    public void HideTrail()
    {
        _trail.enabled = false;
    }
}
