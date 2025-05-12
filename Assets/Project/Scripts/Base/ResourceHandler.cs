using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TerrainScaner))]
public class ResourceHandler : MonoBehaviour
{
    [SerializeField] private ResourceHarvester _harvester;

    private TerrainScaner _terrainScaner;
    private List<Resource> _handleResources;
    private Vector3 _basePosition;
    private float _cooldown = 3.5f;
    private WaitForSeconds _duration;

    private void Awake()
    {       
        _handleResources = new List<Resource>();
        _duration = new WaitForSeconds(_cooldown);
        _basePosition = transform.position;       
        _terrainScaner = GetComponent<TerrainScaner>();
    }

    private void OnEnable()
    {
        _harvester.ResourceReceived += AddResources;
    }

    private void OnDisable()
    {
        _harvester.ResourceReceived -= AddResources;
    }

    private void Start()
    {
        AddResources();
    }

    public Resource FindClosestResource()
    {
        Resource target = null;
        float distance = Mathf.Infinity;

        foreach (Resource resource in _handleResources)
        {
            Vector3 difference = resource.transform.position - _basePosition;
            float SqrDifference = difference.sqrMagnitude;

            if (SqrDifference < distance)
            {
                target = resource;
                distance = SqrDifference;
            }
        }

        _handleResources.Remove(target);

        return target;
    }

    public int GetCountResources()
    {
        return _handleResources.Count;
    }

    public void AddResources()
    {
        _handleResources.Clear();
        _handleResources = _terrainScaner.Scan();
    }
}