using System.Collections.Generic;
using UnityEngine;

public class TerrainScaner : MonoBehaviour
{
    [SerializeField] private float _radiusScan;

    private Vector3 _basePosition;

    private void Awake()
    {
        _basePosition = transform.position;
    }

    public List<Resource> Scan()
    {
        Collider[] colliders = Physics.OverlapSphere(_basePosition, _radiusScan);
        List<Resource> _resources = new List<Resource>();

        foreach (Collider hit in colliders)
        {
            if (hit.TryGetComponent(out Resource resource) && hit.transform.parent.name == resource.StartNameParent)
            {
                _resources.Add(resource);
            }
        }

        return _resources;
    }
}
