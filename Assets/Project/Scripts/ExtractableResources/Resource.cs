using System;

public abstract class Resource : PoolableObject, IResourceable
{
    private string _startNameParent;

    public event Action<Resource> Removed;

    public string StartNameParent => _startNameParent;

    public void Remove()
    {
        Removed?.Invoke(this);
    }

    public void SetNameParent(string name)
    {
        _startNameParent = name;
    }
}
