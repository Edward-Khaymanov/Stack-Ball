using UnityEngine;
using UnityEngine.UI;

public class BallInvincibilityIndicator : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _filler;

    private Canvas _canvas;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    public void Show()
    {
        _canvas.enabled = true;
    }

    public void Hide()
    {
        _canvas.enabled = false;
    }

    public void Fill(float amount)
    {
        _filler.fillAmount = amount;
    }

    public void ChangeFillerColor(Color32 color)
    {
        _filler.color = color;
    }
}
