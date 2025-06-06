using System;
using UnityEngine;

public class ResourceÑollisionHandler : MonoBehaviour
{
    public event Action<Resource> ResourceCollisionDetected;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Resource resource))
        {
            ResourceCollisionDetected?.Invoke(resource);
        }
    }
}
