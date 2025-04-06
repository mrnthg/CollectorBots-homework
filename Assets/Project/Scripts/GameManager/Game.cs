using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private ResourceHarvester _resourceHarvester;
    [SerializeField] private ScoreCounter _scoreCounter;

    private void OnEnable()
    {
        _resourceHarvester.ResourceReceived += _scoreCounter.AddScore;
    }

    private void OnDisable()
    {
        _resourceHarvester.ResourceReceived -= _scoreCounter.AddScore;
    }
}
