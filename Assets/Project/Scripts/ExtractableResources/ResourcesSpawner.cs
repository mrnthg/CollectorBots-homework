using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spawners;

public class ResourcesSpawner : Spawner<Resource>
{
    [SerializeField] private Collider _spawnZone;
    [SerializeField] private int _maxResourceCount;

    private Bounds _spawnBounds;
    private List<Resource> _resources = new List<Resource>();
    private float _durationSpawn = 15f;
    private WaitForSeconds _duration;

    private void OnEnable()
    {
        _duration = new WaitForSeconds(_durationSpawn);
    }

    private void Start()
    {
        CreateAllResources();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            _resources[0].Remove();
        }
    }

    public override void PerformOnGet(Resource resource)
    {
        _resources.Add(resource);
       
        resource.gameObject.SetActive(true);
        resource.SetNameParent(Parent.name);
        resource.transform.position = GetPositionSpawn();
        resource.Removed += RemoveObject;
    }

    public override void OnRelease(Resource resource)
    {
        _resources.RemoveAt(0);

        resource.gameObject.SetActive(false);
        resource.transform.SetParent(Parent);
        StartCoroutine(CreateResource());
        resource.Removed -= RemoveObject;
    }

    private Vector3 GetPositionSpawn()
    {
        _spawnBounds = _spawnZone.bounds;

        float posX = Random.Range(_spawnBounds.min.x, _spawnBounds.max.x);
        float posZ = Random.Range(_spawnBounds.min.z, _spawnBounds.max.z);

        return new Vector3(posX, -1.2f, posZ);
    }

    private void CreateAllResources()
    {
        for (int i = 0; i < _maxResourceCount; i++)
        {
            GetObject();
        }
    }

    private IEnumerator CreateResource()
    {
        while (_resources.Count < _maxResourceCount)
        {
            yield return _duration;
            
            if (_resources.Count < _maxResourceCount)
            {                
                GetObject();
            }
        }
    } 
}
