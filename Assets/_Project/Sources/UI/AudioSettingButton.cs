using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AudioSettingButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Sprite _enabledIcon;
    [SerializeField] private Sprite _disabledIcon;

    private Image _icon;
    private Settings _settings;

    private void Awake()
    {
        _settings = Settings.Current;
        _icon = GetComponent<Image>();
    }

    private void Start()
    {
        if (_settings.AudioIsEnabled)
            EnableSound();
        else
            DisableSound();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_settings.AudioIsEnabled)
            DisableSound();
        else
            EnableSound();
    }

    private void EnableSound()
    {
        _settings.AudioIsEnabled = true;
        _icon.sprite = _enabledIcon;
        _settings.Save();
    }

    private void DisableSound()
    {
        _settings.AudioIsEnabled = false;
        _icon.sprite = _disabledIcon;
        _settings.Save();
    }
}
