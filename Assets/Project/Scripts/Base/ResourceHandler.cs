using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TerrainScaner))]
public class ResourceHandler : MonoBehaviour
{
    private TerrainScaner _terrainScaner;
    private List<Resource> _handleResources;
    private Vector3 _basePosition;
    private float _cooldown = 3.5f;
    private WaitForSeconds _duration;

    public List<Resource> HandleResources => _handleResources;

    private void Awake()
    {
        _handleResources = new List<Resource>();
        _duration = new WaitForSeconds(_cooldown);
        _basePosition = transform.position;       
        _terrainScaner = GetComponent<TerrainScaner>();                 
    }

    private void Start()
    {
        StartCoroutine(StartHandler());
    }

    public Resource FindClosestResource()
    {
        Resource target = null;
        float distance = Mathf.Infinity;

        foreach (Resource resource in _handleResources)
        {
            Vector3 difference = resource.transform.position - _basePosition;
            float currentDistance = difference.sqrMagnitude;

            if (currentDistance < distance)
            {
                target = resource;
                distance = currentDistance;
            }
        }

        _handleResources.Remove(target);

        return target;
    }

    private void AddResources()
    {
        _handleResources.Clear();
        _handleResources = _terrainScaner.Scan();
    }

    private IEnumerator StartHandler()
    {
        while (enabled)
        {
            AddResources();
            yield return _duration;
        }
    }
}