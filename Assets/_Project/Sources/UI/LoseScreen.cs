using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class LoseScreen : Screen, IPointerDownHandler
{
    [SerializeField] private float _countDuration;
    [SerializeField] private AnimationCurve _countPattern;
    [SerializeField] private TMP_Text _currentScore;
    [SerializeField] private TMP_Text _bestScore;

    private IEnumerator _countCoroutine;

    public void Render(int score, int bestScore)
    {
        _bestScore.text = $"BEST: \n {bestScore}";
        _countCoroutine = AnimationExtention.StartCount(0, score, _countDuration, _countPattern, RenderScore);
        Show();
        StartCoroutine(_countCoroutine);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        EventsHolder.BuildLevel();
        Hide();
        StopCoroutine(_countCoroutine);
        RenderScore(0);
    }

    private void RenderScore(int score)
    {
        _currentScore.text = $"SCORE: \n {score}";
    }
}
