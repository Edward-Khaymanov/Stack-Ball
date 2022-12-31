using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class Score
{
    [SerializeField] private int _currentScore;
    [SerializeField] private int _bestScore;

    public int CurrentScore => _currentScore;
    public int BestScore => _bestScore;

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
