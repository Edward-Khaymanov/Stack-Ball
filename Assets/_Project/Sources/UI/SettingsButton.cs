using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SettingsButton : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private Image _vibrationButton;
    [SerializeField] private Image _audioButton;

    public void OnPointerDown(PointerEventData eventData)
    {
        _vibrationButton.enabled = !_vibrationButton.enabled;
        _audioButton.enabled = !_audioButton.enabled;
    }
}
