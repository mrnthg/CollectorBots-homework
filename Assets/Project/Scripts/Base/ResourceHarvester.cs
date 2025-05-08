using System;
using UnityEngine;

public class ResourceHarvester : MonoBehaviour
{
    public Action ResourceReceived;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out BotCollector bot))
        {
            TakeBotResource(bot);
        }
    }

    private void TakeBotResource(BotCollector bot)
    {
        Resource resource;

        if (bot.IsLoad)
        {
            resource = bot.Unload();
            ResourceReceived?.Invoke();
            resource.Remove();
        }
    }
}
