using System;
using UnityEngine;

public class ResourceHarvester : MonoBehaviour
{
    public event Action ResourceReceived;

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
            Debug.Log("������� ������");
            
            resource = bot.Unload();         
            resource.Remove(); 
            
            ResourceReceived?.Invoke();
        }
    }
}
