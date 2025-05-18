using UnityEngine;
using System.Collections.Generic;

public class ResourceScoreCounter : MonoBehaviour
{
    [SerializeField] private List<ResourceTypeUI> _resourceUIs = new List<ResourceTypeUI>();

    private int _startValue = 0;
    private Dictionary<ResourceType, ResourceTypeUI> _resourcesDictionary = new Dictionary<ResourceType, ResourceTypeUI>();

    private void Start()
    {
        foreach (ResourceTypeUI typeUI in _resourceUIs)
        {
            _resourcesDictionary.Add(typeUI.ResourceType, typeUI);
            typeUI.SetValue(_startValue);
            UpdateUI(typeUI.ResourceType);
        }
    }

    public void AddResource(Resource resource)
    {
        ResourceType type = resource.Type;

        if (_resourcesDictionary.TryGetValue(type, out ResourceTypeUI resourceUI))
        {
            resourceUI.AddValue();
            UpdateUI(type);
        }
    }

    private void UpdateUI(ResourceType type)
    {
        if (_resourcesDictionary.TryGetValue(type, out ResourceTypeUI resourceUI))
        {
            resourceUI.SetCounterText(resourceUI.GetValue().ToString());
        }
    } 
}
