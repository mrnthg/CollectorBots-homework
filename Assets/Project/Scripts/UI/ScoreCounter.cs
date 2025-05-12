using System;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    private int _value;
    private int _startValue = 0;

    public event Action<int> ScoreChanged;

    private void Start()
    {
        Reset();
    }

    public void AddValue()
    {
        _value++;
        ScoreChanged?.Invoke(_value);
    }

    private void Reset()
    {
        _value = _startValue;
        ScoreChanged?.Invoke(_value);
    }
}
