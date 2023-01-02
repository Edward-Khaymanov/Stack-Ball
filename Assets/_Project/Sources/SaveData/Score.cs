using System;
using UnityEngine;

[Serializable]
public class Score
{
    [SerializeField] private int _bestScore;
    [SerializeField] private int _currentScore;

    public int BestScore => _bestScore;
    public int CurrentScore => _currentScore;

    public void AddScorePoints(int amount)
    {
        if (amount > 0)
            _currentScore += amount;
    }

    public void ResetCurrentScore()
    {
        _currentScore = 0;
    }

    public void TrySetBestScore()
    {
        if (_bestScore < _currentScore)
            _bestScore = _currentScore;
    }
}
