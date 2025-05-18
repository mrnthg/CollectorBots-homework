using System.Collections.Generic;
using UnityEngine;

public class TerrainScaner : MonoBehaviour
{
    [SerializeField] private float _radiusScan;
    [SerializeField] private LayerMask _scanLayer;

    private Vector3 _basePosition;
    private Collider[] _colliders;
    private int _maxCountScanResources = 20;
    private int _numColliders;

    private void Awake()
    {
        _colliders = new Collider[_maxCountScanResources];
        _basePosition = transform.position;      
    }

    public List<Resource> Scan()
    {
        List<Resource> resources = new List<Resource>();

        _numColliders = Physics.OverlapSphereNonAlloc(_basePosition, _radiusScan, _colliders, _scanLayer);

        for (int i = 0; i < _numColliders; i++)
        {
            Collider hit = _colliders[i];

            if (hit.TryGetComponent(out Resource resource) && hit.transform.parent.name == resource.StartNameParent)
            {
                resources.Add(resource);
            }
        }

        return resources;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radiusScan);
    }
}
