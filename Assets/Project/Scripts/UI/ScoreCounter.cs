using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _score;
    private int _startScore = 0;

    public event Action<int> ScoreChanged;

    private void Awake()
    {
        Reset();
    }

    public void AddScore()
    {
        _score++;
        ScoreChanged?.Invoke(_score);
    }

    private void Reset()
    {
        _score = _startScore;
        ScoreChanged?.Invoke(_score);
    }
}
