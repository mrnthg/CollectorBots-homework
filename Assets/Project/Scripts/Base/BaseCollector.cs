using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TransmitterClosestFoundResources))]
public class BaseCollector : MonoBehaviour
{
    [SerializeField] private BotSpawner _botSpawner;

    private List<Bot> _collectorBots;
    private TransmitterClosestFoundResources _transmitterClosestFoundResources;

    public BotSpawner BotSpawner => _botSpawner;

    private void Awake()
    {
        _collectorBots = new List<Bot>();
        _transmitterClosestFoundResources = GetComponent<TransmitterClosestFoundResources>();
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
            if (_transmitterClosestFoundResources.GetCountResources() != 0)
            {
                Resource resource = _transmitterClosestFoundResources.FindClosestResource();
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
