using TMPro;
using UnityEngine;

public class ResourceTypeUI : MonoBehaviour
{
    [SerializeField] private ResourceType _resourceType;
    [SerializeField] private TextMeshProUGUI _counterText;
    
    private int _currentValue = 0;

    public ResourceType ResourceType => _resourceType;

    public void SetValue(int value)
    {
        _currentValue = value;
    }

    public void AddValue()
    {
        _currentValue++;
    }

    public int GetValue()
    {
        return _currentValue;
    }

    public void SetCounterText(string value)
    {
        _counterText.text = value;
    }
}
