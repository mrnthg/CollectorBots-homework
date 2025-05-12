using System;
using UnityEngine;

public abstract class Bot : PoolableObject
{
    private Vector3 _homePosition;

    public event Action<Bot> Removed;

    public Vector3 HomePosition => _homePosition;

    public void Remove()
    {
        Removed?.Invoke(this);
    }

    public void SetHome(Vector3 position)
    {
        _homePosition = position;
    }
}
