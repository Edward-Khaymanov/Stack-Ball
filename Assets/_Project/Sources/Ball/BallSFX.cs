using UnityEngine;

public class BallSFX : MonoBehaviour
{
    [SerializeField] private AudioClip _bounce;
    [SerializeField] private AudioClip _breakBall;
    [SerializeField] private AudioClip _invinciblityActivation;

    private AudioSource _source;
    private Settings _settings;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
        _settings = Settings.Current;
    }

    public void PlayInvincible()
    {
        if (_settings.AudioIsEnabled)
            _source.PlayOneShot(_invinciblityActivation);
    }

    public void PlayBreakBall()
    {
        if (_settings.AudioIsEnabled)
            _source.PlayOneShot(_breakBall);

        if (_settings.VibrationIsEnabled)
            Vibration.Vibrate(50);
    }

    public void PlayBounce()
    {
        if (_settings.AudioIsEnabled)
            _source.PlayOneShot(_bounce);

        if (_settings.VibrationIsEnabled)
            Vibration.Vibrate(20);
    }
}
