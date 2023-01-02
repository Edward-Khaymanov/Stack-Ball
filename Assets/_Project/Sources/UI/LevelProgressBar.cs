using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelProgressBar : MonoBehaviour
{
    [SerializeField] private Image _background;
    [SerializeField] private Image _filler;
    [SerializeField] private TMP_Text _currentLevelText;
    [SerializeField] private TMP_Text _nextLevelText;

    private int _currentBrokenPlatforms;

    public void Render(Color32 color, int level)
    {
        _filler.color = color;
        _background.color = color;
        _currentLevelText.text = $"{level}";
        _nextLevelText.text = $"{level + 1}";

        ResetProgress();
    }

    public void Fill(int platformsAmount)
    {
        _currentBrokenPlatforms++;
        var progress = (float)_currentBrokenPlatforms / platformsAmount;
        _filler.fillAmount = Mathf.Clamp01(progress);
    }

    private void ResetProgress()
    {
        _currentBrokenPlatforms = 0;
        _filler.fillAmount = Mathf.Clamp01(0);
    }
}
