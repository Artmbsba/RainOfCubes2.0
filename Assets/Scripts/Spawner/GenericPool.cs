using System;
using UnityEngine;
using UnityEngine.Pool;

public class GenericPool<T> where T : Component
{
    private ObjectPool<T> _pool;
    private T _prefab;

    private int _allObjects;
    private int _activeObjectCounter;
    private int _createdObjects;

    public event Action<int> ActiveCountChanged;
    public event Action<int> AllCountChanged;
    public event Action<int> CreatedCountChanged;
      
    public GenericPool(T prefab, int capacity, int maxSize)
    {
        _prefab = prefab;

        _pool = new ObjectPool<T>(
            createFunc: () => CreateAnObject(_prefab),
            actionOnGet: (obj) => ActionOnGet(obj),
            actionOnRelease: (obj) => ActionOnRelease(obj),
            actionOnDestroy: (obj) => UnityEngine.Object.Destroy(obj),
            collectionCheck: true,
            defaultCapacity: capacity,
            maxSize: maxSize);
    }

    public T Get()
    {
              return _pool.Get();
    }

    public void Release(T obj)
    {
        _pool.Release(obj);
    }

    private T CreateAnObject(T prefab)
    {
        _createdObjects++;
        CreatedCountChanged?.Invoke(_createdObjects);

        return UnityEngine.Object.Instantiate(prefab);
    }

    private void ActionOnGet(T obj)
    {
        obj.gameObject.SetActive(true);

        _allObjects++;
        AllCountChanged?.Invoke(_allObjects);

        _activeObjectCounter++;
        ActiveCountChanged?.Invoke(_activeObjectCounter);
        

        if (obj.TryGetComponent(out IPoolable poolable))
            poolable.OnSpawn();
    }

    private void ActionOnRelease(T obj)
    {
        obj.gameObject.SetActive(false);

        _activeObjectCounter--;
        ActiveCountChanged.Invoke(_activeObjectCounter);

        if (obj.TryGetComponent(out IPoolable poolable))
            poolable.OnDespawn();
    }
}