using System;
using UnityEngine;

public class ResourceHarvester : MonoBehaviour
{
    [SerializeField] private BaseCollector _baseCollector;

    public event Action<Resource> ResourceReceived;
    public event Action CollectionCompleted;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BotCollector bot))
        {
            if (bot.transform.parent == GetSpawnerBot())
            {
                TakeBotResource(bot);
            }           
        }
    }

    private Transform GetSpawnerBot()
    {
        return _baseCollector.BotSpawner.transform;
    }

    private void TakeBotResource(BotCollector bot)
    {
        Resource resource;

        if (bot.IsLoad)
        {            
            resource = bot.Unload();
            ResourceReceived?.Invoke(resource);
            resource.Remove();

            CollectionCompleted?.Invoke();
        }
    }
}
