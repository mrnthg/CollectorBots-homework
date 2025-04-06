using System;
using UnityEngine;

public abstract class Bot : PoolableObject
{
    private Vector3 _homeTransform;

    public event Action<Bot> Removed;

    public Vector3 HomeTransform => _homeTransform;

    public void Remove()
    {
        Removed?.Invoke(this);
    }

    public void SetHome(Vector3 position)
    {
        _homeTransform = position;
    }
}
