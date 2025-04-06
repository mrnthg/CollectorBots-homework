using System;
using UnityEngine;
using UnityEngine.Pool;

namespace Spawners
{
    public abstract class Spawner<T> : MonoBehaviour where T : PoolableObject
    {
        [SerializeField] private T _prefab;
        [SerializeField] private Transform _parent;

        private ObjectPool<T> _pool;
        private int _defaultSize = 10;
        private int _maxSize = 100;

        public event Action<T> Spawned;

        public Transform Parent => _parent;

        private void Awake()
        {
            _pool = new ObjectPool<T>(CreateObject, PerformOnGet, OnRelease, Destroy, true, _defaultSize, _maxSize);
        }

        public T CreateObject()
        {
            T newObject = Instantiate(_prefab);
            newObject.transform.SetParent(_parent);
            Spawned?.Invoke(newObject);
            return newObject;
        }

        public abstract void PerformOnGet(T newObject);

        public abstract void OnRelease(T newObject);

        protected void GetObject()
        {
            _pool.Get();
        }

        protected void RemoveObject(T newObject)
        {
            _pool.Release(newObject);
        }
    }
}

