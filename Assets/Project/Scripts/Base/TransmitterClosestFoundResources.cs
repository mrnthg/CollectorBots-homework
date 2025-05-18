using System.Collections.Generic;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(TerrainScaner))]
public class TransmitterClosestFoundResources : MonoBehaviour
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

    private void Start()
    {
        StartCoroutine(FindResources());;
    }

    public Resource FindClosestResource()
    {
        Resource target = null;
        float distance = Mathf.Infinity;

        foreach (Resource resource in _handleResources)
        {
            Vector3 difference = resource.transform.position - _basePosition;
            float sqrDifference = difference.sqrMagnitude;

            if (sqrDifference < distance)
            {
                target = resource;
                distance = sqrDifference;
            }
        }

        _handleResources.Remove(target);

        return target;
    }

    public int GetCountResources()
    {
        return _handleResources.Count;
    }

    private void AddResources()
    {
        _handleResources.Clear();
        _handleResources = _terrainScaner.Scan();
    }

    private IEnumerator FindResources()
    {
        while (enabled)
        {
            yield return _duration;
            AddResources();
        }
    }
}