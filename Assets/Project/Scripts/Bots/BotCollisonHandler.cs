using System;
using UnityEngine;

public class BotCollisonHandler : MonoBehaviour
{
    public event Action<Resource> CollisionDetected;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Resource resource))
        {
            CollisionDetected?.Invoke(resource);
        }
    }
}
