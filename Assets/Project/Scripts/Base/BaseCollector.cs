using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ResourceHandler))]
public class BaseCollector : MonoBehaviour
{
    [SerializeField] private BotSpawner _botSpawner;

    private List<Bot> _collectorBots;
    private ResourceHandler _resourceHandler;
    private BotCollectorMover _botCollector = null;

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
        if (bot.TryGetComponent(out BotCollectorMover botCollectorMover))
        {
            _botCollector = botCollectorMover;
        }

        if (bot.IsLoad == false && bot.IsMove == false && _botCollector != null)
        {
            if (_resourceHandler.HandleResources.Count != 0)
            {
                _botCollector.StartMove(_resourceHandler.FindClosestResource().transform);
            }
            else
            {
                _botCollector.GoHome();
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
}   
