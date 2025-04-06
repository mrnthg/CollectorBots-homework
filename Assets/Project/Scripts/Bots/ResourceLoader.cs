using System;
using UnityEngine;

[RequireComponent(typeof(BotCollisonHandler), typeof(BotCollector))]
public class ResourceLoader : MonoBehaviour
{
    [SerializeField] private Transform _cargoPosition;

    private Resource _transportableResource;
    private BotCollisonHandler _botCollisonHandler;
    private BotCollector _botCollector;

    public event Action Loaded;
    public event Action Unloaded;

    private void Awake()
    {
        _botCollisonHandler = GetComponent<BotCollisonHandler>();
        _botCollector = GetComponent<BotCollector>();
    }

    private void OnEnable()
    {
        _botCollisonHandler.CollisionDetected += LoadProcess;
    }

    private void OnDisable()
    {
        _botCollisonHandler.CollisionDetected -= LoadProcess;
    }

    private void LoadProcess(Resource resource)
    {
        if (resource is Iridium && _botCollector.IsLoad == false)
        {           
            if (resource.TryGetComponent(out Transform transform))
            {
                Loaded?.Invoke();
                _transportableResource = resource;
                _transportableResource.transform.position = _cargoPosition.position;
                _transportableResource.transform.SetParent(this.transform);
            }
        }
    }

    public Resource UnloadProcess()
    {
        Unloaded?.Invoke();
        return _transportableResource;
    }
}
