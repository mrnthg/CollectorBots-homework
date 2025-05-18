using System;
using UnityEngine;

[RequireComponent(typeof(Resource—ollisionHandler), typeof(BotCollector))]
public class ResourceLoader : MonoBehaviour
{
    [SerializeField] private Transform _cargoPosition;

    private Resource _transportableResource;
    private BotCollector _botCollector;

    private void Awake()
    {
        _botCollector = GetComponent<BotCollector>();
    }

    public bool LoadProcess(Resource resource)
    {
        bool isLoad = false;

        if (resource is IResourcable && _botCollector.IsLoad == false)
        {
            _transportableResource = resource;
            _transportableResource.transform.position = _cargoPosition.position;
            _transportableResource.transform.SetParent(this.transform);

            isLoad = true;
        }

        return isLoad;
    }

    public Resource UnloadProcess()
    {
        return _transportableResource;
    }
}
