using System;

public abstract class Resource : PoolableObject, IResourcable
{
    private ResourceType _type;
    private string _startNameParent;

    public event Action<Resource> Removed;

    public string StartNameParent => _startNameParent;
    public ResourceType Type => _type;


    public void Remove()
    {
        Removed?.Invoke(this);
    }

    public void SetNameParent(string name)
    {
        _startNameParent = name;
    }

    public void SetResourceType(ResourceType type)
    {
        _type = type;
    }
}
