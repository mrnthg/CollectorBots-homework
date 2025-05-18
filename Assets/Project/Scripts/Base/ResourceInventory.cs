using UnityEngine;

public class ResourceInventory : MonoBehaviour
{
    [SerializeField] private ResourceHarvester _resourceHarvester;
    [SerializeField] private ResourceScoreCounter _scoreCounter;   

    private void OnEnable()
    {
        _resourceHarvester.ResourceReceived += _scoreCounter.AddResource;
    }

    private void OnDisable()
    {
        _resourceHarvester.ResourceReceived -= _scoreCounter.AddResource;
    }
}
