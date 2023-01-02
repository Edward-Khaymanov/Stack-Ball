using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class VibrationSettingButton : MonoBehaviour, IPointerDownHandler
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
        if (_settings.VibrationIsEnabled)
            EnableVibration();
        else
            DisableVibration();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (_settings.VibrationIsEnabled)
            DisableVibration();
        else
            EnableVibration();
    }

    private void EnableVibration()
    {
        _settings.VibrationIsEnabled = true;
        _icon.sprite = _enabledIcon;
        _settings.Save();
    }

    private void DisableVibration()
    {
        _settings.VibrationIsEnabled = false;
        _icon.sprite = _disabledIcon;
        _settings.Save();
    }
}
