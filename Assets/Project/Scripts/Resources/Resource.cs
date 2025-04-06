using System;

public abstract class Resource : PoolableObject, IResourceable
{
    public event Action<Resource> Removed;

    public void Remove()
    {
        Removed?.Invoke(this);
    }
}
