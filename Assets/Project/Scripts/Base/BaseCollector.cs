using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceHandler))]
public class BaseCollector : MonoBehaviour
{
    [SerializeField] private BotSpawner _botSpawner;

    private List<Bot> _collectorBots;
    private ResourceHandler _resourceHandler;

    private void Awake()
    {
        _collectorBots = new List<Bot>();
        _resourceHandler = GetComponent<ResourceHandler>();
    }

    private void OnEnable()
    {
        _botSpawner.Spawned += AddBot;
    }

    private void OnDisable()
    {
        _botSpawner.Spawned -= AddBot;
    }

    private void Start()
    {
        StartCoroutine(StartMining());
    }

    private void AddBot(Bot bot)
    {
        if (bot != null)
        {
            _collectorBots.Add(bot);
        }           
    }

    private void SetTarget(BotCollector bot)
    {       
        if (bot.TryGetComponent(out BotCollectorMover botCollectorMover) && bot.IsLoad == false && bot.IsMove == false)
        {
            if (_resourceHandler.GetCountResources() != 0)
            {
                Resource resource = _resourceHandler.FindClosestResource();
                ResourceProcessing(resource);
                botCollectorMover.StartMove(resource.transform);
            }
            else
            {
                botCollectorMover.GoHome(bot.HomePosition);
            }
        }
    }
    
    private IEnumerator StartMining()
    {
        while (enabled)
        {
            foreach (BotCollector botCollector in _collectorBots)
            {               
                SetTarget(botCollector);
            }

            yield return null;
        }        
    }

    private void ResourceProcessing(Resource resource)
    {
        resource.transform.SetParent(transform);
    }
}   
